using Microsoft.JSInterop;

namespace SignalR.Utils
{
	public class UserUtils
	{
		public UserUtils(IJSRuntime jSRuntime)
		{
			JSRuntime = jSRuntime;
		}

		public IJSRuntime JSRuntime { get; }

		public async Task<string> GetNameOrLogin()
		{
			string name = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "name");

			if (String.IsNullOrEmpty(name))
			{
				name = await JSRuntime.InvokeAsync<string>("prompt", "Enter your nickname:");
				await JSRuntime.InvokeAsync<string>("localStorage.setItem", "name", name);
			}

			return name;
		}
	}
}
