using System.ComponentModel.DataAnnotations;

namespace WebApplicationDailyTask.Models
{
    public class DisplayModel
    {
        public int RecordID { get; set; }
        public int ID { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string EmailID { get; set; }


        public string Password { get; set; }



        public string ConfirmPassword { get; set; }


        public DateTime DOB { get; set; }

        public string Gender { get; set; }


        public int Phone { get; set; }


        public string Dept { get; set; }



        public string Role { get; set; }




        public int Fee { get; set; }



        public string Status { get; set; }



        // public bool Qualification { get; set; }

        // public bool qualification1 { get; set; }

        // public bool qualification2 { get; set; }

        //public bool qualification3 { get; set; }


    }
}
