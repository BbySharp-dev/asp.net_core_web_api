namespace Asp.netCoreWeb.Models.DTO;

public class AddRegionRequestDto
{
    public string? Code { get; set; } = string.Empty;

    public string? Name { get; set; } = string.Empty;

    public string? RegionImageUrl { get; set; }
}