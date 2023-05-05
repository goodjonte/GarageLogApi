namespace GarageLog.Models
{
    public class Vehcile
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public string? Name { get; set; }
        public string? Img { get; set; }
        public bool IsHours { get; set; }
        public int? KilometersOrHours { get; set; }
        public VType VehcileType { get; set; }
        public enum VType
        {
            Car = 0,
            Motorbike = 1,
            Truck = 2,
            Tractor = 3,
            Bus = 4,
            Boat = 5,
            Plane = 6,
            Helicopter = 7,
            Other = 8,
        }
    }
}
