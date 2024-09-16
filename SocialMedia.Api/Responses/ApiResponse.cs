namespace SocialMedia.Api.Responses
{
	// T es para mandarle el tipo de data que va a recibir
	public class ApiResponse<T>
	{
		public ApiResponse(T data) 
		{
			Data = data;
		}

		public T Data { get; set; }
	}
}
