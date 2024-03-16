using Microsoft.AspNetCore.Mvc;

namespace GecisKontrol.Components.LayoutComponents
{
	public class ThemeScriptViewComponent:ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			return View("ThemeScript");
		}
	}
}
