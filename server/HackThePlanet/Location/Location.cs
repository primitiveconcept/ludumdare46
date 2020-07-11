namespace HackThePlanet
{
    using GeoCoordinatePortable;
    using PrimitiveEngine;

    public class Location : IEntityComponent
    {
        #region Properties
        public GeoCoordinate Coordinates { get; set; }
        #endregion


        /// <summary>
        /// Get the distance to another Location, in meters.
        /// </summary>
        /// <param name="otherLocation"></param>
        /// <returns></returns>
        public double GetDistanceTo(Location otherLocation)
        {
            return this.Coordinates.GetDistanceTo(otherLocation.Coordinates);
        }
    }
}