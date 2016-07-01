//using DevExpress.Charts.Model;
using DevExpress.Web.Mvc;
using DXWebApplication5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXWebApplication5.Controllers {
    public class HomeController : Controller {
        CustomerList clist = new CustomerList();
        public ActionResult Index()
        {
            Session["WebHotelInvoices"] = clist.GetTypedListModel();
            return View(clist.GetTypedListModel());
        }
        public ActionResult GridViewPartial() {
            return PartialView(Session["WebHotelInvoices"]);
        }
        public ActionResult CustomCallbackFromGrid(string flag) {
            ViewData["flag"] = flag;
           // return PartialView("GridViewPartial", clist.GetTypedListModel());
            return PartialView("GridViewPartial", Session["WebHotelInvoices"]);
        }
    }
}
