namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;


	[Serializable]
	public class ComputerComponent : IEntityComponent
	{
		// TODO: Temporary, needs to wrap a procedural seed field.
		public string Identity { get; } = "Random";

		public long IpAddress;
		public byte Ram;

		public List<Port> OpenPorts = new List<Port>();
		public Dictionary<string, Process> RunningProcesses = new Dictionary<string, Process>();
		public List<long> Inbox = new List<long>();
		public List<long> Outbox = new List<long>();


		#region Properties
		public byte UsedRam
		{
			get
			{
				byte usedRam = 0;
				foreach (Process process in this.RunningProcesses.Values)
				{
					usedRam += process.Ram;
				}

				return usedRam;
			}
		}
		#endregion


		public bool AddProcess(Process process)
		{
			if (this.RunningProcesses.ContainsKey(process.Command))
				return false;
			
			this.RunningProcesses.Add(process.Command, process);
			return true;
		}


		public List<Email> GetInboxMessages()
		{
			List<Email> emails = new List<Email>();
			foreach (long id in this.Inbox)
			{
				emails.Add(Email.Index[id]);
			}

			return emails;
		}


		public List<Email> GetOutboxMessages()
		{
			List<Email> emails = new List<Email>();
			foreach (long id in this.Outbox)
			{
				emails.Add(Email.Index[id]);
			}

			return emails;
		}


		public bool RemoveProcess(Process process)
		{
			return RemoveProcess(process.Command);
		}


		public bool RemoveProcess(string processCommand)
		{
			if (this.RunningProcesses.ContainsKey(processCommand))
				return false;
			
			this.RunningProcesses.Remove(processCommand);
			return true;
		}
	}
}