namespace GarageLog
{
    public class Vehcile
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Img { get; set; }
        public bool IsHours { get; set; }
        public int? KilometersOrHours { get; set; }
        public enum VehcileType
        {
            Car,
            Motorbike,
            Truck,
            Tractor,
            Bus,
            Boat,
            Plane,
            Helicopter
        }
    }
}
