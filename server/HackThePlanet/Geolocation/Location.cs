namespace HackThePlanet
{
    using PrimitiveEngine;

    public class Location : IEntityComponent
    {
        #region Properties
        public GeoCoordinate Coordinates { get; set; }
        #endregion


        public double GetDistanceTo(Location otherLocation)
        {
            return this.Coordinates.GetDistanceTo(otherLocation.Coordinates);
        }
    }
}