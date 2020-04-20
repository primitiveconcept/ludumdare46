namespace HackThePlanet
{
    using System.Collections.Generic;
    using System.Linq;
    using PrimitiveEngine;


    public class Email
    {
        public static Dictionary<long, Email> Index = new Dictionary<long, Email>();

        public long Id = -1;
        public string From;
        public string To;
        public string Subject;
        public string Body;


        public static Email Send(
            ComputerComponent fromComputer, 
            ComputerComponent toComputer, 
            string subject, 
            string body)
        {
            Email email = new Email();

            long id = Index.Count > 0
                          ? Index.Last().Key + 1
                          : 0;

            email.Id = id;
            email.From = fromComputer.Identity;
            email.To = toComputer.Identity;
            email.Subject = subject;
            email.Body = body;
            
            fromComputer.Outbox.Add(id);
            toComputer.Inbox.Add(id);

            PlayerComponent playerRecipient = toComputer.GetEntity().GetComponent<PlayerComponent>();
            if (playerRecipient != null
                && playerRecipient.Session != null)
            {
                // TODO: Trigger and update message to an active player.
            }

            return email;
        }


        // TODO: Convenience properties to get sender and recipient names via entity IDs.
    }
}