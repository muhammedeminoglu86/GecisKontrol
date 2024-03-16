using Microsoft.AspNetCore.Mvc;

namespace GecisKontrol.Components.LayoutComponents
{
	public class HeadViewComponent:ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			return View("Head");
		}
	}
}
