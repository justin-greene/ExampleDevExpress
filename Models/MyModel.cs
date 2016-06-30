using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace DXWebApplication5.Models {

    public class MyModel {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        [Key]
        public int CustomerID { get; set; }
        public MyModel() {
        }
        public MyModel(string first, string last, DateTime birth, int id) {
            CustomerID = id;
            FirstName = first;
            LastName = last;
            BirthDate = birth;
        }

    }
    public class CustomerList {
        public List<MyModel> GetTypedListModel() {
            if (HttpContext.Current.Session["ModelList"] == null) {
                List<MyModel> typedList = new List<MyModel>();
                for (int index = 0; index < 100; index++) {
                    typedList.Add(new MyModel("Name" + index, "Last" + index, new DateTime(1990, 10, 1), index));
                }
                HttpContext.Current.Session["ModelList"] = typedList;
            }
            return (List<MyModel>)HttpContext.Current.Session["ModelList"];
        }
        public void AddCustomer(MyModel model) {
            List<MyModel> list = GetTypedListModel();
            model.CustomerID = list.Max(m=>m.CustomerID)+1;

            list.Add(model);
        }

        public void UpdateCustomer(MyModel modelInfo) {
            MyModel editedModel = GetTypedListModel().Where(m => m.CustomerID == modelInfo.CustomerID).First();

            editedModel.FirstName = modelInfo.FirstName;
            editedModel.BirthDate = modelInfo.BirthDate;
            editedModel.LastName = modelInfo.LastName;
        }

        public void DeleteCustomer(int personId) {
            List<MyModel> customers = GetTypedListModel();
            customers.Remove(customers.Where(m => m.CustomerID == personId).First());
        }

    }
}