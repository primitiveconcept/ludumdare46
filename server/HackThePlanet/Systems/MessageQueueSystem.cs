namespace HackThePlanet.Systems
{
    using System;
    using PrimitiveEngine;


    [EntitySystem(UpdateType = UpdateType.FixedUpdate, Layer = 99)]
    public class MessageQueueSystem : EntityComponentProcessingSystem<PlayerComponent>
    {
        public override void Process(Entity entity, PlayerComponent playerComponent)
        {
            if (playerComponent.MessageQueue.Count == 0)
                return;

            foreach (string message in playerComponent.MessageQueue)
            {
                playerComponent.Session.Send(message);
            }
            playerComponent.MessageQueue.Clear();
        }
    }
}