//Sven Masche
//ST10030798
//PROG7311 POE Part 2
namespace poe.Models
{
    public class SessionUser //singleton class to store the loggin info of user
    {
        private static SessionUser instance;
        private User currentUser;

        private SessionUser() // constructor to prevent instantiation
        {
            
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static SessionUser Instance
        {
            get
            {
                //initialising the singleton instance
                if (instance == null)
                {
                    instance = new SessionUser();
                }
                return instance;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
