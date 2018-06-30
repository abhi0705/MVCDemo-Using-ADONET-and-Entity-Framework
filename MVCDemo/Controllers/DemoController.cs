using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
//using BusinessLayer;


namespace MVCDemo.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        RegistrationContext dbContext = new RegistrationContext();

        public ActionResult Index()
        {
            RegistrationContext reg = new RegistrationContext();
            List<Registration> registrations = reg.Registrations.ToList();
            return View(registrations);
        }


        public ActionResult BusinessLayer()
        {
            // Using ADO.NET in MVC  RegistrationBusinessModel
            //RegistrationBusinessModel rbm = new RegistrationBusinessModel();
            //List<Registration> registrations = rbm.Registrations.ToList();
            //return View(registrations);

            List<Registration> registrations = dbContext.Registrations.ToList();
            return View(registrations);
        }

        public ActionResult Details1(int id)
        {
            RegistrationContext reg = new RegistrationContext();
            Registration registration = reg.Registrations.Single(x => x.ID == id);
            return View(registration);
        }

        public ActionResult NotFound()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {

            return View();
       
            //ViewBag.Items = new List<SelectListItem>()
            //{
            //    new SelectListItem() {Text="January",Value="January" },
            //    new SelectListItem() {Text="February",Value="February"},
            //    new SelectListItem() {Text="March",Value="March" },
            //    new SelectListItem() {Text="April",Value="April" },
            //    new SelectListItem() {Text="May",Value="May" },
            //    new SelectListItem() {Text="June",Value="June" },
            //    new SelectListItem() {Text="July",Value="July" },
            //    new SelectListItem() {Text="August",Value="August" },
            //    new SelectListItem() {Text="September",Value="September" },
            //    new SelectListItem() {Text="October",Value="October" },
            //    new SelectListItem() {Text="November",Value="November" },
            //    new SelectListItem() {Text="December",Value="December" }
            //};

            //ViewBag.gender = new List<SelectListItem>()
            //{
            //    new SelectListItem() {Text="Male", Value="Male" },
            //    new SelectListItem() {Text="Female", Value="Female" },
            //    new SelectListItem() {Text="Other", Value="Other" },
            //    new SelectListItem() {Text="Rather not say", Value="Rather not say" }
            //};


            //ViewBag.country = new List<SelectListItem>()
            //{

            //    new SelectListItem() {Text="India", Value="India" },
            //    new SelectListItem() {Text="United States", Value="United States" },
            //    new SelectListItem() {Text="Russia", Value="Russia" },
            //    new SelectListItem() {Text="Mexico", Value="Mexico" }
            //};

            //ViewBag.question = new List<SelectListItem>()
            //{
            //    new SelectListItem() {Text="What is your pet's name", Value="What is your pet's name" },
            //    new SelectListItem() {Text="what was your first cat name", Value="what was your first cat name" },
            //    new SelectListItem() {Text="who is your favourite author", Value="who is your favourite author" },
            //    new SelectListItem() {Text="who is your favourite actor", Value="who is your favourite actor" },
            //    new SelectListItem() {Text="what is your nick name", Value="what is your nick name" }

            //};


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Registration reg)
        {
            bool Status = false;
            string Message = "";

            //model validation
            if (ModelState.IsValid)
            {
                #region email is exists or not
                var isexist = IsEmailIDExist(reg.Email_username);
                if (isexist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exists");
                    return View(reg);
                }
                #endregion

                #region password hasing
                reg.Password = hash.crypto(reg.Password);
                reg.Confirmpassword = hash.crypto(reg.Confirmpassword);
                #endregion

                #region Saving in database
                using (RegistrationContext regi = new RegistrationContext())
                    {
                        regi.Registrations.Add(reg);
                        regi.SaveChanges();

                        Message = "Registration sucessfully done" + reg.Email_username;
                        Status = true;
                    }
                #endregion
            }
            else
            {
                Message = "Invalid Request";
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;


            return View(reg);
        }
        //public ActionResult Create(FormCollection formcollection)
        //{
        //    // to display form values on the screen using keys
        //    //foreach( string key in formcollection.AllKeys)
        //    //{
        //    //    Response.Write("Key = " + key + " ");
        //    //    Response.Write(formcollection[key]);
        //    //    Response.Write("<br/>");
        //    //}

        //    Registration reg = new Registration();
        //    reg.Firstname = formcollection["Firstname"];
        //    reg.Lastname = formcollection["Lastname"];
        //    reg.Email_username = formcollection["Email_username"];
        //    reg.Password = formcollection["Password"];
        //    reg.Confirmpassword = formcollection["Confirmpassword"];
        //    reg.Month = formcollection["Month"];
        //    reg.Day = Convert.ToInt32(formcollection["Day"]);
        //    reg.year = Convert.ToInt32(formcollection["year"]);
        //    reg.Gender = formcollection["Gender"];
        //    reg.Country = formcollection["Country"];
        //    reg.Question = formcollection["Question"];
        //    reg.Answer = formcollection["Answer"];

        //    RegistrationBusinessModel rbm = new RegistrationBusinessModel();
        //    rbm.addusers(reg);

        //    return RedirectToAction("BusinessLayer");


        //}

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            var reg = dbContext.Registrations.Single(x => x.ID == id);

            return View(reg);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(Registration reg)
        {
            var oldreg = dbContext.Registrations.SingleOrDefault(x => x.ID == reg.ID);
            dbContext.Registrations.Remove(oldreg);
            dbContext.Registrations.Add(reg);
            dbContext.SaveChanges();
            return RedirectToAction("BusinessLayer");
        }


        public ActionResult Delete(int id)
        {
            var reg = dbContext.Registrations.SingleOrDefault(x => x.ID == id);
            dbContext.Registrations.Remove(reg);
            dbContext.SaveChanges();
            return RedirectToAction("BusinessLayer");
        }

        // Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Registration user)
        {
           
            using (RegistrationContext db = new RegistrationContext())
            {
                var usr = db.Registrations.SingleOrDefault(u => u.Email_username == user.Email_username);
                if (usr != null)
                {
                    if (string.Compare(hash.crypto(user.Password), usr.Password) == 0)
                    {
                        Session["ID"] = user.ID.ToString();
                        Session["Email_username"] = user.Email_username.ToString();
                        return RedirectToAction("LoggedIN");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong.");
                }
            }
            return View();
        }

       
        public ActionResult logout()

        {
            Session.Clear();
            return RedirectToAction("Login"); 
        }


        public ActionResult LoggedIN()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        [NonAction]
        public bool IsEmailIDExist(string emailid)
        {
            using (dbContext)
            {
                var v = dbContext.Registrations.Where(a => a.Email_username == emailid).FirstOrDefault();
                return v != null;
            }

        }

    }

   
}

