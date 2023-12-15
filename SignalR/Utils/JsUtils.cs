using Microsoft.JSInterop;

namespace SignalR.Utils
{
	public class JsUtils : IJsUtils
	{
		private readonly IJSRuntime _jSRuntime;

		public JsUtils(IJSRuntime jSRuntime)
		{
			_jSRuntime = jSRuntime;
		}

		public async Task Alert(string message)
		{
			await _jSRuntime.InvokeAsync<string>("alert", message);
		}
	}
}
