using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using TestInitProject.Application.Common.Interfaces;
using TestInitProject.Application.Common.Mapping;
using TestInitProject.Application.Common.Models;

namespace TestInitProject.Application.Users.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : IRequest<PaginatedList<UserDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserDto>>
{
    private readonly IUserRepository _UserRepository;
    private readonly IMapper _mapper;
    public GetUsersWithPaginationQueryHandler(IUserRepository UserRepository, IMapper mapper)
    {
        _mapper = mapper;
        _UserRepository = UserRepository;
    }
    public async Task<PaginatedList<UserDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _UserRepository
            .AsQueryable()
            .OrderBy(x => x.Name)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
