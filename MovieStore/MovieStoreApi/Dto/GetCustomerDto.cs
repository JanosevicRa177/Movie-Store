namespace MovieStoreApi.Dto;

public class GetCustomerDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
}