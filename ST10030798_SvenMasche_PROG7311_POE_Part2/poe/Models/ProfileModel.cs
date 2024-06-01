//Sven Masche
//ST10030798
//PROG7311 POE Part 2
using poe.Controllers;
using System.Drawing;
using System.Runtime.InteropServices;

namespace poe.Models
{
    public class ProfileModel //model for both the farmer profile and employeepage views. I know its dumb
    {

        //all the getters and setters you could ask for
        private readonly DBController _dbController;
        public Farmer Farmer { get; set; }
        public Employee Employee { get; set; }

        public string ProductName { get; set; }
        public string ProductPrice { get; set; } 
        public string ProductDescription { get; set; } 

        public string SelectedType { get; set; }

        public string farmerName { get; set; }
        public string farmerUserName { get; set; }

        public string farmerPassword { get; set; }

        public Image ProductImage { get; set; }

        public byte[] imageprod { get; set; }

        public string errorMessage { get; set; }

        public bool error { get; set; }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public ProfileModel() //constructor method
        {
            //creates new dbController object
            DBController db = new DBController();

            if (SessionUser.Instance.CurrentUser.Role) //checks user role, true means that the user is a farmer
            {
                //make new farmer
                Farmer = new Farmer();

                //get farmer info from database, did it likw this incase i wanted more farmer info in the database
                Farmer = db.GetFarmerByUsername(SessionUser.Instance.CurrentUser.Username); 
            }
            else //else we're dealing with an employee
            {
                //create employee object
                Employee = new Employee(); 

                //get employee info from databse 
                Employee = db.GetEmployeeByUsername(SessionUser.Instance.CurrentUser.Username);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void CreateProduct() //method to create product (only by farmers)
        {
            //create variables
            DBController db = new DBController();
            Product product = new Product();

            //setting product info from the view bound variables
            product.Name = ProductName;
            product.Price = ProductPrice;
            product.Description = ProductDescription;
            product.UploadDate = DateTime.Now;
            product.ProductType = SelectedType;

            //turning the file input from the view to a byte array and storing it into the product
            imageprod = ImageToByteArray(ProductImage);
            product.image = imageprod;

            //saving product to database
            db.saveProduct(product);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void createFarmer() //creating new farmer account, only by employees
        {
            //creating variables
            DBController db = new DBController();
            Farmer farmer = new Farmer();

            //getting farmer info from views
            farmer.userName = farmerUserName;
            farmer.FullName = farmerName;
            farmer.Password = farmerPassword;

            //run method to save the farmer
            db.saveFarmer(farmer);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public byte[] ImageToByteArray(Image image) //method takes image file and converts to byte array for storage
        {
            using (MemoryStream ms = new MemoryStream()) //new memory stream
            {
                //converting png to to memory stream 
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                //return memory stream as array
                return ms.ToArray();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
