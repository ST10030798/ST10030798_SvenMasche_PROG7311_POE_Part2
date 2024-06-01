//Sven Masche
//ST10030798
//PROG7311 POE Part 2
using Microsoft.AspNetCore.Mvc;
using poe.Controllers;
using System.Drawing;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;

namespace poe.Models
{
    public class Login //Login Model class
    {
        //getters and setters that bind with LoginPage view 
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Role { get; set; }

        public bool error { get; set; }

        public string errorMessage { get; set; }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public bool checkLog() //method to check login credentials
        {
            //creating variables
            bool check = false;
            DBController db = new DBController();

            //checking if Username and password are in teh database
            check = db.checkLogin(Username, Password, Role);

            return check; //returns true if there was a match
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void LogOn() //method to set the current Session User
        {
            //setting the session user using the inputs from the view
            SessionUser.Instance.CurrentUser = new User
            {
                Username = Username,
                Password = Password,
                Role = Role
            };
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
