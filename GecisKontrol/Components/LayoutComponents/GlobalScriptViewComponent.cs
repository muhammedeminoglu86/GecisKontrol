using Microsoft.AspNetCore.Mvc;

namespace GecisKontrol.Components.LayoutComponents
{
	public class GlobalScriptViewComponent:ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			return View("GlobalScript");
		}
	}
}
