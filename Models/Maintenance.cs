namespace GarageLog.Models
{
    public class Maintenance
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DueDate { get; set; }
        public int? DueKilometers { get; set; }
        public int? DueHours { get; set; }
        public string? Notes { get; set; }
        public MType MaintType { get; set; }
        public enum MType
        {
            OilAndFilter,
            AirFilter,
            FuelFilter,
            SparkPlugs,
            TimingBelt,
            BrakePads,
            BrakeDiscs,
            BrakeFluid,
            Coolant,
            PowerSteeringFluid,
            TransmissionFluid,
            TransmissionFilter,
            DifferentialFluid,
            WheelAlignment,
            Tires,
            WiperBlades,
            Battery,
            TireRotation,
            SerpentineBelt,
            Suspension,
            Other
        }


    }
}
