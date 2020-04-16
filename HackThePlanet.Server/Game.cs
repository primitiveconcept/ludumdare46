namespace HackThePlanet.Server
{
	using PrimitiveEngine;


	public class Game : GameLoop
	{
		private EntityWorld entityWorld;

		public EntityWorld EntityWorld
		{
			get { return this.entityWorld; }
		}


		public Game()
		{
			this.entityWorld = new EntityWorld();
		}

		
		public override void Update(long deltaTime)
		{
			// TODO
		}
	}
}