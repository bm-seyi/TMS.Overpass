using Riok.Mapperly.Abstractions;
using TMS.Overpass.Domain;
using TMS.Overpass.Domain.DTOs;
using TMS.Overpass.Domain.Overpass;

namespace TMS.Overpass.Application.Mapping;

[Mapper]
public partial class OverpassMapper
{
    [MapProperty(nameof(Node.Id), nameof(LinesDTO.OpenStreetMapsId))]
    [MapProperty(nameof(Node), nameof(LinesDTO.Coordinates))]
    public partial LinesDTO Map(Node node, string lineCode);

    
    [MapperIgnoreSource(nameof(Node.Type))]
    [MapperIgnoreSource(nameof(Node.Id))]
    [MapperIgnoreSource(nameof(Node.Tags))]

    public partial Coordinates MapCoordinates(Node element);

    public IEnumerable<LinesDTO> Map(OverpassResponse response, string lineCode) => response.Nodes.Select(e => Map(e, lineCode));
}
