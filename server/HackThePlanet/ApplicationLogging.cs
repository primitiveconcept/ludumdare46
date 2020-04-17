namespace HackThePlanet
{
	using Microsoft.Extensions.Logging;


	public class ApplicationLogging
	{
		private static ILoggerFactory _loggerFactory;


		#region Properties
		public static ILoggerFactory LoggerFactory
		{
			get
			{
				if (_loggerFactory == null)
				{
					_loggerFactory = new LoggerFactory();
				}

				return _loggerFactory;
			}
		}
		#endregion


		public static ILogger CreateLogger<T>()
		{
			return LoggerFactory.CreateLogger<T>();
		}
	}
}