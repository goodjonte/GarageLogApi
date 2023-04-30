namespace GarageLog.Models
{
    public class Maintenance
    {
        public Guid Id { get; set; }
        public Guid VehcileId { get; set; }
        public string? Name { get; set; }
        public DateTime? DueDate { get; set; }
        public int? DueKilometers { get; set; }
        public int? DueHours { get; set; }
        public string? Notes { get; set; }
        public MType MaintType { get; set; }
        public enum MType
        {
            OilAndFilter = 0,
            AirFilter = 1,
            FuelFilter = 2,
            SparkPlugs = 3,
            TimingBelt = 4,
            BrakePads = 5,
            BrakeDiscs = 6,
            BrakeFluid = 7,
            Coolant = 8,
            PowerSteeringFluid = 9,
            TransmissionFluid = 10,
            TransmissionFilter = 11,
            DifferentialFluid = 12,
            WheelAlignment = 13,
            Tires = 14,
            WiperBlades = 15,
            Battery = 16,
            TireRotation = 17,
            SerpentineBelt = 18,
            Suspension = 19,
            Other = 20,
        }


    }
}
