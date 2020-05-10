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
		private List<List<NetworkInterface>> routes;
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
		public NetworkRoute(
			IGraph<NetworkInterface> graph, 
			NetworkInterface fromNode, 
			NetworkInterface toNode)
		{
			this.graph = graph;
			this.fromNode = fromNode;
			this.toNode = toNode;
			this.routes = new List<List<NetworkInterface>>();
			this.visitedNodes = new HashSet<NetworkInterface>();
			this.routeStack = new Stack<NetworkInterface>();

			FindRoute(this.fromNode);
			this.routes.Sort(
				(a, b) => a.Count - b.Count);
		}


		/// <summary>
		/// Create a direct route between two nodes.
		/// </summary>
		/// <param name="fromNode"></param>
		/// <param name="toNode"></param>
		public NetworkRoute(NetworkInterface fromNode, NetworkInterface toNode)
		{
			this.graph = null;
			this.fromNode = fromNode;
			this.toNode = toNode;
			this.routes = new List<List<NetworkInterface>> 
							{ 
								new List<NetworkInterface> { fromNode, toNode } 
							};
		}


		/// <summary>
		/// Create a direct route between two nodes.
		/// </summary>
		/// <param name="fromNode"></param>
		/// <param name="toNode"></param>
		public NetworkRoute(NetworkInterface loopbackNode)
		{
			this.graph = null;
			this.fromNode = loopbackNode;
			this.toNode = loopbackNode;
			this.routes = new List<List<NetworkInterface>> 
							{ 
								new List<NetworkInterface> { loopbackNode } 
							};
		}
		#endregion


		#region Properties
		/// <summary>
		/// Node all paths start from.
		/// </summary>
		public NetworkInterface FromNode
		{
			get { return this.fromNode; }
		}


		/// <summary>
		/// All possible paths that are "unblocked," sorted by length.
		/// </summary>
		public List<List<NetworkInterface>> Routes
		{
			get { return this.routes; }
		}


		/// <summary>
		/// Return the shortest found route.
		/// </summary>
		public List<NetworkInterface> Shortest
		{
			get
			{
				if (this.Routes != null
					&& this.Routes.Count > 0)
				{
					return this.Routes[0];
				}
				return null;	
			}
		}


		/// <summary>
		/// Node all paths end at.
		/// </summary>
		public NetworkInterface ToNode
		{
			get { return this.toNode; }
		}
		#endregion


		#region Operators
		public static implicit operator List<NetworkInterface>(NetworkRoute networkRoute)
		{
			return networkRoute != null 
						? networkRoute.Shortest 
						: null;
		}
		#endregion


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

				if (path == null)
					continue;

				if (!path.CanTraverse())
				{
					isBlocked = true;
					break;
				}
			}

			if (!isBlocked)
				this.routes.Add(reversePath);
		}
	}
}