namespace OlimpiaAPI.Models
{
    public record CreatePlayerDto(string  Name, int Age, int Height, int Weight);

    public record UpdatePlayerDto(string Name, int Age, int Height, int Weight);

    public record CreateDatDto(string? Country, string? County, string? Description, Guid? PlayerId);

    public record UpdateDatDto(string? Country, string? County, string? Description, Guid? PlayerId);
}
