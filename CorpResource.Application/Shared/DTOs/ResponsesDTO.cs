namespace CorpResource.Application.Shared.DTOs;

public class CreationResponseDTO
{
    public string Message { get; set; }
    public Guid Id { get; set; }
}

public class  ErrorResponseDTO
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
}
