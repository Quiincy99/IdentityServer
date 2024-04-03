using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using TestInitProject.Application.Common.Interfaces;
using TestInitProject.Application.Common.Mapping;
using TestInitProject.Application.Common.Models;

namespace TestInitProject.Application.Customers.Queries.GetCustomersWithPagination;

public record GetCustomersWithPaginationQuery : IRequest<PaginatedList<CustomerDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCustomersWithPaginationQueryHandler : IRequestHandler<GetCustomersWithPaginationQuery, PaginatedList<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    public GetCustomersWithPaginationQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }
    public async Task<PaginatedList<CustomerDto>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository
            .AsQueryable()
            .OrderBy(x => x.Name)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
