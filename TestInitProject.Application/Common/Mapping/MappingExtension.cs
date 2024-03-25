using Microsoft.EntityFrameworkCore;
using TestInitProject.Application.Common.Models;

namespace TestInitProject.Application.Common.Mapping;

public static class MappingExtenion
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
}
