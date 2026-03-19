# ARCHITECTURE.md - KiriBlog

## Technical Stack

* **Runtime:** .NET
* **Database:** PostgreSQL (Supabase)
* **ORM:** Entity Framework Core
* **Authentication:** JWT (ASP.NET Core)
* **Mapping:** Manual (Plain C#, no AutoMapper)
* **Architecture Style:** Layered (Clean Architecture inspired)

---

#  Language Policy

* All code must be written in **English**
* Exceptions and messages must be in **English**
* DTOs, entities, and methods must follow **clear naming conventions**

---

# Solution Structure (Layered Architecture)

---

## I. Domain Layer

### Responsibility:

Business core. Defines the **rules of the system**.

### Components:

* Entities (e.g., `Post`, `Comment`, `User`)
* Enums
* Repository Interfaces (e.g., `ICommentRepository`)
* Custom Exceptions

### Rules:

*  No dependencies on other layers
*  No EF Core or external frameworks
*  Contains business validations
*  Entities encapsulate logic

---

## II. Application Layer

### Responsibility:

Orchestrates application logic through **Use Cases**

### Components:

* UseCases (e.g., `CreateCommentUseCase`, `ReplyToCommentUseCase`)
* DTOs (`Request`, `Response`)
* Interfaces (e.g., `ICreateCommentUseCase`)
* Services (optional orchestration layer)
* UnitOfWork usage

---

### Rules:

* Each UseCase has a **single responsibility**
* Uses DTOs (never expose Entities to API)
* Calls Domain for validations
* Calls Repositories via interfaces
* Handles flow of the operation
* No direct access to DbContext
* No business logic duplication

---

### Use Case Pattern

```csharp
public class CreateCommentUseCase : ICreateCommentUseCase
{
    private readonly ICommentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentUseCase(ICommentRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCommentResponse> ExecuteAsync(CreateCommentRequest request)
    {
        var comment = new Comment(request.PostId, request.UserId, request.Content);

        await _repository.CreateAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return new CreateCommentResponse
        {
            Id = comment.Id,
            Content = comment.Content
        };
    }
}
```

---

## III. Infrastructure Layer

### Responsibility:

Handles **data persistence and external systems**

### Components:

* `ApplicationDbContext`
* Repository Implementations
* UnitOfWork
* Dependency Injection

---

### Rules:

* Implements Domain interfaces
* Uses EF Core
* Handles queries and persistence
* No business logic
* No UseCase logic

---

## IV. API Layer

### Responsibility:

Expose HTTP endpoints

### Components:

* Controllers
* Authentication / Authorization
* Middleware (if needed)

---

### Rules:

* Receives Request DTOs
* Calls UseCases
* Returns Responses
* No business logic
* No direct DbContext usage

---

### Controller Example

```csharp
[HttpPost("{commentId:guid}/replies")]
public async Task<IActionResult> ReplyToComment(Guid commentId, [FromBody] ReplyToCommentRequestDto request)
{
    request.ParentCommentId = commentId;

    var response = await _replyToCommentUseCase.ExecuteAsync(request);

    return StatusCode(201, response);
}
```

---

# Request Flow

```text
API → Application → Domain → Application → Infrastructure → Database
```
---

# Core Rules
## 1. Separation of Concerns
* API → HTTP only
* Application → orchestration
* Domain → business logic
* Infrastructure → data

---

## 2. No Entity Exposure

Return Entities
Use DTOs

---

## 3. No Trust in Client

`userId` from request
Extract from JWT

---

## 4. Avoid Logic in Controllers

Controllers with logic
UseCases handle everything

---

## 5. Async Everywhere

All database calls must be async

---

# Feature Development Workflow

---

## 1. Domain

* Create/modify Entity
* Add validations
* Define Repository interface

---

## 2. Infrastructure

* Configure EF mapping
* Implement Repository
* Add indexes if needed

---

## 3. Application

* Create DTOs
* Create UseCase
* Map Entity ↔ DTO manually
* Use UnitOfWork

---

## 4. API

* Create Controller endpoint
* Inject UseCase
* Return response

---

# Best Practices

* Keep UseCases small and focused
* Avoid N+1 queries
* Use indexes in DB
* Prefer explicit logic over magic
* Favor readability over abstraction

---

# Philosophy

> The system is simple because each layer does only one thing — but does it well.

---
