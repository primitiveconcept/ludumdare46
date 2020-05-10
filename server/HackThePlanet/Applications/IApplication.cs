namespace HackThePlanet
{
    using System;


    public interface IApplication
    {
        #region Properties
        ushort ProcessId { get; set; }
        int OriginEntityId { get; set; }
        ushort RamUse { get; }
        StringReference User { get; set; }
        #endregion
    }


    public static class ApplicationExtensions
    {
        public static string GetName(this IApplication application)
        {
            return application
                .GetType()
                .Name
                .Replace("Application", string.Empty)
                .ToLower();
        }
    }
}