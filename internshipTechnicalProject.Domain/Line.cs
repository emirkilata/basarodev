using NetTopologySuite.Geometries;

namespace internshipTechnicalProject.Domain
{
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Geometry { get; set; } = null!;
    }
} 