//Sven Masche
//ST10030798
//PROG7311 POE Part 2
using Microsoft.AspNetCore.Mvc.RazorPages;
using poe.Controllers;

namespace poe.Models
{
    public class StoreModel : PageModel //model for the store page
    {

        //getters and setters
        private readonly DBController _dbController;
        public bool dateFilter {  get; set; }
        public bool typeFilter { get; set; }

        public List<Product> Products { get; set; } 
        public List<Farmer> Farmers { get; set; }

        public string errorMessage { get; set; }

        public bool error { get; set; }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public StoreModel() //constructor 
        {
         /*   //check user role for farmer, if true we can already get a list of thier products
            if (SessionUser.Instance.CurrentUser.Role)
            {*/
                //create dbcontroller object
                DBController db = new DBController();
                //get the list of products all pertaining to the current user name
                Products = db.getProducts(SessionUser.Instance.CurrentUser.Username);
          //  }
           
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Method to filter out the products
        public void filter(string username, bool dateFilter, DateTime startDate, 
            DateTime endDate, bool typeFilter, string type)
        {
            //creating db controller object
            DBController db = new DBController();

            //get products based on username from databse and store inside a new list
            var filteredProducts = db.getProducts(username);

            //if we're filtering for date
            if (dateFilter)
            {
                //alter the products list to only show the products in teh date range
                filteredProducts = filteredProducts.Where(p => p.UploadDate >= startDate && p.UploadDate <= endDate).ToList();
            }

            //if we're filtering for product type
            if (typeFilter)
            {
                //Alter products list to only provide products with that type
                filteredProducts = filteredProducts.Where(p => p.ProductType == type).ToList();
                
            }
            //set the global products list to the filtered one
            Products = filteredProducts;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
