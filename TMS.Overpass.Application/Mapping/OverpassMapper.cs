using Riok.Mapperly.Abstractions;
using TMS.Overpass.Domain;
using TMS.Overpass.Domain.DTOs;
using TMS.Overpass.Domain.Overpass;

namespace TMS.Overpass.Application.Mapping;

[Mapper]
public partial class OverpassMapper
{
    public IEnumerable<LinesDTO> Map(OverpassResponse response, string LineCode)
        => response.Elements
                .Select(x => new LinesDTO()
                {
                    OpenStreetMapsId = x.Id,
                    Coordinates = new Coordinates()
                    {
                        Latitude = x.Latitude,
                        Longitude = x.Longitude
                    },
                    LineCode = LineCode
                });
                   
}
