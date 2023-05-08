namespace GarageLog.ErrorHandling
{
    
    public class NoKeyInAppSettings : Exception
    {
        public NoKeyInAppSettings(string message) : base(message)
        {
        }

        public NoKeyInAppSettings() : base()
        {
        }

        public NoKeyInAppSettings(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
