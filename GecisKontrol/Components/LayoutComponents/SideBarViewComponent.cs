using Microsoft.AspNetCore.Mvc;

namespace GecisKontrol.Components.LayoutComponents
{
	public class SideBarViewComponent: ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			return View("SideBar");
		}
	}
}
