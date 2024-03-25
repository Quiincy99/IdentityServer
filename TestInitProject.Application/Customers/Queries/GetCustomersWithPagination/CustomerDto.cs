using AutoMapper;
using TestInitProject.Domain;

public class CustomerDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}