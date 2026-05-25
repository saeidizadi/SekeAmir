using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Contracts.Users;
using Domain.Account.Permission;
using Persistence.Extention;

namespace SekeAmir.Web.Areas.Admin.Component.Menu
{
    [Authorize]
    public class FillMenu : ViewComponent
    {
        private IPermisionList _permisionList;
        private IUser _user;

        public FillMenu(IPermisionList permisionList, IUser user)
        {
            _permisionList = permisionList;
            _user = user;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            IEnumerable<PermissionList> menus = new List<PermissionList>();
            if (HttpContext.Session.GetString("menus") == null)
            {

                var Identity = HttpContext.User.GetUserId();
                menus =await _permisionList.UserMenuAsync(Identity);
                HttpContext.Session.SetData("menus", menus);
            }
            else
            {
                menus = HttpContext.Session.GetData<List<PermissionList>>("menus");
            }



            return View("/Areas/Admin/Component/Menu/MyFillMenu.cshtml", menus);
        }
    }
}
