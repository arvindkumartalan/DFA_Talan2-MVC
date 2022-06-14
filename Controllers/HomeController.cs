
using DFA_Talan2.DFA_DB;
using DFA_Talan2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace DFA_Talan2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Uri apiUri = new Uri("http://localhost:50106/");
        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = apiUri;
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
           public ActionResult Login(userInfo Info)
        {
            arvindEntities1 db = new arvindEntities1();
            var res = db.userInfoes.Where(m => m.userMail == Info.userMail).FirstOrDefault();
            if (res == null)
            {
                TempData["email"] = "Email Not Found";
            }
            else
            {
                if (res.userMail == Info.userMail && res.userPassword == Info.userPassword)
                {
                    FormsAuthentication.SetAuthCookie(Info.userMail, false);
                    Session["userName"] = Info.userName;
                    Session["userMail"] = Info.userMail;
                    Session["userPassword"] = Info.userPassword;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["Password"] = "Invalid Password";
                    return View();
                }
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Sign Info)
        {
            arvindEntities1 db = new arvindEntities1();
            userInfo ui = new userInfo();
            ui.userName = Info.userName;
            ui.userMail = Info.userMail;
            ui.userGender = Info.userGender;
            ui.userPassword = Info.userPassword;
            if (Info.userId == 0)
            {
                db.userInfoes.Add(ui);
                db.SaveChanges();
                TempData["Successful"] = "Registration Successfull";
            }
            else
            {
                db.Entry(ui).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session["userMail"] = null;
            FormsAuthentication.SignOut();
            return View("Login");
        }
        public ActionResult Index()
        {
            List<Employee> emplist = new List<Employee>();
            HttpResponseMessage listStd = client.GetAsync(client.BaseAddress + "Data/Employee/Details").Result;
            if (listStd.IsSuccessStatusCode)
            {
                string data = listStd.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<List<Employee>>(data);
                foreach (var item in res)
                {
                    emplist.Add(new Employee
                    {
                        empid = item.empid,
                        empmail = item.empmail,
                        empname = item.empname,
                        empsalary = item.empsalary
                    });
                }
            }
            return View(emplist);
        }
        [HttpGet]
        public ActionResult AddEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmp(Employee obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage res = client.PostAsync(client.BaseAddress + "emoloyee/AddorEdit", content).Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            //arvindEntities1 db = new arvindEntities1();
            //employee1 emp = new employee1();
            //emp.empid = obj.empid;
            //emp.empmail = obj.empmail;
            //emp.empname = obj.empname;
            //emp.empsalary = obj.empsalary;
            //if (obj.empid == 0)
            //{
            //    db.employee1.Add(emp);
            //    db.SaveChanges();
            //}
            //else
            //{
            //    db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            //    db.SaveChanges();
            // }
                return RedirectToAction("Index");
            }
            public ActionResult Delete(int id)
        {
            var res = client.DeleteAsync(client.BaseAddress + "employee/delete" + '?' + "Id" + "=" + id.ToString()).Result;

            //arvindEntities1 db = new arvindEntities1();
            //var row = db.employee1.Where(m => m.empid == empid).First();
            //db.employee1.Remove(row);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var res = client.GetAsync(client.BaseAddress + "employee/edit" + '?' + "Id" + "=" + id.ToString()).Result;
            string data = res.Content.ReadAsStringAsync().Result;
            var emp = JsonConvert.DeserializeObject<Employee>(data);
           
            //arvindEntities1 db = new arvindEntities1();
            //var row = db.employee1.Where(m => m.empid == empid).First();
            //Employee objemp = new Employee();
            //objemp.empid = row.empid;
            //objemp.empname = row.empname;
            //objemp.empsalary = row.empsalary;
            //objemp.empmail = row.empmail;

                ViewBag.edit = "Edit";
                return View("AddEmp", emp);
            }
            [HttpGet]
        public ActionResult DashBoard()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Forgot(string mail)
        {
            arvindEntities1 db = new arvindEntities1();
            userInfo info = new userInfo();
           
            if (info.userMail == mail)
            {
                var row = db.userInfoes.Where(m => m.userMail == mail).First();
                info.userGender = row.userGender;
                info.userMail = row.userMail;
                info.userName = row.userName;
                info.userPassword = row.userPassword;

                return RedirectToAction("SignUp",info);
            }
            else
            {
                TempData["Email"] = "Email Not Found";
            }
            ViewBag.edit = "Edit";

            return RedirectToAction("Login");
        }




































        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}