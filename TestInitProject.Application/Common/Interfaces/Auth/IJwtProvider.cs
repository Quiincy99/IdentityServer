
using TestInitProject.Domain.Entities;

namespace TestInitProject.Application;

public interface IJwtProvider
{
    string Generate(Customer customer);
}
