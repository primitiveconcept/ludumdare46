namespace HackThePlanet.Host
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;


    public class GameService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.Out.WriteLine("GameService task is starting.");
            
                Game.Internet.Seed();
                
                stoppingToken.Register(() => Console.Out.WriteLine("GameService task is stopping."));

                while (!stoppingToken.IsCancellationRequested)
                {
                    Game.Update();
                    Game.Time.Update();
                    await Task.Delay(30, stoppingToken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}