using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DevExpress.Data;
using DevExpress.Web.Mvc;
using DXWebApplication5.Models;


namespace DXWebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Show(object hotelinvoiceid)
        {
            return null;
        }

        public ActionResult Edit(object id)
        {
            return null;
        }

        public ActionResult Index()
        {
            try
            {
                var today = DateTime.Today;

                //We want to make sure that we don't have any trouble with the dates in sql, so this will ensure that the maximum date is 12/31/9999 (the max date in sql) and that the min date is 1/1/1990 (Earlier than anythin in Genesis)
                var begindate =  new DateTime(today.Year, today.Month, 1);
                var enddate = today;


                DataTable dt = GetData("spHotelInvoiceGetByFilter", begindate, enddate);

                Session["WebHotelInvoices"] = dt;

                ViewData["BeginDate"] = ((DateTime)begindate).ToString("MM/dd/yyy");
                ViewData["EndDate"] = ((DateTime)enddate).ToString("MM/dd/yyy");

                return View("Index");
            }
            catch (Exception ex)
            {
                //if (!CygHelper.SendErrorMail(ex)) throw;
                TempData["Error"] = "We ran into an error while retrieving your invoices and have been notified about the problem.  Please try again later.";
                return RedirectToAction("Index");
            }
        }

        private DataTable GetData(string v, DateTime? begindate, DateTime? enddate)
        {
            var table = new DataTable("Invoices");
            table.Columns.Add("HotelName", typeof(string));
            table.Columns.Add("ChainName", typeof(string));
            table.Columns.Add("BrandName", typeof(string));
            table.Columns.Add("ManagementCompanyName", typeof(string));
            table.Columns.Add("WebHotelInvoiceId", typeof(int));
            table.Columns.Add("HotelInvoiceKey", typeof(int));
            table.Columns.Add("HotelInvoiceNumber", typeof(string));
            table.Columns.Add("InvoiceDate", typeof(DateTime));
            table.Columns.Add("Nights", typeof(int));
            table.Columns.Add("InvoiceTotal", typeof(decimal));
            table.Columns.Add("Billing", typeof(string));
            table.Columns.Add("AmountPaid", typeof(decimal));
            table.Columns.Add("PaymentDate", typeof(DateTime));
            table.Columns.Add("CheckNumber", typeof(string));
            table.Columns.Add("CardNumber", typeof(string));
            table.Columns.Add("CardTransactionDate", typeof(DateTime));
            table.Columns.Add("RejectionReason", typeof(string));
            table.Columns.Add("ApprovalDate", typeof(DateTime));
            table.Columns.Add("CompletionDate", typeof(DateTime));
            table.Columns.Add("FirstNight", typeof(DateTime));
            table.Columns.Add("LastNight", typeof(DateTime));
            table.Columns.Add("WebHotelInvoiceIsEditable", typeof(bool));
            table.Columns.Add("PriorityPayDaysToPay", typeof(int));
            table.Columns.Add("CreationDate", typeof(DateTime));
            table.Columns.Add("HasReservationVCards", typeof(bool));
            table.Columns.Add("Filter", typeof(string));

            table.Rows.Add("Value Place - Abilene, TX", "Value Place", "Value Place", "(Independent)", 62458, 2049790, "CLS-SHC0", "01/20/2016", 35, 1461.51, "Direct Bill to Comdata VCard", 1461.51, null, "", null, "01/30/2016", null, null, null, "12/23/2015", "01/09/2016", false, 7, "01/20/2016", false, "Charged");
            table.Rows.Add("Value Place - Abilene, TX", "Value Place", "Value Place", "(Independent)", 67595, 2076072, "AbileneTX022116", "02/21/2016", 28, 963.54, "Direct Bill to Comdata VCard", 963.54, null, "", null, "03/02/2016", null, null, null, "01/05/2016", "02/19/2016", false, 7, "02/21/2016", true, "Charged");
            table.Rows.Add("Value Place - Abilene, TX", "Value Place", "Value Place", "(Independent)", 72094, 2099509, "AbileneTX031916", "03/19/2016", 2, 109.35, "Direct Bill to Comdata VCard", 109.35, null, "", null, "03/31/2016", null, null, null, "03/01/2016", "03/02/2016", false, 7, "03/19/2016", false, "Charged");
            table.Rows.Add("Value Place - Amarillo, TX", "Value Place", "Value Place", null, null, 2136711, "18280", "04/07/2016", 8, 394.32, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "03/30/2016", "04/06/2016", false, 7, "05/05/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Amarillo, TX", "Value Place", "Value Place", null, null, 2151562, "18353 4/8-5/15", "05/16/2016", 38, 2012.72, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "04/08/2016", "05/15/2016", false, 7, "05/23/2016", true, "CC Ready");
            table.Rows.Add("Value Place - Amarillo, TX", "Value Place", "Value Place", null, null, 2151571, "18353 5/16-5/22", "05/23/2016", 7, 300.02, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "05/16/2016", "05/22/2016", false, 7, "05/23/2016", true, "CC Ready");
            table.Rows.Add("Value Place - Apex, NC", "Value Place", "Value Place", null, 62106, 2051106, "CLS-Luk", "01/21/2016", 7, 349.37, "Direct Bill to Comdata VCard", 349.37, null, "", null, "02/02/2016", null, null, null, "01/09/2016", "01/15/2016", false, 7, "01/21/2016", false, "Charged");
            table.Rows.Add("Value Place - Duncan, SC", "Value Place", "Value Place", "(Independent)", null, 2133177, "31927", "04/10/2016", 7, 363.02, "Direct Bill to Comdata VCard", 0, null, "", null, "05/09/2016", null, null, null, "04/03/2016", "04/09/2016", false, 7, "05/02/2016", true, "Charged");
            table.Rows.Add("Value Place - Duncan, SC", "Value Place", "Value Place", "(Independent)", null, 2133185, "31926", "04/10/2016", 7, 363.02, "Direct Bill to Comdata VCard", 0, null, "", null, "05/09/2016", null, null, null, "04/03/2016", "04/09/2016", false, 7, "05/02/2016", true, "Charged");
            table.Rows.Add("Value Place - Duncan, SC", "Value Place", "Value Place", "(Independent)", null, 2154812, "COMD05/25/16-209", "04/30/2016", 6, 311.16, "Comdata VCard Credit Card", 311.16, "05/25/2016", "", null, null, null, null, null, "04/24/2016", "04/29/2016", false, 7, "05/25/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place - Duncan, SC", "Value Place", "Value Place", "(Independent)", null, 2154820, "COMD05/25/16-210", "04/30/2016", 6, 311.12, "Comdata VCard Credit Card", 311.12, "05/25/2016", "", null, null, null, null, null, "04/24/2016", "04/29/2016", false, 7, "05/25/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place - Duncan, SC", "Value Place", "Value Place", "(Independent)", 67630, 2076103, "DUNCANSC022116", "02/21/2016", 74, 2835.36, "Direct Bill to Comdata VCard", 2835.36, null, "", null, "03/12/2016", null, null, null, "01/15/2016", "02/20/2016", false, 7, "02/21/2016", false, "Charged");
            table.Rows.Add("Value Place - Florence, KY", "Value Place", "Value Place", "WoodSpring Property Management", 76033, 2116663, "2859/2860/2861", "04/12/2016", 27, 1212.3, "Direct Bill to Comdata VCard", 1212.3, null, "", null, "04/20/2016", null, null, null, "03/28/2016", "04/05/2016", false, 7, "04/12/2016", false, "Charged");
            table.Rows.Add("Value Place - Florence, KY", "Value Place", "Value Place", "WoodSpring Property Management", 83295, 2149499, "2862/2863/2864", "05/20/2016", 102, 4458.51, "Direct Bill to Comdata VCard", 4458.51, null, "", null, null, null, null, null, "04/06/2016", "05/09/2016", false, 7, "05/20/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", null, 2096537, "12401", "03/16/2016", 21, 1036.14, "Direct Bill to Comdata VCard", 1036.14, null, "", null, "04/01/2016", null, null, null, "02/24/2016", "03/15/2016", false, 7, "03/16/2016", false, "Charged");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", 72095, 2099510, "GainesvilleVA031916", "03/19/2016", 11, 536.5, "Direct Bill to Comdata VCard", 536.5, null, "", null, "03/29/2016", null, null, null, "03/14/2016", "03/18/2016", false, 7, "03/19/2016", false, "Charged");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", 76806, 2119938, "1071/1073", "04/15/2016", 54, 2643.3, "Direct Bill to Comdata VCard", 2643.3, null, "", null, "04/27/2016", null, null, null, "03/19/2016", "04/14/2016", false, 7, "04/15/2016", false, "Charged");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", 78273, 2126813, "1073 / 1071", "04/22/2016", 14, 685.3, "Direct Bill to Comdata VCard", 685.3, null, "", null, "05/02/2016", null, null, null, "04/15/2016", "04/21/2016", false, 7, "04/22/2016", false, "Charged");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", 80442, 2135860, "1104/1114d", "05/04/2016", 6, 295.26, "Direct Bill to Comdata VCard", 295.26, null, "", null, "05/12/2016", null, null, null, "04/29/2016", "05/03/2016", false, 7, "05/04/2016", false, "Charged");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", 81343, 2138528, "Dunn", "05/10/2016", 6, 296.04, "Direct Bill to Comdata VCard", 296.04, null, "", null, "05/19/2016", null, null, null, "05/04/2016", "05/09/2016", false, 7, "05/10/2016", false, "Charged");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", 83299, 2149510, "1121", "05/20/2016", 10, 493.4, "Direct Bill to Comdata VCard", 493.4, null, "", null, null, null, null, null, "05/10/2016", "05/19/2016", false, 7, "05/20/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Gainesville, VA", "Value Place", "Value Place", "WoodSpring Property Management", 83766, 2151582, "1121Dunn", "05/23/2016", 3, 148.02, "Direct Bill to Comdata VCard", 148.02, null, "", null, null, null, null, null, "05/20/2016", "05/22/2016", false, 7, "05/23/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2097944, "29942", "03/02/2016", 7, 245.3, "Direct Bill to Comdata VCard", 245.3, null, "", null, null, null, null, null, "02/24/2016", "03/01/2016", false, 7, "03/17/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2097947, "29942 3/2", "03/03/2016", 1, 35.05, "Direct Bill to Comdata VCard", 35.05, null, "", null, null, null, null, null, "03/02/2016", "03/02/2016", false, 7, "03/17/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2097949, "29942 3/3", "03/04/2016", 1, 35.05, "Direct Bill to Comdata VCard", 35.05, null, "", null, null, null, null, null, "03/03/2016", "03/03/2016", false, 7, "03/17/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2097951, "29942 3/4", "03/05/2016", 1, 35.05, "Direct Bill to Comdata VCard", 35.05, null, "", null, null, null, null, null, "03/04/2016", "03/04/2016", false, 7, "03/17/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2147884, "30063 3/19-4/13", "04/14/2016", 26, 1156.48, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "03/19/2016", "04/13/2016", false, 7, "05/19/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2147886, "30094 3/19-4/13", "04/14/2016", 26, 1156.48, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "03/19/2016", "04/13/2016", false, 7, "05/19/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2151391, "29915 3/19-5/20", "05/21/2016", 63, 1578.33, "Direct Bill to Comdata VCard", 0, null, "", null, "05/28/2016", null, null, null, "03/19/2016", "05/20/2016", false, 7, "05/23/2016", false, "Charged");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2151434, "29915 5/21-22", "05/23/2016", 2, 53.14, "Direct Bill to Comdata VCard", 0, null, "", null, "05/28/2016", null, null, null, "05/21/2016", "05/22/2016", false, 7, "05/23/2016", false, "Charged");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2151449, "30052 3/19-5/22", "05/23/2016", 65, 2891.2, "Direct Bill to Comdata VCard", 0, null, "", null, "05/28/2016", null, null, null, "03/19/2016", "05/22/2016", false, 7, "05/23/2016", false, "Charged");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2151491, "40332 4/9-5/22", "05/23/2016", 44, 1957.12, "Direct Bill to Comdata VCard", 1957.12, "05/23/2016", "", null, "05/28/2016", null, null, null, "04/09/2016", "05/22/2016", false, 7, "05/23/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, null, 2154069, "40451", "04/21/2016", 3, 144.81, "Direct Bill to CLS", 0, null, "", null, null, null, null, null, "04/18/2016", "04/20/2016", false, 7, "05/25/2016", true, "Invoice Accepted: Processing");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, 67602, 2076078, "JohnsonCityTN022116", "02/21/2016", 30, 1349.48, "Direct Bill to Comdata VCard", 1349.48, null, "", null, "03/04/2016", null, null, null, "01/31/2016", "02/18/2016", false, 7, "02/21/2016", false, "Charged");
            table.Rows.Add("Value Place - Johnson City, TN", "Value Place", "Value Place", null, 72096, 2099511, "JohnsonCityTN031916", "03/19/2016", 74, 2682.32, "Direct Bill to Comdata VCard", 2682.32, null, "", null, null, null, null, null, "02/21/2016", "03/18/2016", false, 7, "03/19/2016", false, "CC Ready");
            table.Rows.Add("Value Place - La Porte, TX", "Value Place", "Value Place", null, null, 2096571, "18481", "03/04/2016", 12, 509.16, "Direct Bill to Comdata VCard", 509.16, null, "", null, null, null, null, null, "02/21/2016", "03/03/2016", false, 7, "03/16/2016", false, "CC Ready");
            table.Rows.Add("Value Place - La Porte, TX", "Value Place", "Value Place", null, 67635, 2076108, "LaPorteTX022116", "02/21/2016", 33, 1648.5, "Direct Bill to Comdata VCard", 1648.5, null, "", null, "03/03/2016", null, null, null, "01/19/2016", "02/20/2016", false, 7, "02/21/2016", false, "Charged");
            table.Rows.Add("Value Place - La Porte, TX", "Value Place", "Value Place", null, 72097, 2099512, "LaPortetX031916", "03/19/2016", 15, 636.45, "Direct Bill to Comdata VCard", 636.45, null, "", null, "03/31/2016", null, null, null, "03/04/2016", "03/18/2016", false, 7, "03/19/2016", false, "Charged");
            table.Rows.Add("Value Place - Lebanon, TN", "Value Place", "Value Place", null, null, 2125969, "31565", "03/27/2016", 7, 371.98, "Direct Bill to Comdata VCard", 0, null, "", null, "04/27/2016", null, null, null, "03/20/2016", "03/26/2016", false, 7, "04/21/2016", false, "Charged");
            table.Rows.Add("Value Place - Lebanon, TN", "Value Place", "Value Place", null, null, 2125971, "31565 3/27-3/31", "04/01/2016", 5, 265.7, "Direct Bill to Comdata VCard", 0, null, "", null, "04/27/2016", null, null, null, "03/27/2016", "03/31/2016", false, 7, "04/21/2016", false, "Charged");
            table.Rows.Add("Value Place - Lexington, SC", "Value Place", "Value Place", null, null, 2098150, "18657", "02/29/2016", 8, 322.08, "Direct Bill to Comdata VCard", 322.08, null, "", null, null, null, null, null, "02/21/2016", "02/28/2016", false, 7, "03/17/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Lexington, SC", "Value Place", "Value Place", null, null, 2098151, "18657 2/29-3/4", "03/05/2016", 5, 201.3, "Direct Bill to Comdata VCard", 201.3, null, "", null, null, null, null, null, "02/29/2016", "03/04/2016", false, 7, "03/17/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Lexington, SC", "Value Place", "Value Place", null, 67650, 2076116, "LexingtonSC022116", "02/21/2016", 24, 975.6, "Direct Bill to Comdata VCard", 975.6, null, "", null, "03/03/2016", null, null, null, "01/28/2016", "02/20/2016", false, 7, "02/21/2016", false, "Charged");
            table.Rows.Add("Value Place - Lexington, SC", "Value Place", "Value Place", null, 72098, 2099513, "LexingtonSC031916", "03/19/2016", 5, 201.3, "Direct Bill to Comdata VCard", 201.3, null, "", null, "04/01/2016", null, null, null, "03/05/2016", "03/09/2016", false, 7, "03/19/2016", false, "Charged");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", null, 2098715, "NO SHOW WAIVED", "02/25/2016", 1, 0, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "02/24/2016", "02/24/2016", false, 7, "03/18/2016", false, "CC Not Ready");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", null, 2130987, "COMD03/28/16-100", "03/27/2016", 1, 0, "Comdata VCard Credit Card", 0, "04/28/2016", "", null, null, null, null, null, "03/26/2016", "03/26/2016", false, 7, "04/28/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", null, 2154714, "COMD03/29/16-206", "04/17/2016", 20, 1407.39, "Comdata VCard Credit Card", 1407.39, "05/25/2016", "", null, null, null, null, null, "03/27/2016", "04/15/2016", false, 7, "05/25/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", null, 2154742, "40701", "05/08/2016", 22, 1351.17, "Direct Bill to Comdata VCard", 1351.17, "06/01/2016", "", null, null, null, null, null, "04/16/2016", "05/07/2016", false, 7, "05/25/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", 76567, 2118974, "1711/1712/1713a", "04/14/2016", 21, 965.58, "Direct Bill to Comdata VCard", 965.58, "04/29/2016", "", null, "04/23/2016", null, null, null, "04/07/2016", "04/13/2016", false, 7, "04/14/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", 77084, 2120804, "1714", "04/18/2016", 8, 367.84, "Direct Bill to Comdata VCard", 367.84, "05/03/2016", "", null, "04/28/2016", null, null, null, "04/10/2016", "04/17/2016", false, 7, "04/18/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", 78285, 2126825, "1711/1712/1713", "04/22/2016", 24, 1183.2, "Direct Bill to Comdata VCard", 1183.2, "05/07/2016", "", null, null, null, null, null, "04/14/2016", "04/21/2016", false, 7, "04/22/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", 78552, 2128146, "1714B", "04/25/2016", 7, 345.1, "Direct Bill to Comdata VCard", 345.1, "05/10/2016", "", null, "05/06/2016", null, null, null, "04/18/2016", "04/24/2016", false, 7, "04/25/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", 80443, 2135881, "ROMERO/SANCHEZ", "05/04/2016", 30, 1462.9, "Direct Bill to Comdata VCard", 1462.9, "05/19/2016", "", null, "05/13/2016", null, null, null, "04/29/2016", "05/03/2016", false, 7, "05/04/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", 81344, 2138537, "5/4-5/9", "05/10/2016", 24, 1173.54, "Direct Bill to Comdata VCard", 1173.54, "05/25/2016", "", null, "05/19/2016", null, null, null, "05/04/2016", "05/09/2016", false, 7, "05/10/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place - Loveland, CO", "Value Place", "Value Place", "WoodSpring Property Management", 83338, 2149674, "4/7/16", "05/20/2016", 59, 2873.28, "Direct Bill to Comdata VCard", 2873.28, "05/27/2016", "", null, null, null, null, null, "05/04/2016", "05/19/2016", false, 7, "05/20/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place - Mentor, OH", "Value Place", "Value Place", "WoodSpring Property Management", 79003, 2129717, "850/851", "04/26/2016", 2, 153.72, "Direct Bill to Comdata VCard", 153.72, null, "", null, "05/05/2016", null, null, null, "04/25/2016", "04/25/2016", false, 7, "04/26/2016", false, "Charged");
            table.Rows.Add("Value Place - Midland, TX", "Value Place", "Value Place", null, 60542, 2037854, "clstmg", "01/05/2016", 42, 3047.38, "Direct Bill to Comdata VCard", 3047.38, null, "", null, null, null, null, null, "12/14/2015", "12/20/2015", false, 7, "01/05/2016", false, "CC Ready");
            table.Rows.Add("Value Place - Plainfield, IN", "Value Place", "Value Place", "WoodSpring Property Management", 79005, 2129727, "191/192/193", "04/26/2016", 3, 157.17, "Direct Bill to Comdata VCard", 157.17, null, "", null, "05/06/2016", null, null, null, "04/24/2016", "04/24/2016", false, 7, "04/26/2016", false, "Charged");
            table.Rows.Add("Value Place - Plainfield, IN", "Value Place", "Value Place", "WoodSpring Property Management", 80425, 2136237, "191/192/193A-1", "05/05/2016", 1, 45.49, "Direct Bill to Comdata VCard", 45.49, null, "", null, "05/13/2016", null, null, null, "05/03/2016", "05/03/2016", false, 7, "05/05/2016", false, "Charged");
            table.Rows.Add("Value Place - Plainfield, IN", "Value Place", "Value Place", "WoodSpring Property Management", 80426, 2136247, "191/192/193A-2", "05/05/2016", 1, 45.49, "Direct Bill to Comdata VCard", 45.49, null, "", null, "05/13/2016", null, null, null, "05/03/2016", "05/03/2016", false, 7, "05/05/2016", false, "Charged");
            table.Rows.Add("Value Place - Plainfield, IN", "Value Place", "Value Place", "WoodSpring Property Management", 80427, 2135911, "191/192/193A-3", "05/04/2016", 1, 45.49, "Direct Bill to Comdata VCard", 45.49, null, "", null, "05/12/2016", null, null, null, "05/03/2016", "05/03/2016", false, 7, "05/04/2016", false, "Charged");
            table.Rows.Add("Value Place - West Valley, UT", "Value Place", "Value Place", "WoodSpring Property Management", 67631, 2076104, "WestValleyUT022116", "02/21/2016", 35, 1325.16, "Direct Bill to Comdata VCard", 1325.16, null, "", null, "03/02/2016", null, null, null, "01/20/2016", "02/17/2016", false, 7, "02/21/2016", false, "Charged");
            table.Rows.Add("Value Place - West Valley, UT", "Value Place", "Value Place", "WoodSpring Property Management", 77363, 2121861, "10291", "04/19/2016", 31, 888.95, "Direct Bill to Comdata VCard", 888.95, null, "", null, "04/28/2016", null, null, null, "02/18/2016", "03/19/2016", false, 7, "04/19/2016", false, "Charged");
            table.Rows.Add("Value Place Columbia - Elgin, SC", "Value Place", "Value Place", null, null, 2147289, "27340", "04/14/2016", 26, 935.74, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "03/19/2016", "04/13/2016", false, 7, "05/18/2016", true, "CC Ready");
            table.Rows.Add("Value Place Columbia - Elgin, SC", "Value Place", "Value Place", null, 67600, 2076077, "ElginSC022116", "02/21/2016", 85, 3721.07, "Direct Bill to Comdata VCard", 3721.07, null, "", null, "03/03/2016", null, null, null, "01/18/2016", "02/12/2016", false, 7, "02/21/2016", true, "Charged");
            table.Rows.Add("Value Place Columbia - Elgin, SC", "Value Place", "Value Place", null, 72099, 2099514, "ElginSC031916", "03/19/2016", 9, 289.26, "Direct Bill to Comdata VCard", 289.26, null, "", null, "04/04/2016", null, null, null, "03/10/2016", "03/18/2016", false, 7, "03/19/2016", true, "Charged");
            table.Rows.Add("Value Place Denver - Firestone, CO", "Value Place", "Value Place", "WoodSpring Property Management", 62503, 2050072, "CLS-WN", "01/20/2016", 39, 2238.6, "Direct Bill to Comdata VCard", 2238.6, null, "", null, "02/01/2016", null, null, null, "12/12/2015", "01/19/2016", false, 7, "01/20/2016", false, "Charged");
            table.Rows.Add("Value Place Denver - Firestone, CO", "Value Place", "Value Place", "WoodSpring Property Management", 67634, 2076107, "FirestoneCO022116", "02/21/2016", 19, 1090.6, "Direct Bill to Comdata VCard", 1090.6, null, "", null, "03/02/2016", null, null, null, "01/20/2016", "02/07/2016", false, 7, "02/21/2016", false, "Charged");
            table.Rows.Add("Value Place Denver - Firestone, CO", "Value Place", "Value Place", "WoodSpring Property Management", 72100, 2099515, "FirestoneCO031916", "03/19/2016", 14, 803.6, "Direct Bill to Comdata VCard", 803.6, null, "", null, "03/30/2016", null, null, null, "02/25/2016", "03/09/2016", false, 7, "03/19/2016", true, "Charged");
            table.Rows.Add("Value Place East - Baton Rouge, LA", "Value Place", "Value Place", "(Independent)", null, 2136407, "COMD04/05/16-228", "04/09/2016", 7, 316.23, "Comdata VCard Credit Card", 316.23, "05/05/2016", "", null, null, null, null, null, "04/02/2016", "04/08/2016", false, 7, "05/05/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place Raleigh North - Raleigh, NC", "Value Place", "Value Place", "(Independent)", null, 2102465, "COMD02/27/16-114", "03/22/2016", 26, 1256.58, "Comdata VCard Credit Card", 1256.58, "03/23/2016", "", null, null, null, null, null, "02/25/2016", "03/21/2016", false, 7, "03/23/2016", true, "Payment Mailed");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, null, 2095859, "20262", "02/25/2016", 3, 212.55, "Direct Bill to Comdata VCard", 212.55, null, "", null, null, null, null, null, "02/22/2016", "02/24/2016", false, 0, "03/15/2016", false, "CC Ready");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, null, 2144024, "GNS", "04/19/2016", 1, 0, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "04/18/2016", "04/18/2016", false, 0, "05/16/2016", true, "Invoice Accepted: Processing");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, null, 2144031, "GNS 4/18", "04/19/2016", 1, 0, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "04/18/2016", "04/18/2016", false, 0, "05/16/2016", true, "Invoice Accepted: Processing");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, null, 2144033, "GNS 4/18 4/19", "04/19/2016", 1, 0, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "04/18/2016", "04/18/2016", false, 0, "05/16/2016", true, "Invoice Accepted: Processing");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, null, 2144035, "GNS 4/18-4/19", "04/19/2016", 1, 0, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "04/18/2016", "04/18/2016", false, 0, "05/16/2016", true, "Invoice Accepted: Processing");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, 80588, 2136257, "0416", "05/05/2016", 41, 2085.53, "Direct Bill to Comdata VCard", 2085.53, "05/13/2016", "", null, "05/06/2016", null, null, null, "04/20/2016", "05/04/2016", false, 0, "05/05/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, 82414, 2143692, "051616", "05/16/2016", 58, 2976.74, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "05/05/2016", "05/15/2016", false, 0, "05/16/2016", false, "Invoice Accepted: Processing");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, 83934, 2151974, "052416", "05/24/2016", 51, 2682.54, "Direct Bill to Comdata VCard", 2682.54, "06/01/2016", "", null, "05/25/2016", null, null, null, "05/16/2016", "05/23/2016", false, 0, "05/24/2016", false, "Payment Mailed");
            table.Rows.Add("Value Place Savannah - Garden City, GA", "Value Place", "Value Place", null, 85005, 2159060, "053116", "05/31/2016", 27, 1396.71, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "05/22/2016", "05/30/2016", false, 0, "05/31/2016", true, "Invoice Accepted: Processing");
            table.Rows.Add("Value Place South - Wichita, KS", "Value Place", "Value Place", "WoodSpring Property Management", 61963, 2047475, "CLSGW", "01/18/2016", 12, 387.24, "Direct Bill to Comdata VCard", 387.24, null, "", null, "01/29/2016", null, null, null, "01/03/2016", "01/04/2016", false, 7, "01/18/2016", false, "Charged");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2079740, "COMD02/24/16-555", "01/24/2016", 7, 351.68, "Comdata VCard Credit Card", 351.68, "02/24/2016", "", null, null, null, null, null, "01/17/2016", "01/23/2016", false, 0, "02/24/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2084962, "COMD03/02/16-319", "01/31/2016", 14, 820.82, "Comdata VCard Credit Card", 820.82, "03/02/2016", "", null, null, null, null, null, "01/17/2016", "01/30/2016", false, 0, "03/02/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2085196, "COMD03/02/16-210", "01/31/2016", 14, 703.36, "Comdata VCard Credit Card", 703.36, "03/02/2016", "", null, null, null, null, null, "01/17/2016", "01/30/2016", false, 0, "03/02/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2085836, "COMD03/02/16-564", "01/31/2016", 14, 820.82, "Comdata VCard Credit Card", 820.82, "03/02/2016", "", null, null, null, null, null, "01/17/2016", "01/30/2016", false, 0, "03/02/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2091109, "COMD03/09/16-304", "02/08/2016", 6, 301.44, "Comdata VCard Credit Card", 301.44, "03/09/2016", "", null, null, null, null, null, "02/02/2016", "02/07/2016", false, 0, "03/09/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2091114, "COMD03/09/16-303", "02/08/2016", 6, 301.44, "Comdata VCard Credit Card", 301.44, "03/09/2016", "", null, null, null, null, null, "02/02/2016", "02/07/2016", false, 0, "03/09/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2091116, "COMD03/09/16-310", "02/08/2016", 6, 301.44, "Comdata VCard Credit Card", 301.44, "03/09/2016", "", null, null, null, null, null, "02/02/2016", "02/07/2016", false, 0, "03/09/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2091119, "10668", "02/12/2016", 1, 93.8, "Direct Bill to Comdata VCard", 93.8, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/11/2016", "02/11/2016", false, 0, "03/09/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094233, "COMD03/14/16-133", "03/04/2016", 16, 803.84, "Comdata VCard Credit Card", 803.84, "03/14/2016", "", null, null, null, null, null, "02/17/2016", "03/03/2016", false, 0, "03/14/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094245, "COMD03/14/16-314", "03/11/2016", 23, 1155.52, "Comdata VCard Credit Card", 1155.52, "03/14/2016", "", null, null, null, null, null, "02/17/2016", "03/10/2016", false, 0, "03/14/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094250, "COMD03/14/16-132", "03/11/2016", 23, 1155.52, "Comdata VCard Credit Card", 1155.52, "03/14/2016", "", null, null, null, null, null, "02/17/2016", "03/10/2016", false, 0, "03/14/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094261, "10678", "02/28/2016", 14, 466.62, "Direct Bill to Comdata VCard", 466.62, "03/22/2016", "", null, "03/17/2016", null, null, null, "02/14/2016", "02/27/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094287, "641", "03/08/2016", 30, 965.7, "Direct Bill to Comdata VCard", 965.7, "04/05/2016", "", null, "03/17/2016", null, null, null, "02/07/2016", "03/07/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094300, "641 3/8-13", "03/14/2016", 6, 193.14, "Direct Bill to Comdata VCard", 193.14, "03/22/2016", "", null, "03/17/2016", null, null, null, "03/08/2016", "03/13/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094309, "643", "02/12/2016", 7, 233.31, "Direct Bill to Comdata VCard", 233.31, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/05/2016", "02/11/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094314, "643 2/12-3/1", "03/02/2016", 19, 633.27, "Direct Bill to Comdata VCard", 633.27, "03/22/2016", "", null, "03/17/2016", null, null, null, "02/12/2016", "03/01/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094338, "643 3/2-6", "03/07/2016", 5, 132.15, "Direct Bill to Comdata VCard", 132.15, "03/22/2016", "", null, "03/17/2016", null, null, null, "03/02/2016", "03/06/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094356, "643 3/7-11", "03/14/2016", 7, 225.33, "Direct Bill to Comdata VCard", 225.33, "03/22/2016", "", null, "03/17/2016", null, null, null, "03/07/2016", "03/13/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094367, "640", "02/12/2016", 10, 333.3, "Direct Bill to Comdata VCard", 333.3, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/02/2016", "02/11/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094459, "640 2/12-3/2", "03/03/2016", 20, 632.4, "Direct Bill to Comdata VCard", 632.4, "03/22/2016", "", null, "03/17/2016", null, null, null, "02/12/2016", "03/02/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094470, "640 3/3-13", "03/14/2016", 11, 354.09, "Direct Bill to Comdata VCard", 354.09, "03/22/2016", "", null, "03/17/2016", null, null, null, "03/03/2016", "03/13/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094493, "639", "02/12/2016", 10, 586.3, "Direct Bill to Comdata VCard", 586.3, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/02/2016", "02/11/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094497, "639 2/12-3/2", "03/03/2016", 20, 1112.6, "Direct Bill to Comdata VCard", 1112.6, "03/22/2016", "", null, "03/17/2016", null, null, null, "02/12/2016", "03/02/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2094515, "639 3/3-13", "03/14/2016", 11, 622.93, "Direct Bill to Comdata VCard", 622.93, "03/22/2016", "", null, "03/17/2016", null, null, null, "03/03/2016", "03/13/2016", false, 0, "03/14/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098237, "COMD03/17/16-628", "02/09/2016", 7, 410.41, "Comdata VCard Credit Card", 410.41, "03/17/2016", "", null, null, null, null, null, "02/02/2016", "02/08/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098245, "628", "03/01/2016", 21, 1231.23, "Direct Bill to Comdata VCard", 1231.23, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/09/2016", "02/29/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098247, "628 3/1-2", "03/03/2016", 2, 57.26, "Direct Bill to Comdata VCard", 57.26, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/01/2016", "03/02/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098252, "628 3/3-7", "03/08/2016", 5, 283.15, "Direct Bill to Comdata VCard", 283.15, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/03/2016", "03/07/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098262, "COMD03/17/16-110", "02/23/2016", 21, 1055.04, "Comdata VCard Credit Card", 1055.04, "03/17/2016", "", null, null, null, null, null, "02/02/2016", "02/22/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098266, "COMD03/17/16-110A", "03/01/2016", 7, 351.68, "Comdata VCard Credit Card", 351.68, "03/17/2016", "", null, null, null, null, null, "02/23/2016", "02/29/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098269, "COMD03/17/16-110B", "03/03/2016", 2, 49.18, "Comdata VCard Credit Card", 49.18, "03/17/2016", "", null, null, null, null, null, "03/01/2016", "03/02/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098276, "COMD03/17/16-110C", "03/08/2016", 5, 242.65, "Comdata VCard Credit Card", 242.65, "03/17/2016", "", null, null, null, null, null, "03/03/2016", "03/07/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098319, "COMD03/17/16-216", "02/16/2016", 14, 820.82, "Comdata VCard Credit Card", 820.82, "03/17/2016", "", null, null, null, null, null, "02/02/2016", "02/15/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098326, "COMD03/17/16-216A", "02/23/2016", 7, 410.41, "Comdata VCard Credit Card", 410.41, "03/17/2016", "", null, null, null, null, null, "02/16/2016", "02/22/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098340, "COMD03/17/16-216B", "03/01/2016", 7, 410.41, "Comdata VCard Credit Card", 410.41, "03/17/2016", "", null, null, null, null, null, "02/23/2016", "02/29/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098357, "COMD03/17/16-216C", "03/03/2016", 2, 57.26, "Comdata VCard Credit Card", 57.26, "03/17/2016", "", null, null, null, null, null, "03/01/2016", "03/03/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2098361, "COMD03/17/16-216D", "03/08/2016", 5, 283.15, "Comdata VCard Credit Card", 283.15, "03/17/2016", "", null, null, null, null, null, "03/03/2016", "03/07/2016", false, 0, "03/17/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099105, "10722", "03/02/2016", 10, 333.3, "Direct Bill to Comdata VCard", 333.3, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/21/2016", "03/01/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099106, "10722 3/2-13", "03/14/2016", 12, 399.96, "Direct Bill to Comdata VCard", 399.96, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/02/2016", "03/13/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099107, "10722 3/14-17", "03/18/2016", 4, 133.32, "Direct Bill to Comdata VCard", 133.32, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/14/2016", "03/17/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099109, "10705", "03/03/2016", 11, 366.63, "Direct Bill to Comdata VCard", 366.63, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/21/2016", "03/02/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099112, "10705 3/3-14", "03/15/2016", 12, 399.96, "Direct Bill to Comdata VCard", 399.96, "04/12/2016", "", null, "03/31/2016", null, null, null, "03/03/2016", "03/14/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099114, "10705 3/15-17", "03/18/2016", 3, 99.99, "Direct Bill to Comdata VCard", 99.99, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/15/2016", "03/17/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099268, "10717", "03/02/2016", 10, 333.3, "Direct Bill to Comdata VCard", 333.3, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/21/2016", "03/01/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099273, "10717 3/2-13", "03/14/2016", 12, 399.96, "Direct Bill to Comdata VCard", 399.96, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/02/2016", "03/13/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099275, "10717 3/14-17", "03/18/2016", 4, 133.32, "Direct Bill to Comdata VCard", 133.32, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/14/2016", "03/17/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099280, "10718 2/21-3/1", "03/02/2016", 10, 333.3, "Direct Bill to Comdata VCard", 333.3, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/21/2016", "03/01/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099282, "10718 3/2-3/13", "03/14/2016", 12, 399.96, "Direct Bill to Comdata VCard", 399.96, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/02/2016", "03/13/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099287, "10718 3/14-17", "03/18/2016", 4, 133.32, "Direct Bill to Comdata VCard", 133.32, "04/06/2016", "", null, "03/31/2016", null, null, null, "03/14/2016", "03/17/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2099296, "10679", "02/21/2016", 7, 233.31, "Direct Bill to Comdata VCard", 233.31, "04/06/2016", "", null, "03/31/2016", null, null, null, "02/14/2016", "02/20/2016", false, 0, "03/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2113034, "10746", "03/13/2016", 12, 399.96, "Direct Bill to Comdata VCard", 399.96, "05/24/2016", "", null, "05/17/2016", null, null, null, "03/01/2016", "03/12/2016", false, 0, "04/06/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2113795, "10907", "03/23/2016", 2, 66.66, "Direct Bill to Comdata VCard", 66.66, "05/24/2016", "", null, "05/17/2016", null, null, null, "03/21/2016", "03/22/2016", false, 0, "04/07/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2117197, "10714", "03/21/2016", 3, 99.99, "Direct Bill to Comdata VCard", 99.99, "04/12/2016", "", null, null, null, null, null, "03/18/2016", "03/20/2016", false, 0, "04/12/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121165, "10888", "03/28/2016", 13, 433.29, "Direct Bill to Comdata VCard", 433.29, "04/26/2016", "", null, "04/27/2016", null, null, null, "03/15/2016", "03/27/2016", false, 0, "04/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121191, "641 3/14-3/26", "03/27/2016", 13, 418.47, "Direct Bill to Comdata VCard", 418.47, "04/26/2016", "", null, "04/27/2016", null, null, null, "03/14/2016", "03/26/2016", false, 0, "04/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121192, "641 3/27", "03/28/2016", 1, 32.19, "Direct Bill to Comdata VCard", 32.19, "04/26/2016", "", null, "04/27/2016", null, null, null, "03/27/2016", "03/27/2016", false, 0, "04/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121241, "10722 03/18-3.26", "03/27/2016", 9, 299.97, "Direct Bill to Comdata VCard", 299.97, "04/26/2016", "", null, "04/27/2016", null, null, null, "03/18/2016", "03/26/2016", false, 0, "04/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121255, "10717 3/18-3/26", "03/27/2016", 9, 299.97, "Direct Bill to Comdata VCard", 299.97, "04/26/2016", "", null, "04/27/2016", null, null, null, "03/18/2016", "03/26/2016", false, 0, "04/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121263, "10705 3/18-3/19", "03/20/2016", 2, 66.66, "Direct Bill to Comdata VCard", 66.66, "05/24/2016", "", null, "05/17/2016", null, null, null, "03/18/2016", "03/19/2016", false, 0, "04/18/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121876, "639 03/14-04/17", "04/17/2016", 34, 1925.42, "Direct Bill to Comdata VCard", 1925.42, "04/27/2016", "", null, "04/27/2016", null, null, null, "03/14/2016", "04/16/2016", false, 0, "04/19/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121920, "643 3/14-4/17", "04/17/2016", 34, 1094.46, "Direct Bill to Comdata VCard", 1094.46, "04/27/2016", "", null, "04/27/2016", null, null, null, "03/14/2016", "04/16/2016", false, 0, "04/19/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2121941, "640 3/27-4/17", "04/17/2016", 34, 1094.46, "Direct Bill to Comdata VCard", 1094.46, "04/27/2016", "", null, "04/27/2016", null, null, null, "03/14/2016", "04/16/2016", false, 0, "04/19/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2130822, "10676", "02/15/2016", 1, 0, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "02/14/2016", "02/14/2016", false, 0, "04/28/2016", false, "Invoice Accepted: Processing");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2130823, "10679-1", "03/28/2016", 36, 1199.88, "Direct Bill to Comdata VCard", 0, null, "", null, null, null, null, null, "02/21/2016", "03/27/2016", false, 0, "04/28/2016", false, "Invoice Accepted: Processing");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2135895, "10792", "03/10/2016", 4, 133.29, "Direct Bill to Comdata VCard", 133.29, "05/24/2016", "", null, "05/17/2016", null, null, null, "03/06/2016", "03/09/2016", false, 0, "05/04/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2143728, "10986", "04/23/2016", 23, 766.59, "Direct Bill to Comdata VCard", 766.59, "05/24/2016", "", null, "05/17/2016", null, null, null, "03/31/2016", "04/22/2016", false, 0, "05/16/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2143734, "11097 4/10", "04/11/2016", 1, 33.33, "Direct Bill to Comdata VCard", 33.33, "05/27/2016", "", null, null, null, null, null, "04/10/2016", "04/10/2016", false, 0, "05/16/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2143757, "11097", "04/15/2016", 4, 133.32, "Direct Bill to Comdata VCard", 133.32, "05/24/2016", "", null, "05/17/2016", null, null, null, "04/11/2016", "04/14/2016", false, 0, "05/16/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2143763, "11008", "04/15/2016", 12, 399.96, "Direct Bill to Comdata VCard", 399.96, "05/24/2016", "", null, "05/17/2016", null, null, null, "04/03/2016", "04/14/2016", false, 0, "05/16/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2148634, "11122", "04/29/2016", 12, 399.96, "Direct Bill to Comdata VCard", 399.96, "05/27/2016", "", null, "05/25/2016", null, null, null, "04/17/2016", "04/28/2016", false, 0, "05/19/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2148645, "11132", "04/30/2016", 11, 366.63, "Direct Bill to Comdata VCard", 366.63, "05/27/2016", "", null, "05/25/2016", null, null, null, "04/19/2016", "04/29/2016", false, 0, "05/19/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157243, "11131", "04/27/2016", 6, 199.98, "Direct Bill to Comdata VCard", 199.98, "05/27/2016", "", null, null, null, null, null, "04/21/2016", "04/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157418, "11145", "05/01/2016", 7, 233.31, "Direct Bill to Comdata VCard", 233.31, "05/27/2016", "", null, null, null, null, null, "04/24/2016", "04/30/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157420, "11146", "05/01/2016", 7, 233.31, "Direct Bill to Comdata VCard", 233.31, "05/27/2016", "", null, null, null, null, null, "04/24/2016", "04/30/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157421, "11149", "05/03/2016", 9, 299.97, "Direct Bill to Comdata VCard", 299.97, "05/27/2016", "", null, null, null, null, null, "04/24/2016", "05/02/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157422, "10850", "05/06/2016", 54, 1738.26, "Direct Bill to Comdata VCard", 1738.26, "05/27/2016", "", null, null, null, null, null, "03/13/2016", "05/05/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157424, "11207", "05/15/2016", 7, 233.31, "Direct Bill to Comdata VCard", 233.31, "05/27/2016", "", null, null, null, null, null, "05/08/2016", "05/14/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157428, "11109", "05/11/2016", 29, 966.57, "Direct Bill to Comdata VCard", 966.57, "05/27/2016", "", null, null, null, null, null, "04/12/2016", "05/10/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157434, "11121", "05/26/2016", 32, 1030.08, "Direct Bill to Comdata VCard", 1030.08, "05/27/2016", "", null, null, null, null, null, "04/24/2016", "05/25/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157437, "643 4/17", "05/11/2016", 24, 414.41, "Direct Bill to Comdata VCard", 414.41, "05/27/2016", "", null, null, null, null, null, "04/17/2016", "05/10/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157478, "10722 3/27", "05/25/2016", 59, 1544.83, "Direct Bill to Comdata VCard", 1544.83, "05/27/2016", "", null, null, null, null, null, "03/27/2016", "05/24/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157488, "11072", "05/27/2016", 47, 1518.13, "Direct Bill to Comdata VCard", 1518.13, "05/27/2016", "", null, null, null, null, null, "04/10/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157494, "11166", "05/27/2016", 32, 1030.08, "Direct Bill to Comdata VCard", 1030.08, "05/27/2016", "", null, null, null, null, null, "04/25/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157500, "11148", "05/27/2016", 33, 1124.67, "Direct Bill to Comdata VCard", 1124.67, "05/27/2016", "", null, null, null, null, null, "04/24/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157508, "11147", "05/27/2016", 33, 1062.27, "Direct Bill to Comdata VCard", 1062.27, "05/27/2016", "", null, null, null, null, null, "04/24/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157517, "11123", "05/25/2016", 31, 997.89, "Direct Bill to Comdata VCard", 997.89, "05/27/2016", "", null, null, null, null, null, "04/24/2016", "05/24/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157534, "11014 4/3", "04/17/2016", 14, 450.66, "Direct Bill to Comdata VCard", 450.66, "05/27/2016", "", null, null, null, null, null, "04/03/2016", "04/16/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157536, "11014 4/17", "05/27/2016", 40, 1287.6, "Direct Bill to Comdata VCard", 1287.6, "05/27/2016", "", null, null, null, null, null, "04/17/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157540, "10991", "05/27/2016", 55, 1791.25, "Direct Bill to Comdata VCard", 1791.25, "05/27/2016", "", null, null, null, null, null, "04/02/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157545, "10976", "05/27/2016", 61, 1963.59, "Direct Bill to Comdata VCard", 1963.59, "05/27/2016", "", null, null, null, null, null, "03/27/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157547, "10717 3/27", "05/27/2016", 61, 1561.77, "Direct Bill to Comdata VCard", 1561.77, "05/27/2016", "", null, null, null, null, null, "03/27/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157554, "639 4/17", "05/27/2016", 40, 1502.75, "Direct Bill to Comdata VCard", 1502.75, "05/27/2016", "", null, null, null, null, null, "04/17/2016", "05/26/2016", false, 0, "05/27/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157702, "11258", "05/25/2016", 5, 166.65, "Direct Bill to Comdata VCard", 166.65, "05/28/2016", "", null, null, null, null, null, "05/20/2016", "05/24/2016", false, 0, "05/28/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2157704, "11271", "05/27/2016", 2, 66.66, "Direct Bill to Comdata VCard", 66.66, "05/28/2016", "", null, null, null, null, null, "05/25/2016", "05/26/2016", false, 0, "05/28/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Chattanooga, TN", "Value Place", "Value Place", "(Independent)", null, 2161013, "11007", "04/25/2016", 22, 733.26, "Direct Bill to Comdata VCard", 733.26, "06/01/2016", "", null, null, null, null, null, "04/03/2016", "04/24/2016", false, 0, "06/01/2016", false, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Frederick, MD", "Value Place", "Value Place", "(Independent)", null, 2119013, "COMD02/29/16-303", "02/29/2016", 14, 739.1, "Comdata VCard Credit Card", 739.1, "04/14/2016", "", null, null, null, null, null, "02/15/2016", "02/28/2016", false, 0, "04/14/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Frederick, MD", "Value Place", "Value Place", "(Independent)", null, 2119059, "COMD03/02/16-333", "03/07/2016", 14, 739.12, "Comdata VCard Credit Card", 739.12, "04/14/2016", "", null, null, null, null, null, "02/22/2016", "03/06/2016", false, 0, "04/14/2016", true, "Payment Mailed");
            table.Rows.Add("WoodSpring Suites - Frederick, MD", "Value Place", "Value Place", "(Independent)", null, 2119063, "COMD03/09/16-131", "03/14/2016", 7, 369.53, "Comdata VCard Credit Card", 369.53, "04/14/2016", "", null, null, null, null, null, "03/07/2016", "03/13/2016", false, 0, "04/14/2016", true, "Payment Mailed");












            return table;
        }

        public ActionResult _responsiveGrid()
        {
            var dt = new DataTable();

            if (Session["WebHotelInvoices"] != null)
            {
                dt = (DataTable)Session["WebHotelInvoices"];
            }

            return PartialView("_responsiveGrid", dt);
        }

        public ActionResult ExportToExcel()
        {
            var dt = new DataTable();

            if (Session["WebHotelInvoices"] != null)
            {
                dt = (DataTable)Session["WebHotelInvoices"];
            }

            return GridViewExtension.ExportToXlsx(GetGridSettings(), dt);
        }

        public ActionResult CustomCallbackFromGrid(string UseGrouping)
        {
            ViewData["UseGrouping"] = UseGrouping;
            return PartialView("_responsiveGrid", Session["WebHotelInvoices"]);
        }

        // Returns the settings of the exported GridView. 
        private GridViewSettings GetGridSettings()
        {
            var settings = new GridViewSettings
            {
                Name = "gridView",
                CallbackRouteValues = new { Controller = "HotelInvoice", Action = "_responsiveGrid" },
                Width = Unit.Percentage(100)
            };

            // Export-specific settings  
            settings.SettingsExport.ExportSelectedRowsOnly = false;
            settings.SettingsExport.FileName = "Invoices.xlsx";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;

            settings.KeyFieldName = "WebHotelInvoiceId";
            settings.Columns.Add("Filter");
            settings.Columns.Add("HotelName");
            settings.Columns.Add("HotelInvoiceNumber");
            settings.Columns.Add("InvoiceDate").SortIndex = 0;
            settings.Columns.Add("InvoiceDate").SortOrder = ColumnSortOrder.Descending;
            settings.Columns.Add("Nights");
            settings.Columns.Add("InvoiceTotal");

            MVCxGridViewColumn billingColumn = settings.Columns.Add("Method of Payment");
            billingColumn.UnboundType = UnboundColumnType.String;

            MVCxGridViewColumn statusColumn = settings.Columns.Add("Status");
            statusColumn.UnboundType = UnboundColumnType.String;

            settings.CustomUnboundColumnData = (sender, e) =>
            {
                if (e.Column.ToString() == "Status")
                {
                    var paymentDate = e.GetListSourceFieldValue("PaymentDate").ToString();
                    var checkNumber = e.GetListSourceFieldValue("CheckNumber").ToString();
                    var cardNumber = e.GetListSourceFieldValue("CardNumber").ToString();
                    var cardTransanctionDate = e.GetListSourceFieldValue("CardTransactionDate").ToString();
                    var priorityPayDaysToPay = e.GetListSourceFieldValue("PriorityPayDaysToPay").ToString();
                    var creationDate = Convert.ToDateTime(e.GetListSourceFieldValue("CreationDate").ToString());
                    var billing = e.GetListSourceFieldValue("Billing").ToString();
                    var rejectionReason = e.GetListSourceFieldValue("RejectionReason").ToString();
                    var completionDate = e.GetListSourceFieldValue("CompletionDate").ToString();
                    var approvalDate = e.GetListSourceFieldValue("ApprovalDate").ToString();
                    var hotelInvoiceKey = e.GetListSourceFieldValue("HotelInvoiceKey").ToString();
                    string returnValue = string.Empty;

                    if (!string.IsNullOrEmpty(hotelInvoiceKey))
                    {
                        if (!string.IsNullOrEmpty(paymentDate) && !string.IsNullOrEmpty(checkNumber))
                            returnValue = string.Format("Payment mailed via check #{0} on {1}.", checkNumber, Convert.ToDateTime(paymentDate).ToShortDateString());
                        else if (!string.IsNullOrEmpty(cardNumber) && !string.IsNullOrEmpty(cardTransanctionDate))
                            returnValue = string.Format("Credit card charged on {0}.", Convert.ToDateTime(cardTransanctionDate).ToShortDateString());
                        else if (!string.IsNullOrEmpty(cardNumber))
                            returnValue = "Credit card available.";
                        else if (Convert.ToDouble(priorityPayDaysToPay) > 0 && creationDate <= creationDate.AddDays(Convert.ToDouble(priorityPayDaysToPay)) && MethodsOfPayment.PostPayVCardTypes.Contains(billing))
                        {
                            returnValue = "Credit card will be available on " + creationDate.AddDays(Convert.ToDouble(priorityPayDaysToPay)).ToShortDateString();
                        }
                        else if (!string.IsNullOrEmpty(paymentDate))
                            returnValue = string.Format("Payment sent on {0}.", Convert.ToDateTime(paymentDate).ToShortDateString());
                        else
                            returnValue = "Invoice accepted.  Awaiting processing by Creative Lodging Solutions.";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(rejectionReason))
                            returnValue = "Rejected: " + rejectionReason;
                        else if (!string.IsNullOrEmpty(completionDate) && string.IsNullOrEmpty(approvalDate))
                            returnValue = "Awaiting approval by Creative Lodging Solutions because\n you changed the rate, tax, or misc. charges within the invoice.";
                        else if (string.IsNullOrEmpty(completionDate))
                            returnValue = "Awaiting submission.";
                    }
                    e.Value = returnValue;
                }
                else if (e.Column.ToString() == "Method of Payment")
                {
                    var billing = e.GetListSourceFieldValue("Billing");
                    e.Value = MethodsOfPayment.ToSimpleDesc(billing.ToString()).ToString();
                }
            };

            return settings;
        }
    }
}