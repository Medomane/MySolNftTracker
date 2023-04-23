using MyLibrary;
using System.Reflection;
using System.Web.Mvc;

namespace MySolNftTracker.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Index()
        {
            _app.Init(Assembly.GetExecutingAssembly());
            return Json(new Notification(_app.Name, "success", "success"), JsonRequestBehavior.AllowGet);
        }
    }
}
