using AutoMapper;
using TestInitProject.Domain.Entities;

namespace TestInitProject.Application.Users.Queries.GetUsersWithPagination;

public class UserDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
