using AutoMapper;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Users.Queries.GetUsersWithPagination;

public class UserDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    private sealed class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
