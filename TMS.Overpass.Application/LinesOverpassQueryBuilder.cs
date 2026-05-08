namespace TMS.Overpass.Application;

internal static class LinesOverpassQueryBuilder
{
    public static string Build(string lineCode)
    {
        return $"""
[out:json][timeout:25];
(
  way["ref:GB:tfgm"='{lineCode}'];
);
(._; >;);
out body;
>;
out skel qt;
""";
    }
}