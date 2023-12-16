using Server.Excpetions;

namespace Domain.Shared
{
	public class ResponseDTO<T>
	{
		public T? Data { get; set; }
		public bool IsSuccess { get; set; }
		public string? Error { get; set; }

        public ResponseDTO(){}

        public ResponseDTO(T? data = default)
		{
			Data = data;
			IsSuccess = true;
		}

		public ResponseDTO(ServerException error) 
		{
			Error = error.Message;
			IsSuccess = false;
		}
	}

	public class ResponseDTO : ResponseDTO<int>
	{
		public ResponseDTO(): base(1) { }
		public ResponseDTO(ServerException error): base(error) { }
	}
}
