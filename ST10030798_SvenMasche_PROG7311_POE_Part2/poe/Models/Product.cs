//Sven Masche
//ST10030798
//PROG7311 POE Part 2
using System.Drawing;

namespace poe.Models
{
    public class Product //product object class
    {
        //getters and setters
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public byte[] image { get; set; }
        public string uploader { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public string ProductType { get; set; }
    }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
//----------------------------------------------------| (• ◡•)| (❍ᴥ❍ʋ)------------------------------------------------------
