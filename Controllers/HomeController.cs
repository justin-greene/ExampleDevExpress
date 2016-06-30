﻿//using DevExpress.Charts.Model;
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
        public ActionResult Index() {
            return View(clist.GetTypedListModel());
        }
        public ActionResult GridViewPartial() {
            return PartialView(clist.GetTypedListModel());
        }
        public ActionResult CustomCallbackFromGrid(string flag) {
            ViewData["flag"] = flag;
            return PartialView("GridViewPartial", clist.GetTypedListModel());
        }
    }
}