namespace Server.Excpetions
{
	public class ServerException : Exception
	{
		public ServerException(string message) : base(message) { }
	}
}
