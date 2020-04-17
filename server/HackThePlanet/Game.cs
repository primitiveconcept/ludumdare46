namespace HackThePlanet
{
	using Microsoft.Extensions.Logging;
	using PrimitiveEngine;


	public class Game : GameLoop
	{
		private static readonly ILogger logger = ApplicationLogging.CreateLogger<Game>();

		private EntityWorld entityWorld;


		#region Constructors
		public Game()
		{
			this.entityWorld = new EntityWorld();
		}
		#endregion


		#region Properties
		public EntityWorld EntityWorld
		{
			get { return this.entityWorld; }
		}
		#endregion


		public override void Update(long deltaTime)
		{
			// TODO
		}
	}
}