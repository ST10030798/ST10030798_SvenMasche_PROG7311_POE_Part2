//Sven Masche
//ST10030798
//PROG7311 POE Part 2
using poe.Controllers;
using System.Reflection;

namespace poe.Models
{
    public class Validation //vallidation class, holds all validation methods
    {
        //method to check if input string is null
        public bool checkNull(string input)
        {
            //set boolean flag
            bool bflag = false;

            //check if input is null
            if (input == null)
            {
                //set bflag
                bflag = true;
            }
            //return bFlag
            return bflag;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //method to check if start date is before end date
        public bool checkDates(DateTime start, DateTime end) 
        { 
            //set bool flag
            bool bflag = false;

            //check if start is greater equal to end date
            if (start >= end)
            {
                //set flag
                bflag = true;
            }
            return bflag;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Method to check if file is a .png
        public bool checkFile(IFormFile file)
        {
            //gets the file extension
            string extension = Path.GetExtension(file.FileName).ToLower();
            //list of valid extensions, i only did png due to time constraints
            List<string> validImageExtensions = new List<string> {".png" };

            //return if extension is present in list 
            return validImageExtensions.Contains(extension);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Method to see if farmer exists in database
        public bool checkFarmer(string userName)
        {
            //create variables
            DBController dbController = new DBController();
            bool bflag = false;
            Farmer farmer = new Farmer();

            //get farmer based on username
            farmer = dbController.GetFarmerByUsername(userName);

            //check if null is returned
            if (farmer != null) 
            {  
                //true means there is no farmer
                bflag = true;
            }

            return bflag;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
