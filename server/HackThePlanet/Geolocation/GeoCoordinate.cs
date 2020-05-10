namespace HackThePlanet
{
    using System;


    public struct GeoCoordinate
    {
        public readonly double Latitude;
        public readonly double Longitude;
        
        
        #region Constructors
        public GeoCoordinate(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        #endregion


        /// <summary>
        /// Get the distance from this coordinate to another in meters.
        /// </summary>
        /// <param name="otherCoordinate">Other coordinate to get distance to.</param>
        /// <returns>Distance in meters.</returns>
        public double GetDistanceTo(GeoCoordinate otherCoordinate)
        {
            return GetDistance(this, otherCoordinate);
        }
        

        /// <summary>
        /// Get the distance between two GeoCoordinates in meters.
        /// </summary>
        /// <param name="coordinate">First GeoCoordinate.</param>
        /// <param name="otherCoordinate">Other GeoCoordinate.</param>
        /// <returns>Distance in meters.</returns>
        public static double GetDistance(
            GeoCoordinate coordinate,
            GeoCoordinate otherCoordinate)
        {
            return GetDistance(
                coordinate.Latitude,
                coordinate.Longitude,
                otherCoordinate.Latitude,
                otherCoordinate.Longitude);
        }
        
        
        /// <summary>
        /// Get the distance between two sets of latitude and longitude in meters.
        /// </summary>
        /// <param name="latitude">First latitude.</param>
        /// <param name="longitude">First longitude.</param>
        /// <param name="otherLatitude">Other latitude.</param>
        /// <param name="otherLongitude">Other longitude.</param>
        /// <returns>Distance in meters.</returns>
        public static double GetDistance(
            double latitude,
            double longitude,
            double otherLatitude,
            double otherLongitude)
        {
            double d1 = latitude * (Math.PI / 180.0);
            double num1 = longitude * (Math.PI / 180.0);
            double d2 = otherLatitude * (Math.PI / 180.0);
            double num2 = otherLongitude * (Math.PI / 180.0) - num1;
            double d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
    
    
    

    
}