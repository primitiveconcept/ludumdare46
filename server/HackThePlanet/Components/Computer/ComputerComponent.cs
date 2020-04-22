namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;


	[Serializable]
	public class ComputerComponent : IEntityComponent
	{
		public long IpAddress;
		public ushort Ram; // Measured in megabytes.
		
		
		public List<Port> OpenPorts = new List<Port>();
		public List<long> Inbox = new List<long>();
		public List<long> Outbox = new List<long>();
		public List<ComponentReference<IProcess>> RunningProcesses = new List<ComponentReference<IProcess>>();


		#region Properties
		// TODO: Temporary, needs to wrap a procedural seed field.
		public string Identity { get; } = "Random";


		public ushort UsedRam
		{
			get
			{
				ushort usedRam = 0;
				foreach (ComponentReference<IProcess> process in this.RunningProcesses)
				{
					usedRam += process.Component.RamUse;
				}

				return usedRam;
			}
		}
		#endregion


		public bool AddProcess(IProcess process)
		{
			ComponentReference<IProcess> componentReference = new ComponentReference<IProcess>(process);
			return AddProcess(componentReference);
		}


		public bool AddProcess(ComponentReference<IProcess> process)
		{
			if (this.RunningProcesses.Contains(process))
				return false;
			
			this.RunningProcesses.Add(process);
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


		public bool RemoveProcess(IProcess process)
		{
			ComponentReference<IProcess> componentReference = new ComponentReference<IProcess>(process);
			return RemoveProcess(componentReference);
		}


		public bool RemoveProcess(ComponentReference<IProcess> process)
		{
			if (!this.RunningProcesses.Contains(process))
				return false;
			
			this.RunningProcesses.Remove(process);
			return true;
		}
	}
}