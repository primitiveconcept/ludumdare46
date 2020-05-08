namespace HackThePlanet
{
    using System.Collections;
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class ProcessPool<T> : 
        IEntityComponent,
        IEnumerable<T>
        where T: class, IApplication, new()
    {
        private List<T> processes = new List<T>();


        #region Properties
        public T this[int index]
        {
            get { return this.processes[index]; }
        }
        #endregion


        public static T RunApplication(ComputerComponent computer)
        {
            Entity computerEntity = computer.GetEntity();
            ProcessPool<T> processPool = computerEntity.GetComponent<ProcessPool<T>>();
            if (processPool == null)
            {
                processPool = new ProcessPool<T>();
                computerEntity.AddComponent(processPool);
            }

            T applicationInstance = new T();
            ushort pid = computer.GetFreeProcessId();
            applicationInstance.OriginEntityId = computerEntity.Id;
            applicationInstance.ProcessId = pid;
            
            processPool.processes.Add(applicationInstance);
            computer.RunningApplications.Add(pid, applicationInstance);
            
            return applicationInstance;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.processes.Count; i++)
            {
                yield return this.processes[i];
            }
        }


        public bool KillProcess(ushort processId)
        {
            foreach (T process in this.processes)
            {
                if (process.ProcessId != processId)
                    continue;
                this.processes.Remove(process);
                
                if (this.processes.Count == 0)
                    this.GetEntity().RemoveComponent<ProcessPool<T>>();
                
                return true;
            }

            return false;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}