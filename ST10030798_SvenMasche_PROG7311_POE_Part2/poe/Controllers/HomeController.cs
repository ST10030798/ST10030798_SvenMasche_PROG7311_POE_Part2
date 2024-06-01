//Sven Masche
//ST10030798
//PROG7311 POE Part 2
using Microsoft.AspNetCore.Mvc;
using poe.Models;
using System.Diagnostics;
using System.Drawing;

namespace poe.Controllers 
{
    public class HomeController : Controller //this class contains the controller methods for all the views
    {
        //i wish i knew what this meant
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Profile() //Profile page controller
        {
            //getting the model
            var model = new ProfileModel();
            //setting the error on this page to false on initial load
            model.error = false;
            //returning the model
            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public IActionResult Profile(ProfileModel pfile, IFormFile ProductImage) //the on post event controller for profile
        {
            //checking if the image is not null/has content, this is a little redundent if you check the view
            if (ProductImage != null && ProductImage.Length > 0)
            {
                //creating new validation object
                Validation val = new Validation();

                //validating teh product image
                if (!val.checkFile(ProductImage)) //if its not valid
                {
                    pfile.error = true; //set the error in teh model to true
                    pfile.errorMessage = "Please upload a .png file"; //make the error message

                    return View(pfile); //return teh model in the view
                }

                //if its not invalid, convert the uploaded image to a byte array
                using (MemoryStream ms = new MemoryStream())
                {
                    ProductImage.CopyTo(ms);
                    pfile.ProductImage = Image.FromStream(ms); //assigning the image to the product image attribute
                }
            }

            //running the method in the view to create a product
            pfile.CreateProduct();

            //returning the model
            return View(pfile);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult EmployeePage() //controller for the employee page
        {
            //create model
            var model = new ProfileModel();

            //set the error value to false for initial load
            model.error = false;
           
            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public IActionResult EmployeePage(ProfileModel pFile) //controller for the on post of employee page
        {
            //create validation object
            Validation val = new Validation();

            //check if farmer exists already based on username
            if (val.checkFarmer(pFile.farmerUserName))
            {
                //if true make the error value of the model true and create a error message
                pFile.error = true;
                pFile.errorMessage = "Farmer with that username already exists";
                //return
                return View(pFile);
            }

            //if you got this far there is no error, just run create farmer method in the model
            pFile.createFarmer();

            //return
            return View(pFile);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Store() //controller for the store page
        {
            //create model
            var model = new StoreModel();
            //return 
            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost] 
        public IActionResult Store(string searchTerm, bool dateFilterRadio, DateTime startDate,
            DateTime endDate, bool typeFilterRadio, string typeFilter) //controller for the on post of the store page
        {
            //creting validation and model object
            Validation val = new Validation();
            StoreModel model = new StoreModel();

            //check if the search term is null
            if (val.checkNull(searchTerm))
            {
                //if true set error message, and error to true
                model.errorMessage = "Search term may not be null";
                model.error = true;

                return View(model);
            }

            //check if the date is being filtered
            if (dateFilterRadio) 
            {
                //if true check the dates if they are valid
                if (val.checkDates(startDate, endDate))
                {
                    //if not set the error 
                    model.errorMessage = "Start date must be earlier than end date";
                    model.error = true;

                    return View(model);
                }
            }

            //if no error so far then run the filter method in teh model. 
            model.filter(searchTerm, dateFilterRadio, startDate, endDate, typeFilterRadio, typeFilter);

            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult LoginPage() //controller for login page
        {
            //create model and return it in the view
            var model = new Login();
            model.error = false;
            return View(model);
           
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public IActionResult LoginPage(Login login) //controller for on post Login page
        {
            //check if login credentials match
            if (login.checkLog())
            {
                //if they do then run login method
                login.LogOn();

                //return to teh store page
                return RedirectToAction("Store");
            }
            else
            {
                login.error = true;
                login.errorMessage = "Login credentions not valid";
                //else return to login page
                return View(login);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Logout() //controller for logout event
        {
            //clear the session user
            SessionUser.Instance.CurrentUser = null;
            return RedirectToAction("LoginPage");
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //i think this is also just skeleton code
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //this is the controller for the error, which is also skeleton code i think
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
