using Domain.Entities;

namespace Domain.Interface.Common.Security;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
