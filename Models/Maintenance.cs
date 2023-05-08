namespace GarageLog.Models
{
    public class Maintenance
    {
        public Guid Id { get; set; }
        public Guid VehcileId { get; set; }
        public string? Name { get; set; }
        public bool DateBool { get; set; } = false;
        public DateTime? DoneAtDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? DoneAtKilometers { get; set; }
        public int? DueKilometers { get; set; }
        public string? Notes { get; set; }
        public MType MaintType { get; set; }
        public enum MType
        {
            AirFilter = 0,
            Battery = 1,
            BrakeDiscs = 2,
            BrakeFluid = 3,
            BrakePads = 4,
            Coolant = 5,
            DifferentialFluid = 6,
            FuelFilter = 7,
            OilAndFilter = 8,
            Other = 9,
            PowerSteeringFluid = 10,
            SerpentineBelt = 11,
            SparkPlugs = 12,
            Suspension = 13,
            Tires = 14,
            TireRotation = 15,
            TimingBelt = 16,
            TransmissionFilter = 17,
            TransmissionFluid = 18,
            WheelAlignment = 19,
            WiperBlades = 20
        }


    }
}
