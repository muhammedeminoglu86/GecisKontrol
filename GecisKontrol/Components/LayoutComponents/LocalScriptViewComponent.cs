using Microsoft.AspNetCore.Mvc;

namespace GecisKontrol.Components.LayoutComponents
{
	public class LocalScriptViewComponent:ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			return View("LocalScript");
		}
	}
}
