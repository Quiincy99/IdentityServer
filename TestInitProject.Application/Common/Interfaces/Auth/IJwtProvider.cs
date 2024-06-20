
using TestInitProject.Domain.Entities;

namespace TestInitProject.Application;

public interface IJwtProvider
{
    Task<string> GenerateAsync(Customer customer);
}
