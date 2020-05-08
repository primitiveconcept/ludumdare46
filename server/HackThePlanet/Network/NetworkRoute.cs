namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using System.Linq;


	public class NetworkRoute
	{
		private IGraph<NetworkInterface> graph;
		private NetworkInterface fromNode;
		private NetworkInterface toNode;
		private IList<IList<NetworkInterface>> unblockedRoutes;
		private IList<IList<NetworkInterface>> blockedRoutes;
		private Stack<NetworkInterface> routeStack;
		private HashSet<NetworkInterface> visitedNodes;


		#region Constructors
		/// <summary>
		/// Finds all possible paths between two nodes.
		/// Paths may be blocked or unblocked, and are sorted by length.
		/// </summary>
		/// <param name="graph">Graph to map paths of.</param>
		/// <param name="fromNode">Source node.</param>
		/// <param name="toNode">Destination node.</param>
		public NetworkRoute(IGraph<NetworkInterface> graph, NetworkInterface fromNode, NetworkInterface toNode)
		{
			this.graph = graph;
			this.fromNode = fromNode;
			this.toNode = toNode;
			this.unblockedRoutes = new List<IList<NetworkInterface>>();
			this.blockedRoutes = new List<IList<NetworkInterface>>();
			this.visitedNodes = new HashSet<NetworkInterface>();
			this.routeStack = new Stack<NetworkInterface>();

			FindRoute(this.fromNode);
			((List<IList<NetworkInterface>>)this.unblockedRoutes).Sort((a, b) => a.Count - b.Count);
			((List<IList<NetworkInterface>>)this.blockedRoutes).Sort((a, b) => a.Count - b.Count);
		}
		#endregion


		#region Properties
		/// <summary>
		/// All possible paths that are "blocked," sorted by length.
		/// </summary>
		public IList<IList<NetworkInterface>> BlockedRoutes
		{
			get { return this.blockedRoutes; }
		}


		/// <summary>
		/// Node all paths start from.
		/// </summary>
		public NetworkInterface FromNode
		{
			get { return this.fromNode; }
		}


		/// <summary>
		/// Node all paths end at.
		/// </summary>
		public NetworkInterface ToNode
		{
			get { return this.toNode; }
		}


		/// <summary>
		/// All possible paths that are "unblocked," sorted by length.
		/// </summary>
		public IList<IList<NetworkInterface>> UnblockedRoutes
		{
			get { return this.unblockedRoutes; }
		}
		#endregion


		public IList<NetworkInterface> GetShortest()
		{
			if (this.UnblockedRoutes.Count > 0)
				return this.UnblockedRoutes[0];

			return null;
		}

		
		// Recursive pathfinding algorithm.
		// Completed paths are stored via RecordCurrentPath().
		private void FindRoute(NetworkInterface currentNode)
		{
			this.routeStack.Push(currentNode);
			this.visitedNodes.Add(currentNode);

			if (currentNode == this.toNode)
			{
				RecordCurrentPath();
			}

			else
			{
				IList<IGraphNodeConnection<NetworkInterface>> connections = this.graph.GetConnections(currentNode);
				foreach (IGraphNodeConnection<NetworkInterface> connection in connections)
				{
					if (!this.visitedNodes.Contains(connection.Destination))
						FindRoute(connection.Destination);
				}
			}

			this.routeStack.Pop();
			this.visitedNodes.Remove(currentNode);
		}


		// Sorts the current path into blocked or unblocked paths lists.
		private void RecordCurrentPath()
		{
			List<NetworkInterface> reversePath = new List<NetworkInterface>();

			reversePath = this.routeStack.ToList();
			reversePath.Reverse();

			bool isBlocked = false;

			for (int i = 0; i < reversePath.Count; i++)
			{
				if (i == reversePath.Count - 1)
					break;

				NetworkInterface currentNode = reversePath[i];
				NetworkInterface nextNode = reversePath[i + 1];

				IGraphNodeConnection<NetworkInterface> path =
					this.graph.GetConnections(currentNode).FirstOrDefault(connection => connection.Destination == nextNode);

				if (path.Equals(default(NetworkInterface)))
					throw new NullReferenceException();

				if (!path.CanTraverse())
				{
					isBlocked = true;
					break;
				}
			}

			if (isBlocked)
				this.blockedRoutes.Add(reversePath);
			else
				this.unblockedRoutes.Add(reversePath);
		}
	}
}