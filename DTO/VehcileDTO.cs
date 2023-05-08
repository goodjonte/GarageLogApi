using static GarageLog.Models.Vehcile;

namespace GarageLog.DTO
{
    public class VehcileDTO
    {
        public string JWT { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public bool IsHours { get; set; }
        public int? KilometersOrHours { get; set; }
        public VType VehcileType { get; set; }
     
    }
}

