using MyLibrary;
using System.Web.Http;

namespace MySolNftTracker.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public Notification Get() => new Notification(Models.AppUser.Current, "success");
    }
}