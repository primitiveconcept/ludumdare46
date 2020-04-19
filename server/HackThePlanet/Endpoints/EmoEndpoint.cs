namespace HackThePlanet
{
	using WebSocketSharp;
	using WebSocketSharp.Server;


	public class EmoEndpoint : WebSocketBehavior
	{
		private int iteration = 0;

		private string[] lyrics = new[]
									{
										"Crawling in my skin",
										"These wounds, they will not heal",
										"Fear is how I fall",
										"Confusing what is real",
										"There's something inside me that pulls beneath the surface",
										"Consuming, confusing",
										"This lack of self control I fear is never ending",
										"Controlling",
										"I can't seem",
										"To find myself again",
										"My walls are closing in",
										"(Without a sense of confidence I'm convinced",
										"That there's just too much pressure to take)",
										"I've felt this way before",
										"So insecure",
										"Crawling in my skin",
										"These wounds, they will not heal",
										"Fear is how I fall",
										"Confusing what is real",
										"Discomfort, endlessly has pulled itself upon me",
										"Distracting, reacting",
										"Against my will I stand beside my own reflection",
										"It's haunting how I can't seem",
										"To find myself again",
										"My walls are closing in",
										"(Without a sense of confidence I'm convinced",
										"That there's just too much pressure to take)",
										"I've felt this way before",
										"So insecure",
										"Crawling in my skin",
										"These wounds, they will not heal",
										"Fear is how I fall",
										"Confusing what is real",
										"Crawling in my skin",
										"These wounds, they will not heal",
										"Fear is how I fall",
										"Confusing, confusing what is real",
										"There's something inside me that pulls beneath the surface",
										"Consuming (confusing what is real)",
										"This lack of self control I fear is never ending",
										"Controlling (confusing what is real)"
									};
		
		protected override void OnMessage(MessageEventArgs message)
		{
			string lyric = lyrics[this.iteration];
			this.iteration = this.iteration == this.lyrics.Length - 1
								? 0
								: this.iteration + 1;
			Send(lyric);
		}
	}
}