namespace TMS.Overpass.Domain.DTOs;

public sealed class LinesDTO
{
    public required int OpenStreetMapsId { get; set; }
    public required Coordinates Coordinates { get; set; }
    public required string LineCode { get; set; }
}