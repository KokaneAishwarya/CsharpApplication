using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace WebApplicationDailyTask.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Id required")]//check for null value
        [RegularExpression("[0-9]", ErrorMessage = "enter digit only")]
        public int ID { get; set; }

        [Required(ErrorMessage = "FirstName required")]
        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Enter letter only")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName required")]

        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Enter letter only")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "Must be at least 8 characters" +
            "Must contain at least one one lower case letter," +
            "One upper case letter," +
            "One digit and one special character" +
            "Valid special characters are – @#$%^&+=")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "Must be at least 8 characters" +
            "Must contain at least one one lower case letter," +
            "One upper case letter," +
            "One digit and one special character" +
            "Valid special characters are – @#$%^&+=")]
        public string ConfirmPassword { get; set; }


        public DateTime DOB { get; set; }

        public string Gender { get; set; }


        public int Phone { get; set; }


        public string Dept { get; set; }


        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

     

        [Required(ErrorMessage = "Salary must be between 3000 and 100000000")]
        [Range(3000, 10000000, ErrorMessage = "Salary must be between 3000 and 100000000")]
        public int Fee { get; set; }


        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Qualification is required")]

        public bool Qualification { get; set; }
       
        public bool qualification1 { get; set; }

        public bool qualification2 { get; set; }

        public bool qualification3 { get; set; }


    }

    public class ViewModel
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



        public bool Status { get; set; }



        //public bool Qualification { get; set; }

       // public bool qualification1 { get; set; }

      //  public bool qualification2 { get; set; }

       // public bool qualification3 { get; set; }


    }
}
