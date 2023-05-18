using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Car_Sale.Models;

namespace Online_Car_Sale.Controllers
{
    public class AccountController : Controller
    {

        //Registration
        [HttpGet]
        public ActionResult Add(int Id = 0)
        {
            Seller sellerModel = new Seller();
            return View(sellerModel);
        }

        [HttpPost]
        public ActionResult Add(Seller sellerModel)
        {
            using (DbModels dbModel = new DbModels())
            {
                if(dbModel.Sellers.Any(x => x.Username == sellerModel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exist";
                    return View("Add", sellerModel);
                }
                dbModel.Sellers.Add(sellerModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("Add", new Seller());
        }

        //Logion
        public ActionResult sellerLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult sellerLogin(Online_Car_Sale.Models.Seller sellerModel) 
        {
            using (DbModels dbModel = new DbModels())
            {
                var sellerDetail = dbModel.Sellers.Where(x => x.Username == sellerModel.Username && x.Password == sellerModel.Password).FirstOrDefault();
                if(sellerDetail == null)
                {
                    sellerModel.LoginErrorMsg = "Invalid Username or Password";
                    return View("sellerLogin", sellerModel);
                }
                else
                {
                    Session["Id"] = sellerDetail.Id;
                    return RedirectToAction("", "Home");
                }
            }
                return View();
        }

    }
}