namespace HackThePlanet
{
	using System.Threading;
	using Microsoft.Extensions.Logging;
	using PrimitiveEngine;


	/// <summary>
	/// Main game state and game loop.
	/// </summary>
	public class Game : GameLoop
	{
		private static readonly ILogger logger = ApplicationLogging.CreateLogger<Game>();

		private EntityWorld entityWorld;


		#region Constructors
		public Game()
		{
			this.entityWorld = new EntityWorld();
			this.entityWorld.InitializeAll();
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
			this.entityWorld.FixedUpdate(deltaTime);
			Thread.Sleep(15); // Fuck it, let's not get fancy here.
		}
	}
}