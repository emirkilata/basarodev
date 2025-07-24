using NTSGPolygon = NetTopologySuite.Geometries.Polygon;

namespace internshipTechnicalProject.Domain
{
    public class Polygon
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Geometry { get; set; } = null!;
    }
} 