namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using Microsoft.Extensions.Logging;
	using PrimitiveEngine;


	/// <summary>
	/// Main game state and game loop.
	/// </summary>
	public class GameThread : GameLoop
	{
		private static readonly ILogger logger = ApplicationLogging.CreateLogger<GameThread>();

		private List<Action> queuedProcesses = new List<Action>();
		private EntityWorld entityWorld;
		private object threadLock = new object();

		#region Constructors
		public GameThread()
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
			ExecuteProcessQueue();
			
			this.entityWorld.FixedUpdate(deltaTime);
			Thread.Sleep(15); // Fuck it, let's not get fancy here.
		}


		public void QueueProcess(Action process)
		{
			lock (this.threadLock)
			{
				this.queuedProcesses.Add(process);
			}
		}

		private void ExecuteProcessQueue()
		{
			lock (this.threadLock)
			{
				for (int i = 0; i < this.queuedProcesses.Count; i++)
				{
					this.queuedProcesses[i].Invoke();
				}
				this.queuedProcesses.Clear();	
			}
		}
	}
}