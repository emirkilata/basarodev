namespace internshipTechnicalProject.Domain.Point
{
    public class Point
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public string ToWKT()
        {
            return $"POINT({X} {Y})";
        }
    }
}
