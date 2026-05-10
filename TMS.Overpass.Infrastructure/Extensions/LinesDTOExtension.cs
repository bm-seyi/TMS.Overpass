using System.Data;
using TMS.Overpass.Domain.DTOs;

namespace TMS.Overpass.Infrastructure.Extensions;

internal static class LinesDTOExtension
{
    extension(IEnumerable<LinesDTO> linesDTOs)
    {
        public DataTable ToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("OpenStreetMapsId", typeof(int));
            table.Columns.Add("Latitude", typeof(decimal));
            table.Columns.Add("Longitude", typeof(decimal));
            table.Columns.Add("LineCode", typeof(string));

            foreach (var line in linesDTOs)
            {
                table.Rows.Add(line.OpenStreetMapsId, line.Coordinates.Latitude, line.Coordinates.Longitude, line.LineCode);
            }

            return table;
        }   
    }
}