﻿using Server.Excpetions;

namespace Domain.Shared
{
	public class ResponseDTO {
		public bool IsSuccess { get; set; }
		public ServerException? Error { get; set; }

		public ResponseDTO(ServerException error)
		{
			Error = error;
			IsSuccess = false;
		}
	}

	public class ResponseDTO<T>
	{
		public T? Data { get; set; }
		public bool IsSuccess { get; set; }
		public ServerException? Error { get; set; }


		public ResponseDTO(T? data = default)
		{
			Data = data;
			IsSuccess = true;
		}

		public ResponseDTO(ServerException error) 
		{
			Error = error;
			IsSuccess = false;
		}
	}
}
