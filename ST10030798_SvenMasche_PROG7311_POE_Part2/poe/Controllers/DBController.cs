//Sven Masche
//ST10030798
//PROG7311 POE Part 2
using Microsoft.CodeAnalysis.CSharp.Syntax;
using poe.Models;
using System.Data;
using System.Data.SqlClient;

namespace poe.Controllers
{
    public class DBController //this class facilitates all database actions
    {
        //creating global variables
        SqlConnection con; 
        SqlCommand cmd;
        public DBController() //this constructor sets up the connection string with a relevant path
        {
            //getting the file path to the debug/6.0 folder (where the database is)
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            string databaseFileName = "AgriEnergyDB.mdf";
            string databasePath = System.IO.Path.Combine(path, databaseFileName);

            //creating the connection string
            con = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databasePath};Integrated Security=true;");
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public Farmer GetFarmerByUsername(string username) //method retrieves a farmer object from the database using a name
        {
            //creating the farmer
            Farmer farmer = null;

            //Open the connection
            con.Open();

            // creating a SQL command to select farmer information
            cmd = new SqlCommand("SELECT username, fullName FROM FarmerTbl WHERE username = @username", con);
            cmd.Parameters.AddWithValue("@username", username);

            //execute the command and get the reader
            SqlDataReader reader = cmd.ExecuteReader();

            //check if a row is returned
            if (reader.HasRows)
            {
                reader.Read();
                farmer = new Farmer
                {
                    userName = reader["username"].ToString(),
                    FullName = reader["fullName"].ToString(),
                    Password = null
                };
            }

            //close the reader and connection
            reader.Close();
            con.Close();

            return farmer;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public Employee GetEmployeeByUsername(string username) //gets an employee from databse using just the username
        {
            //creating the employee
            Employee employee = null;

            
            //open the connection
            con.Open();

            //createing a SQL command to select farmer information
            cmd = new SqlCommand("SELECT username, fullName FROM EmployeeTbl WHERE username = @username", con);
            cmd.Parameters.AddWithValue("@username", username); 

            //execute the command and get the reader
            SqlDataReader reader = cmd.ExecuteReader();

            //check if a row is returned
            if (reader.HasRows)
            {
                reader.Read();
                employee = new Employee
                {
                    userName = reader["username"].ToString(),
                    FullName = reader["fullName"].ToString(),
                    Password = null // Don't return password!
                };
            }

            //close the reader and connection
            reader.Close();
            con.Close();
            
            return employee;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public bool checkLogin(string username, string password, bool farmer)//check for a existing account
        {
            //creating the command, by default we check for farmer accounts
            cmd = new SqlCommand("SELECT username, password FROM FarmerTbl WHERE username = @username", con);

            //if farmer boolean is false, then we change the commnand to check for employee
            if (!farmer)
            {
                 cmd = new SqlCommand("SELECT username, password FROM EmployeeTbl WHERE username = @username", con);

            }

            //adding parameters to the command
            cmd.Parameters.AddWithValue("@username", username);
            
            //opening connection
            con.Open();

            //execute the command and get the reader
            SqlDataReader reader = cmd.ExecuteReader();

            //check if the reader has rows 
            if (reader.HasRows)
            {
                //read the data from the reader
                while (reader.Read())
                {
                    //get uername and password from teh database
                    string dbUsername = reader.GetString(0);
                    string dbPassword = reader.GetString(1);

                    //checking if the username and password provided match with the database
                    if (dbUsername == username && dbPassword == password)
                    {
                        //if they match we can close the connection and return true, to say the credentials are real
                        con.Close();
                        return true;
                    }
                }
            }

            //if it gets this far it was not a match, we close the reader and the connection, return a false value
            reader.Close();
            con.Close();
            return false;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void saveFarmer(Farmer newFarmer) //method to save a farmer to the database, using input farmer object
        {
            //creating the command
            cmd = new SqlCommand("INSERT INTO FarmerTbl (username, fullName, password) " +
                "VALUES (@UserName, @Name, @Password)", con);

            //adding parameters to the command
            cmd.Parameters.AddWithValue("@UserName", newFarmer.userName);
            cmd.Parameters.AddWithValue("@Name", newFarmer.FullName);
            cmd.Parameters.AddWithValue("@Password", newFarmer.Password);

            //open the connection
            con.Open();
            //execute the query
            cmd.ExecuteNonQuery();
            //close the connection easy peasy
            con.Close();

        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void saveProduct(Product prod) //method to save a product to the databse using parsed product object
        {
           //creating the command
            using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductTble (name, price, image, uploader," +
                " description, uploadDate, productType) " +
                "VALUES (@Name, @Price, @Image, @Uploader, @Description, @date, @type)", con))
            {
                // Adding parameters to the command
                cmd.Parameters.AddWithValue("@Name", prod.Name);
                cmd.Parameters.AddWithValue("@Price", prod.Price);
                cmd.Parameters.AddWithValue("@Image", prod.image); 
                cmd.Parameters.AddWithValue("@Uploader", SessionUser.Instance.CurrentUser.Username); 
                cmd.Parameters.AddWithValue("@Description", prod.Description);
                cmd.Parameters.AddWithValue("@date", prod.UploadDate);
                cmd.Parameters.AddWithValue("@type", prod.ProductType);

                //ppen the database connection
                con.Open();

                //execute the command
                cmd.ExecuteNonQuery();

                //close the connection
                con.Close();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public List<Product> getProducts(string username) //method to get all products belonging to a uploader (farmer)
        {
            //creating a list of products to return
            List<Product> products = new List<Product>();
            
            //creating the command
            SqlCommand command = new SqlCommand("SELECT * FROM ProductTble WHERE uploader = @uploader", con);

            //adding parameters to the command
            command.Parameters.AddWithValue("@uploader", username);

            //opening the connection
            con.Open();
               
           //executing the reader
            SqlDataReader reader = command.ExecuteReader();

            //looping through the reader 
            while (reader.Read())
            {
                //creating a product
                Product product = new Product();
                //giving it values 
                product.Id = Convert.ToInt32(reader["Id"]);
                product.Name = reader["name"].ToString();
                product.Price = reader["price"].ToString();
                product.image = (byte[])reader["image"];
                product.uploader = reader["uploader"].ToString();
                product.Description = reader["description"].ToString();
                product.ProductType = reader["productType"].ToString();
                product.UploadDate = Convert.ToDateTime(reader["uploadDate"]);
                //adding the product to a list
                products.Add(product);
            }

            //closing the reader
            reader.Close();

            //closing the connection
            con.Close() ;

            //returning the list of products 
            return products;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
