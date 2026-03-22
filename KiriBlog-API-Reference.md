# KiriBlog API Reference

## Base URL
- Local development: `https://localhost:{port}`
- Swagger UI is enabled in development at root (`/`) by current app setup.

## Authentication
KiriBlog uses JWT Bearer authentication.

- Protected endpoints require:

```http
Authorization: Bearer <access_token>
```

- Public endpoints are explicitly marked with `AllowAnonymous`.
- Claims currently issued in JWT:
  - `sub`: user id
  - `http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier`: user id
  - `email`: user email
  - `http://schemas.microsoft.com/ws/2008/06/identity/claims/role`: role

### Claims used by API
- `userId` is resolved from JWT claims (`NameIdentifier` or `sub`), never from body.
- `role` claim is used where role authorization is required (e.g. post creation).

## Error Format
The codebase defines domain/application exceptions, but a centralized error payload contract is not visible in the provided context.

Use this practical frontend expectation:
- Always handle by HTTP status first (`400`, `401`, `403`, `404`).
- Then read response body message/details if present.

Typical JSON shape in ASP.NET APIs (may vary by middleware configuration):

```json
{
  "status": 400,
  "title": "Bad Request",
  "detail": "Comment content cannot be empty"
}
```

---

## Auth

### POST `/api/User/login`

**Purpose**
- Authenticates an existing user and returns an access token.
- Product problem solved: enables a logged-out user to start an authenticated session.
- Frontend should use this on login form submit before calling protected endpoints.

**Authentication**
- Public (`AllowAnonymous`).
- No JWT required.

**Inputs**
- Route params: none.
- Query params: none.
- Body (`LoginRequestDto`):
  - `email` (string, required)
  - `password` (string, required)

**Request JSON example**
```json
{
  "email": "user@kiriblog.com",
  "password": "MySecurePass123"
}
```

**Success response**
- `200 OK`
- Body (`LoginResponseDto`):
  - `userId` (guid)
  - `name` (string)
  - `email` (string)
  - `role` (string)
  - `token` (string)

**Response JSON example**
```json
{
  "userId": "5b6cce42-c6f7-43be-beb0-d9f17473d0a8",
  "name": "Kiri",
  "email": "user@kiriblog.com",
  "role": "Visitor",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Possible errors**
- `400 Bad Request`: invalid input shape/validation.
- `401 Unauthorized`: invalid credentials.
- `403 Forbidden`: not expected for this endpoint.
- `404 Not Found`: not expected for this endpoint.

**Frontend Implementation Warnings**
- Do not store token in unsafe storage if you can avoid it.
- Treat `401` as authentication failure (show credentials error).
- Do not assume `role` blindly on client for security decisions.
- Save token and attach it only to protected calls.

---

### POST `/api/User/register-visitor`

**Purpose**
- Registers a new visitor account and returns a JWT.
- Product problem solved: onboarding new users without admin intervention.
- Frontend should use this in signup flow for visitor users.

**Authentication**
- Public (`AllowAnonymous`).

**Inputs**
- Route params: none.
- Query params: none.
- Body (`RegisterRequestDto`):
  - `name` (string, required)
  - `lastName` (string, required)
  - `email` (string, required, email format)
  - `password` (string, required, min length 6)

**Request JSON example**
```json
{
  "name": "Kiri",
  "lastName": "Dev",
  "email": "newuser@kiriblog.com",
  "password": "MySecurePass123"
}
```

**Success response**
- `201 Created`
- Body (`RegisterResponseDto`):
  - `name` (string)
  - `lastName` (string)
  - `email` (string)
  - `token` (string)

**Response JSON example**
```json
{
  "name": "Kiri",
  "lastName": "Dev",
  "email": "newuser@kiriblog.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Possible errors**
- `400 Bad Request`: invalid fields/validation.
- `401 Unauthorized`: not expected for this endpoint.
- `403 Forbidden`: not expected for this endpoint.
- `404 Not Found`: not expected for this endpoint.

**Frontend Implementation Warnings**
- Do not send extra fields (especially role or userId).
- Handle duplicate email scenario as a user-facing message.
- After success, persist token and route to authenticated area.

---

## Posts

### POST `/api/post`

**Purpose**
- Creates a new blog post.
- Product problem solved: allows authors to publish content.
- Frontend should call this from author dashboard/editor publish action.

**Authentication**
- Bearer token required.
- Role required: `Author` (`[Authorize(Roles = "Author")]`).
- Backend also validates author existence by request email.

**Inputs**
- Route params: none.
- Query params: none.
- Body (`CreatePostRequest`):
  - `email` (string, required)
  - `title` (string, required)
  - `content` (string, required)
  - `visibility` (enum: `Public` | `Private`)
  - `language` (enum: `English` | `Spanish`)

**Request JSON example**
```json
{
  "email": "author@kiriblog.com",
  "title": "Clean Architecture in Practice",
  "content": "# Intro\nThis is the full markdown content...",
  "visibility": "Public",
  "language": "English"
}
```

**Success response**
- `200 OK`
- Body: empty (current implementation returns `Ok()` with no payload).

**Response JSON example**
```json
{}
```

**Possible errors**
- `400 Bad Request`: invalid input.
- `401 Unauthorized`: missing/invalid JWT.
- `403 Forbidden`: authenticated user without `Author` role.
- `404 Not Found`: author not found by email.

**Frontend Implementation Warnings**
- Do not call this endpoint for non-author users.
- Keep enum values aligned with backend (`Public/Private`, `English/Spanish`).
- Current contract still requires `email` in body; keep it consistent with logged user data.
- Handle duplicate title as a specific UX error.

---

### GET `/api/post/public`

**Purpose**
- Returns public post list for feed/homepage.
- Product problem solved: gives users a lightweight catalog of available content.
- Frontend should use this for listing pages (home, explore).

**Authentication**
- Public (`AllowAnonymous`).

**Inputs**
- Route params: none.
- Query params: none.
- Body: none.

**Request JSON example**
```json
{}
```

**Success response**
- `200 OK`
- Body: array of `PostListItemResponse` items:
  - `id` (guid)
  - `title` (string)
  - `excerpt` (string)
  - `createdAt` (datetime)

**Response JSON example**
```json
[
  {
    "id": "70fd2b55-0cad-4e4f-bff9-e2fe9d64b2e0",
    "title": "Clean Architecture in Practice",
    "excerpt": "# Intro This is the first 200 chars...",
    "createdAt": "2026-03-19T15:10:00Z"
  }
]
```

**Possible errors**
- `400 Bad Request`: not expected in normal flow.
- `401 Unauthorized`: not expected for this endpoint.
- `403 Forbidden`: not expected for this endpoint.
- `404 Not Found`: not expected; empty array is valid.

**Frontend Implementation Warnings**
- Treat empty list as valid state (no posts yet), not an error.
- `excerpt` is generated server-side; do not recompute from full content.
- Use `id` from list to request details endpoint.

---

### GET `/api/post/{id}`

**Purpose**
- Returns full post detail for reading view.
- Product problem solved: retrieves complete content for a selected post.
- Frontend should call this when user opens a post detail page.

**Authentication**
- Public (`AllowAnonymous`).

**Inputs**
- Route params:
  - `id` (guid, required)
- Query params: none.
- Body: none.

**Request JSON example**
```json
{}
```

**Success response**
- `200 OK`
- Body (`PostDetailResponse`):
  - `id` (guid)
  - `title` (string)
  - `content` (string)
  - `createdAt` (datetime)
  - `language` (string)

**Response JSON example**
```json
{
  "id": "70fd2b55-0cad-4e4f-bff9-e2fe9d64b2e0",
  "title": "Clean Architecture in Practice",
  "content": "# Intro\nFull markdown content...",
  "createdAt": "2026-03-19T15:10:00Z",
  "language": "English"
}
```

**Possible errors**
- `400 Bad Request`: invalid `id` format.
- `401 Unauthorized`: not expected.
- `403 Forbidden`: not expected.
- `404 Not Found`: post not found.

**Frontend Implementation Warnings**
- Render `content` as markdown safely (sanitize if needed).
- If `404`, redirect to not-found screen instead of generic error.
- Do not assume private posts are available on this endpoint unless backend rules confirm it.

---

## Comments

### POST `/api/comments`

**Purpose**
- Creates a top-level comment on a post.
- Product problem solved: allows authenticated users to join discussion on a post.
- Frontend should use this on comment form submit in post detail screen.

**Authentication**
- Bearer token required.
- Claim used: `userId` from JWT (`NameIdentifier` or `sub`).

**Inputs**
- Route params: none.
- Query params: none.
- Body (`CreateCommentRequest`):
  - `postId` (guid, required)
  - `content` (string, required)

**Request JSON example**
```json
{
  "postId": "70fd2b55-0cad-4e4f-bff9-e2fe9d64b2e0",
  "content": "Great article!"
}
```

**Success response**
- `201 Created`
- Body (`CreateCommentResponse`):
  - `id` (guid)
  - `content` (string)
  - `createdAt` (datetime)

**Response JSON example**
```json
{
  "id": "2fb7f0d0-f4f8-4b11-beb8-1b2af00c731f",
  "content": "Great article!",
  "createdAt": "2026-03-19T20:30:00Z"
}
```

**Possible errors**
- `400 Bad Request`: invalid payload or empty content.
- `401 Unauthorized`: missing/invalid token or missing user claim.
- `403 Forbidden`: not expected by current endpoint policy.
- `404 Not Found`: not expected by current use case contract.

**Frontend Implementation Warnings**
- Never send `userId` in body.
- Disable submit while request is in flight to avoid duplicates.
- Insert new comment in UI only after `201` or rollback optimistic state on failure.

---

### POST `/api/comments/{commentId}/replies`

**Purpose**
- Creates a reply to a specific parent comment.
- Product problem solved: supports threaded conversation under comments.
- Frontend should use this when user submits reply in a comment thread.

**Authentication**
- Bearer token required.
- Claim used: `userId` from JWT.

**Inputs**
- Route params:
  - `commentId` (guid, required)
- Query params: none.
- Body (`ReplyToCommentRequestDto`):
  - `parentCommentId` (guid, optional; backend overwrites with route `commentId`)
  - `content` (string, required)

**Request JSON example**
```json
{
  "parentCommentId": "2fb7f0d0-f4f8-4b11-beb8-1b2af00c731f",
  "content": "I have the same question."
}
```

**Success response**
- `201 Created`
- Body (`ReplyToCommentResponseDto`):
  - `id` (guid)
  - `parentCommentId` (guid)
  - `postId` (guid)
  - `userId` (guid)
  - `content` (string)
  - `createdAt` (datetime)

**Response JSON example**
```json
{
  "id": "9b8a7d0a-17a2-4918-9b45-f72f3c2fca43",
  "parentCommentId": "2fb7f0d0-f4f8-4b11-beb8-1b2af00c731f",
  "postId": "70fd2b55-0cad-4e4f-bff9-e2fe9d64b2e0",
  "userId": "5b6cce42-c6f7-43be-beb0-d9f17473d0a8",
  "content": "I have the same question.",
  "createdAt": "2026-03-19T20:35:00Z"
}
```

**Possible errors**
- `400 Bad Request`: invalid `commentId` or empty content.
- `401 Unauthorized`: missing/invalid token or missing user claim.
- `403 Forbidden`: not expected by current endpoint policy.
- `404 Not Found`: parent comment not found.

**Frontend Implementation Warnings**
- Do not send `parentCommentId` from client logic as source of truth; route param defines it.
- Handle parent deleted/not found by removing reply box and showing contextual message.
- If reply list is open, append new reply and increment parent `RepliesCount` in local state.

---

### GET `/api/comments/{commentId}/replies`

**Purpose**
- Returns direct replies for one comment.
- Product problem solved: lazy-load thread details only when user expands a comment.
- Frontend should call this when user opens/expands replies.

**Authentication**
- Public (`AllowAnonymous`).

**Inputs**
- Route params:
  - `commentId` (guid, required)
- Query params: none.
- Body: none.

**Request JSON example**
```json
{}
```

**Success response**
- `200 OK`
- Body (`GetRepliesByCommentIdResponse`):
  - `replies`: `CommentReplyDto[]`

`CommentReplyDto`:
- `id` (guid)
- `content` (string)
- `userId` (guid)
- `authorName` (string)
- `createdAt` (datetime)
- `repliesCount` (int)
- `hasReplies` (bool, computed as `repliesCount > 0`)

**Response JSON example**
```json
{
  "replies": [
    {
      "id": "3ef57cf0-5b9d-45dc-8684-af4f2f778f9a",
      "content": "Thanks for the explanation",
      "userId": "2e908928-06ea-4c2c-ae77-3763a367997f",
      "authorName": "Kiri Dev",
      "createdAt": "2026-03-19T20:45:00Z",
      "repliesCount": 1,
      "hasReplies": true
    }
  ]
}
```

**Possible errors**
- `400 Bad Request`: invalid `commentId`.
- `401 Unauthorized`: not expected (`AllowAnonymous`).
- `403 Forbidden`: not expected (`AllowAnonymous`).
- `404 Not Found`: not expected by current implementation; empty `replies` list is valid.

**Frontend Implementation Warnings**
- Do not issue one request per reply to compute children count; use `repliesCount` from payload.
- Empty list means no replies (valid UX state).
- `repliesCount` is already optimized server-side via aggregation (no N+1).

---

### GET `/api/comments/post/{postId}`

**Purpose**
- Returns top-level comments for a post (`ParentCommentId = null`).
- Product problem solved: loads discussion root for post detail pages.
- Frontend should call this when opening comment section of a post.

**Authentication**
- Public (`AllowAnonymous`).

**Inputs**
- Route params:
  - `postId` (guid, required)
- Query params: none.
- Body: none.

**Request JSON example**
```json
{}
```

**Success response**
- `200 OK`
- Body (`GetCommentsByPostResponse`):
  - `comments`: `CommentDto[]`

`CommentDto`:
- `id` (guid)
- `content` (string)
- `userId` (guid)
- `authorName` (string)
- `createdAt` (datetime)
- `repliesCount` (int)

**Response JSON example**
```json
{
  "comments": [
    {
      "id": "2fb7f0d0-f4f8-4b11-beb8-1b2af00c731f",
      "content": "Great article!",
      "userId": "5b6cce42-c6f7-43be-beb0-d9f17473d0a8",
      "authorName": "Kiri Dev",
      "createdAt": "2026-03-19T20:30:00Z",
      "repliesCount": 3
    }
  ]
}
```

**Possible errors**
- `400 Bad Request`: invalid `postId`.
- `401 Unauthorized`: not expected (`AllowAnonymous`).
- `403 Forbidden`: not expected (`AllowAnonymous`).
- `404 Not Found`: not expected by current implementation; empty `comments` list is valid.

**Frontend Implementation Warnings**
- Use `repliesCount` to decide if "Show replies" button should appear.
- Do not infer deletion state client-side; deleted comments are already filtered server-side.
- Support empty state gracefully (new posts may have zero comments).

---

### DELETE `/api/comments/{commentId}`

**Purpose**
- Soft-deletes a comment (`IsDeleted = true`) for the owning authenticated user.
- Product problem solved: allows moderation/self-cleanup without losing historical linkage.
- Frontend should call this for user-initiated delete actions.

**Authentication**
- Bearer token required.
- Claim used: `userId` from JWT.

**Inputs**
- Route params:
  - `commentId` (guid, required)
- Query params: none.
- Body: none.

**Request JSON example**
```json
{}
```

**Success response**
- `200 OK`
- Body (`DeleteCommentResponseDto`):
  - `id` (guid)
  - `isDeleted` (bool)

**Response JSON example**
```json
{
  "id": "2fb7f0d0-f4f8-4b11-beb8-1b2af00c731f",
  "isDeleted": true
}
```

**Possible errors**
- `400 Bad Request`: invalid `commentId`.
- `401 Unauthorized`: missing/invalid token or missing user claim.
- `403 Forbidden`: user is not owner of the comment.
- `404 Not Found`: comment does not exist.

**Frontend Implementation Warnings**
- Do not assume hard delete: remove from UI but understand backend uses soft delete.
- Separate `401` (session/auth issue) from `403` (permission issue) in UX messaging.
- If delete succeeds, update local comment/reply counts to keep UI consistent.
- Avoid retry loops on already-deleted records.

---

## Product-Level Notes for Frontend Teams

- Use Auth endpoints to obtain JWT, then attach token only where required.
- Respect No Trust in Client: never send `userId` or role decisions as authority.
- Comments module is optimized to avoid N+1 for `RepliesCount`; consume returned counts directly.
- Soft delete means list endpoints naturally hide deleted comments; frontend should not attempt to rehydrate deleted comments from stale cache.

