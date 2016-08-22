using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Web;
using System.Web.Mvc.Html;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Routing;
using System.Web.UI;
//using CLS.Data.Models;
//using CLS.Data.Services;
//using Cyg.Framework.Web.Models;
using DevExpress.Web.Mvc.UI;

namespace DXWebApplication5.Helpers
{
	public static class ExportHelper
	{
        public static GridViewSettings GetGridSettings(this HtmlHelper Html)//, WebUser user)
        {
            GridViewSettings settings = new GridViewSettings
            {
                Name = "gridView",
                CallbackRouteValues = new { Controller = "Request", Action = "_responsiveGrid" },
                Width = Unit.Percentage(100)

            };

            // Export-specific settings  
            settings.SettingsExport.ExportSelectedRowsOnly = false;
            settings.SettingsExport.FileName = "Requests.xlsx";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;

            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCellsWindowLimit;
            settings.SettingsAdaptivity.HideDataCellsAtWindowInnerWidth = 500;

            settings.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;
            settings.SettingsAdaptivity.AdaptiveDetailLayoutProperties.SettingsItems.ShowCaption = DefaultBoolean.True;

            settings.Styles.AlternatingRow.Enabled = DefaultBoolean.Default;
            settings.Styles.AlternatingRow.BackColor = Color.Snow;

            settings.SettingsPager.Position = PagerPosition.Bottom;
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.PageSize = 25;
            settings.SettingsPager.PageSizeItemSettings.Items = new string[] { "25", "50", "100" };
            settings.SettingsPager.PageSizeItemSettings.ShowAllItem = true;
            settings.SettingsPopup.HeaderFilter.Height = Unit.Pixel(440);
            settings.SettingsPopup.HeaderFilter.Width = Unit.Pixel(400);

            settings.Settings.ShowGroupPanel = false;
            settings.SettingsBehavior.AutoExpandAllGroups = false;
            settings.SettingsSearchPanel.Visible = true;

            settings.KeyFieldName = "RequestNumber";

            if (Html != null)
            {
                settings.Columns.Add(column =>
                {
                    column.SetDataItemTemplateContent(dataTemplate =>
                    {
                        var id = DataBinder.Eval(dataTemplate.DataItem, "RequestNumber");

                        int webRequestNumber = 0;
                        int requestNumber = 0;

                        if (DataBinder.Eval(dataTemplate.DataItem, "WebRequestNumber").ToString() != string.Empty)
                        {
                            webRequestNumber = Convert.ToInt32(DataBinder.Eval(dataTemplate.DataItem, "WebRequestNumber"));
                        }

                        if (DataBinder.Eval(dataTemplate.DataItem, "RequestNumber").ToString() != string.Empty)
                        {
                            requestNumber = Convert.ToInt32(DataBinder.Eval(dataTemplate.DataItem, "RequestNumber"));
                        }

                        string webRequestStatus = string.Empty;


                        if (DataBinder.Eval(dataTemplate.DataItem, "Status").ToString() != string.Empty)
                        {
                            webRequestStatus = DataBinder.Eval(dataTemplate.DataItem, "Status").ToString();
                        }

                        string html = string.Empty;

                        UrlHelper Url = new UrlHelper(new RequestContext(Html.ViewContext.HttpContext, Html.ViewContext.RouteData));
                        var linkFormat = @"<a href=""{0}"" class=""icon"" title=""{1}"">{2}</a>";
                        var detailsImage = Html.Image("~/Content/images/details.png", "View Details", "");

                        if (id == DBNull.Value && webRequestNumber > 0)
                        {
                            // If there isn't a Genesis request yet, provide a link to show the unintegrated request
                            string detailsUrl = Url.Action("ShowUnintegrated", new { id = webRequestNumber });
                            html += string.Format(linkFormat, detailsUrl, "View Details", detailsImage);


                            Html.ViewContext.Writer.Write(html);

                        }
                        else
                        {
                            // Otherwise, provide a link to the full request, as well as a link to copy the request.
                            string detailsUrl = Url.Action("Show", new { id = id });
                            html += string.Format(linkFormat, detailsUrl, "View Details", detailsImage);

                            //if (Html.GetCurrentUser().HasPermissions("WebAddRequests"))
                            //{
                                string copyUrl = Url.Action("New", new { copyFromRequest = id });
                                var copyImage = Html.Image("~/Content/images/copy.png", "View Details", "");
                                //if (Html.GetCurrentUser().IsClient)
                                //{
                                    html += "&nbsp;" + string.Format(linkFormat, copyUrl, "Create a new request from this one", copyImage);
                                //}
                            //}

                            var clientCanPickOptions = false;



                            var anonymousHash = string.Empty;

                            //if (requestNumber > 0)
                            //{
                            //    anonymousHash = RequestControllerHelper.GetAonymousAuthorizationHash(requestNumber);
                            //    clientCanPickOptions = RequestControllerHelper.GetClientCanPickPotions(requestNumber, webRequestStatus, anonymousHash);
                            //}


                            //if (clientCanPickOptions)
                            //{
                                var pickUrl = Url.Action("PickOption", new { id = id, auth = anonymousHash });
                                var pickImage = Html.Image("~/Content/images/pick2.png", "Pick Options", "");
                                html += "&nbsp;" + string.Format(linkFormat, pickUrl, "Pick a hotel from the provided options", pickImage);
                            //}

                            Html.ViewContext.Writer.Write(html);
                        }

                        column.CellStyle.HorizontalAlign = HorizontalAlign.Center;

                    });
                });
            }

            settings.Columns.Add(column =>
            {
                column.FieldName = "STATUS";
                column.Caption = "Status";
                column.CellStyle.HorizontalAlign = HorizontalAlign.Center;

                column.Settings.AllowHeaderFilter = DefaultBoolean.True;
                column.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;

            });
            //if (!user.IsClient)
            //{
                settings.Columns.Add("ClientName").CellStyle.HorizontalAlign = HorizontalAlign.Center;
            //}

            settings.Columns.Add(column =>
            {
                column.FieldName = "WorkAddress";
                column.Visible = false;
                column.Name = "Address";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "WorkCity";
                column.Visible = false;
                ;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "WorkState";
                column.Visible = false;
                ;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "WorkZip";
                column.Visible = false;
                ;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Work Address Visible";
                column.UnboundType = DevExpress.Data.UnboundColumnType.String;
                column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                column.Caption = "Work Address";
            });

            #region Traveler Columns

            //Room01
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room01Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room01Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room01Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room01Guest2LastName";
                column.Visible = false;
            });

            //Room02
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room02Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room02Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room02Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room02Guest2LastName";
                column.Visible = false;
            });

            //Room03
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room03Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room03Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room03Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room03Guest2LastName";
                column.Visible = false;
            });

            //Room04
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room04Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room04Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room04Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room04Guest2LastName";
                column.Visible = false;
            });

            //Room05
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room05Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room05Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room05Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room05Guest2LastName";
                column.Visible = false;
            });

            //Room06
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room06Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room06Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room06Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room06Guest2LastName";
                column.Visible = false;
            });

            //Room07
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room07Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room07Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room07Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room07Guest2LastName";
                column.Visible = false;
            });

            //Room08
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room08Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room08Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room08Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room08Guest2LastName";
                column.Visible = false;
            });

            //Room09
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room09Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room09Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room09Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room09Guest2LastName";
                column.Visible = false;
            });

            //Room10
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room10Guest1FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room10Guest1LastName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room10Guest2FirstName";
                column.Visible = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Room10Guest2LastName";
                column.Visible = false;
            });


            settings.Columns.Add(column =>
            {
                // "FieldName" contains a unique value that does not refer to any field in the GridView's data model.
                column.FieldName = "Travelers";
                // The column contains string values.
                column.UnboundType = DevExpress.Data.UnboundColumnType.String;
                //    column.Width = Unit.Pixel(170);
                column.CellStyle.Wrap = DefaultBoolean.True;
            });

            #endregion

            //if (user.IsClient && !user.DemoUser)
            //{
                settings.Columns.Add(column =>
                {
                    column.FieldName = "ClientField1";
                    //column.Caption = user.CodingFieldNames["ClientField1Name"];
                    //column.Visible = user.CodingFieldNames["ClientField1Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["ClientField1Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "ClientField2";
                    //column.Caption = user.CodingFieldNames["ClientField2Name"];
                    //column.Visible = user.CodingFieldNames["ClientField2Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["ClientField2Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "ClientField3";
                    //column.Caption = user.CodingFieldNames["ClientField3Name"];
                    //column.Visible = user.CodingFieldNames["ClientField3Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["ClientField3Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    ;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "ClientField4";
                    //column.Caption = user.CodingFieldNames["ClientField4Name"];
                    //column.Visible = user.CodingFieldNames["ClientField4Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["ClientField4Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "ClientField5";
                    //column.Caption = user.CodingFieldNames["ClientField5Name"];
                    //column.Visible = user.CodingFieldNames["ClientField5Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["ClientField5Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "GuestField1";
                    //column.Caption = user.CodingFieldNames["GuestField1Name"];
                   // column.Visible = user.CodingFieldNames["GuestField1Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["GuestField1Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "GuestField2";
                    //column.Caption = user.CodingFieldNames["GuestField2Name"];
                    //column.Visible = user.CodingFieldNames["GuestField2Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["GuestField2Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "GuestField3";
                    //column.Caption = user.CodingFieldNames["GuestField3Name"];
                    //column.Visible = user.CodingFieldNames["GuestField3Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["GuestField3Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "GuestField4";
                    //column.Caption = user.CodingFieldNames["GuestField4Name"];
                    //column.Visible = user.CodingFieldNames["GuestField4Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["GuestField4Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });

                settings.Columns.Add(column =>
                {
                    column.FieldName = "GuestField5";
                    //column.Caption = user.CodingFieldNames["GuestField5Name"];
                    //column.Visible = user.CodingFieldNames["GuestField5Name"] != string.Empty && user.IsClient && !ParentCompany.AutoCodingFieldNames.Contains(user.CodingFieldNames["GuestField5Name"].ToString());
                    column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                });
            //}

            settings.Columns.Add("RequestNumber").CellStyle.HorizontalAlign = HorizontalAlign.Center;

            settings.Columns.Add(column =>
            {
                column.FieldName = "RequestorFirstName";
                column.Visible = false;
                ;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "RequestorLastName";
                column.Visible = false;
                ;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Requested By";
                column.UnboundType = DevExpress.Data.UnboundColumnType.String;

                column.Settings.AllowHeaderFilter = DefaultBoolean.True;
                column.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "ModificationDate";
                column.Visible = true;
                column.PropertiesEdit.DisplayFormatString = "h:mm tt EST <br /> MMM. dd, yyyy";
                column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DueTime";
                column.Visible = true;
                column.PropertiesEdit.DisplayFormatString = "h:mm tt EST <br /> MMM. dd, yyyy";
                column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            });

            settings.CustomUnboundColumnData = (s, e) =>
            {
                if (e.Column.FieldName == "Requested By")
                {
                    string firstName = (e.GetListSourceFieldValue("RequestorFirstName")).ToString();
                    string lastName = (e.GetListSourceFieldValue("RequestorLastName")).ToString();
                    e.Value = firstName + " " + lastName;
                }
                ;
                if (e.Column.FieldName == "Work Address Visible")
                {
                    string formatString = "{0}" + System.Environment.NewLine + "{1}, {2} {3}";

                    string workAddress = e.GetListSourceFieldValue("WorkAddress") != null ? e.GetListSourceFieldValue("WorkAddress").ToString() : string.Empty;
                    string workCity = e.GetListSourceFieldValue("WorkCity") != null ? e.GetListSourceFieldValue("WorkCity").ToString() : string.Empty;
                    string workState = e.GetListSourceFieldValue("WorkState") != null ? e.GetListSourceFieldValue("WorkState").ToString() : string.Empty;
                    string workZip = e.GetListSourceFieldValue("WorkZip") != null ? e.GetListSourceFieldValue("WorkZip").ToString() : string.Empty;

                    e.Value = string.Format(formatString, workAddress, workCity, workState, workZip);
                }
                if (e.Column.FieldName == "Travelers")
                {
                    List<string> guestNames = new List<string>();
                    var roomNumbers = new string[10] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" };
                    foreach (var roomNumber in roomNumbers)
                    {
                        for (int guestNumber = 1; guestNumber < 3; guestNumber++)
                            if ((e.GetListSourceFieldValue("Room" + roomNumber + "Guest1FirstName")).ToString() != string.Empty || (e.GetListSourceFieldValue("Room" + roomNumber + "Guest1LastName")).ToString() != string.Empty)
                            {
                                if (!guestNames.Contains(e.GetListSourceFieldValue("Room" + roomNumber + "Guest" + guestNumber.ToString() + "FirstName").ToString() + " " + e.GetListSourceFieldValue("Room" + roomNumber + "Guest" + guestNumber.ToString() + "LastName").ToString()))
                                {
                                    guestNames.Add(e.GetListSourceFieldValue("Room" + roomNumber + "Guest" + guestNumber.ToString() + "FirstName").ToString() + " " + e.GetListSourceFieldValue("Room" + roomNumber + "Guest" + guestNumber.ToString() + "LastName").ToString());
                                }
                            }
                    }

                    foreach (var guestName in guestNames.Where(g => !string.IsNullOrWhiteSpace(g)))
                    {
                        if (e.Value == null)
                        {
                            e.Value = guestName;
                        }
                        else
                        {
                            e.Value += ", " + guestName;
                        }
                    }

                    if (e.Value == null)
                    {
                        e.Value = string.Empty;
                    }
                }
            };


            return settings;
        }

        // Returns the settings of the exported GridView. 
        //	public static GridViewSettings GetGridSettings(this HtmlHelper Html, bool export) {
        //		var settings = new GridViewSettings {
        //			Name = "gridView",
        //			CallbackRouteValues = new { Controller = "Home", Action = "_responsiveGrid" },
        //			Width = Unit.Percentage(100)
        //		};

        //		// Export-specific settings  
        //		settings.SettingsExport.ExportSelectedRowsOnly = false;
        //		settings.SettingsExport.FileName = "Invoices.xlsx";
        //		settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;

        //		settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
        //		settings.SettingsAdaptivity.HideDataCellsAtWindowInnerWidth = 800;
        //		settings.SettingsAdaptivity.AdaptiveDetailColumnCount = 1;

        //		settings.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;

        //		settings.SettingsAdaptivity.AdaptiveDetailLayoutProperties.SettingsItems.ShowCaption =
        //			DefaultBoolean.True;
        //		settings.SettingsAdaptivity.AdaptiveDetailLayoutProperties.SettingsAdaptivity.AdaptivityMode =
        //			FormLayoutAdaptivityMode.SingleColumnWindowLimit;
        //		settings.SettingsAdaptivity.AdaptiveDetailLayoutProperties.SettingsAdaptivity
        //			.SwitchToSingleColumnAtWindowInnerWidth = 800;

        //		settings.Styles.AlternatingRow.Enabled = DefaultBoolean.Default;
        //		settings.Styles.AlternatingRow.BackColor = Color.Snow;

        //		settings.SettingsPager.Position = PagerPosition.Bottom;
        //		settings.SettingsPager.FirstPageButton.Visible = true;
        //		settings.SettingsPager.LastPageButton.Visible = true;
        //		settings.SettingsPager.PageSizeItemSettings.Visible = true;
        //		settings.SettingsPager.PageSizeItemSettings.Items = new string[] { "10", "20", "50" };
        //		settings.SettingsPager.PageSizeItemSettings.ShowAllItem = true;
        //		settings.SettingsPopup.HeaderFilter.Height = Unit.Pixel(440);
        //		settings.SettingsPopup.HeaderFilter.Width = Unit.Pixel(400);

        //		settings.Settings.ShowGroupPanel = false;
        //		settings.SettingsBehavior.AllowGroup = true;
        //		settings.SettingsBehavior.AllowSort = true;
        //		settings.SettingsBehavior.AutoExpandAllGroups = false;
        //		settings.SettingsSearchPanel.Visible = true;

        //		settings.KeyFieldName = "WebHotelInvoiceId";

        //		if (!export) {
        //			//or
        //			//if (Html != null){

        //			//*******DevExpress Please Help******
        //			settings.Columns.Add(column => {
        //				column.SetDataItemTemplateContent(m => {
        //					var hotelInvoiceKey = DataBinder.Eval(m.DataItem, "HotelInvoiceKey");
        //					bool editable = Convert.ToBoolean(DataBinder.Eval(m.DataItem, "WebHotelInvoiceIsEditable"));
        //					if (hotelInvoiceKey != null) {
        //						Html.ViewContext.Writer.Write(Html.ActionLink("View", "Show", new { HotelInvoiceId = DataBinder.Eval(m.DataItem, "HotelInvoiceKey") }));
        //					}
        //					else if (editable) {
        //						Html.ViewContext.Writer.Write(Html.ActionLink("Edit", "Edit", new { id = DataBinder.Eval(m.DataItem, "WebHotelInvoiceId") }));
        //					}
        //					else {
        //						Html.ViewContext.Writer.Write(Html.ActionLink("View", "Show", new { WebHotelInvoiceid = DataBinder.Eval(m.DataItem, "WebHotelInvoiceId") }));
        //					}

        //				});

        //			});
        //		}


        //		settings.Columns.Add(column => {
        //			column.Name = "Filter";
        //			column.FieldName = "Filter";
        //			column.Caption = "Filter";
        //		    //column.GroupIndex = 0;
        //		});

        //		settings.Columns.Add(column => {
        //			column.FieldName = "HotelName";

        //			column.Settings.AllowHeaderFilter = DefaultBoolean.True;
        //			column.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;

        //			column.Caption = "Hotel Name";
        //		});

        //		settings.Columns.Add("HotelInvoiceNumber");


        //		settings.Columns.Add(column => {
        //			column.FieldName = "InvoiceDate";

        //			column.SortIndex = 0;
        //			column.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        //			column.Settings.AllowHeaderFilter = DefaultBoolean.True;
        //			column.PropertiesEdit.DisplayFormatString = "d";
        //			column.SettingsHeaderFilter.Mode = GridHeaderFilterMode.DateRangePicker;
        //			column.Caption = "Invoice Date";

        //		});

        //		settings.Columns.Add("Nights");
        //		settings.Columns.Add("InvoiceTotal");

        //		MVCxGridViewColumn methodofPaymentColumn = settings.Columns.Add("Method of Payment");
        //		methodofPaymentColumn.UnboundType = UnboundColumnType.String;

        //		MVCxGridViewColumn statusColumn = settings.Columns.Add("Status");
        //		statusColumn.UnboundType = UnboundColumnType.String;

        //		settings.CustomUnboundColumnData = (sender, e) => {
        //			if (e.Column.ToString() == "Status") {
        //				var paymentDate = e.GetListSourceFieldValue("PaymentDate").ToString();
        //				var checkNumber = e.GetListSourceFieldValue("CheckNumber").ToString();
        //				var cardNumber = e.GetListSourceFieldValue("CardNumber").ToString();
        //				var cardTransanctionDate = e.GetListSourceFieldValue("CardTransactionDate").ToString();
        //				var priorityPayDaysToPay = e.GetListSourceFieldValue("PriorityPayDaysToPay").ToString();
        //				var creationDate = Convert.ToDateTime(e.GetListSourceFieldValue("CreationDate").ToString());
        //				var billing = e.GetListSourceFieldValue("Billing").ToString();
        //				var rejectionReason = e.GetListSourceFieldValue("RejectionReason").ToString();
        //				var completionDate = e.GetListSourceFieldValue("CompletionDate").ToString();
        //				var approvalDate = e.GetListSourceFieldValue("ApprovalDate").ToString();
        //				var hotelInvoiceKey = e.GetListSourceFieldValue("HotelInvoiceKey").ToString();
        //				string returnValue = string.Empty;

        //				if (!string.IsNullOrEmpty(hotelInvoiceKey)) {
        //					if (!string.IsNullOrEmpty(paymentDate) && !string.IsNullOrEmpty(checkNumber))
        //						returnValue = string.Format("Payment mailed via check #{0} on {1}.", checkNumber,
        //							Convert.ToDateTime(paymentDate).ToShortDateString());
        //					else if (!string.IsNullOrEmpty(cardNumber) && !string.IsNullOrEmpty(cardTransanctionDate))
        //						returnValue = string.Format("Credit card charged on {0}.",
        //							Convert.ToDateTime(cardTransanctionDate).ToShortDateString());
        //					else if (!string.IsNullOrEmpty(cardNumber))
        //						returnValue = "Credit card available.";
        //					else if (Convert.ToDouble(priorityPayDaysToPay) > 0 &&
        //							 creationDate <= creationDate.AddDays(Convert.ToDouble(priorityPayDaysToPay)) &&
        //							 MethodsOfPayment.PostPayVCardTypes.Contains(billing)) {
        //						returnValue = "Credit card will be available on " +
        //									  creationDate.AddDays(Convert.ToDouble(priorityPayDaysToPay))
        //										  .ToShortDateString();
        //					}
        //					else if (!string.IsNullOrEmpty(paymentDate))
        //						returnValue = string.Format("Payment sent on {0}.",
        //							Convert.ToDateTime(paymentDate).ToShortDateString());
        //					else
        //						returnValue =
        //							"Invoice accepted.  Awaiting processing by Creative Lodging Solutions.";
        //				}
        //				else {
        //					if (!string.IsNullOrEmpty(rejectionReason))
        //						returnValue = "Rejected: " + rejectionReason;
        //					else if (!string.IsNullOrEmpty(completionDate) && string.IsNullOrEmpty(approvalDate))
        //						returnValue =
        //							"Awaiting approval by Creative Lodging Solutions because\n you changed the rate, tax, or misc. charges within the invoice.";
        //					else if (string.IsNullOrEmpty(completionDate))
        //						returnValue = "Awaiting submission.";
        //				}
        //				e.Value = returnValue;
        //			}
        //			else if (e.Column.ToString() == "Method of Payment") {
        //				var billing = e.GetListSourceFieldValue("Billing");
        //				e.Value = MethodsOfPayment.ToSimpleDesc(billing.ToString()).ToString();
        //			}
        //		};


        //		settings.TotalSummary.Add(new ASPxSummaryItem("HotelName", SummaryItemType.Count) {
        //			DisplayFormat = "{0}",
        //			ShowInGroupFooterColumn = "HotelName"
        //		});
        //		//*******DevExpress Please Help******

        //		settings.BeforeGetCallbackResult = (sender, e) => {
        //			var grid = sender as MVCxGridView;
        //			if (grid != null) {
        //				if (Html != null) {
        //					if ((string)Html.ViewData["UseGrouping"] != null) {
        //						if ((string)Html.ViewData["UseGrouping"] == "ungroup") {
        //							grid.ClearSort();
        //						}
        //						else {
        //							grid.GroupBy(grid.DataColumns["Filter"]);
        //						}
        //					}
        //				}

        //				int itemCount = (int)grid.GetTotalSummaryValue(grid.TotalSummary["HotelName"]);
        //				grid.SettingsPager.Summary.Text = "Page {0} of {1} (" + itemCount.ToString() + " items)";
        //			}
        //		};

        //		settings.DataBound = (sender, e) => {
        //			var grid = sender as MVCxGridView;
        //			if (grid != null) {
        //				int itemCount = (int)grid.GetTotalSummaryValue(grid.TotalSummary["HotelName"]);
        //				grid.SettingsPager.Summary.Text = "Page {0} of {1} (" + itemCount.ToString() + " items)";
        //			}
        //		};
        //		settings.PreRender = (sender, e) => {
        //			var grid = sender as MVCxGridView;
        //			if (grid != null) {
        //				int itemCount = (int)grid.GetTotalSummaryValue(grid.TotalSummary["HotelName"]);
        //				grid.SettingsPager.Summary.Text = "Page {0} of {1} (" + itemCount.ToString() + " items)";
        //			}
        //		};

        //		return settings;
        //	}


    }
}