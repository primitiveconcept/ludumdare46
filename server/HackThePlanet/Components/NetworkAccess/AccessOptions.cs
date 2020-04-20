namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;


    [Serializable]
    public class AccessOptions
    {
        public Dictionary<Port, AccessLevel> PortAccessability = new Dictionary<Port, AccessLevel>();


        public List<string> GetAccessOptions(string ip)
        {
            List<string> commands = new List<string>();
            if (this.PortAccessability.Count == 0)
            {
                commands.Add($"[portscan](portscan|{ip})");
                return commands;
            }

            foreach (var entry in this.PortAccessability)
            {
                switch (entry.Key)
                {
                    case Port.Ssh:
                        switch (entry.Value)
                        {
                            case AccessLevel.Known:
                                commands.Add($"[sshcrack](sshcrack|{ip})");
                                break;
                            case AccessLevel.User:
                                // TODO
                                break;
                            case AccessLevel.Root:
                                // TODO
                                break;
                        }
                        break;
                    
                    default:
                        switch (entry.Value)
                        {
                            default:
                                // TODO
                                break;
                        }
                        break;
                }
            }

            return commands;
        }
    }
}