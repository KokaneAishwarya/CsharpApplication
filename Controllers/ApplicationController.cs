using Microsoft.AspNetCore.Mvc;
using WebApplicationDailyTask.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

using System.Text;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata;

namespace WebApplicationDailyTask.Controllers
{
    public class ApplicationController : Controller
    {
        string conn;

        public ApplicationController()
        {
            var dbconfig = new ConfigurationBuilder().
                    SetBasePath(Directory.GetCurrentDirectory()).
                    AddJsonFile("appsettings.json").Build();

            conn = dbconfig["ConnectionStrings:constr"];
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_logins", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                        cmd.Parameters.AddWithValue("@Password", obj.Password);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            return RedirectToAction("Home", "Application");
                        }
                        else
                        {
                            ViewBag.error = "Emailid or Password is not correct";
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");

                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel obj)
        {

            try
            {

                string msg = "";
                if (obj.Qualification == true)
                {
                    msg = msg + "B-tech" + " ";

                }

                if (obj.qualification1 == true)
                {
                    msg = msg + "M-tech" + " ";

                }
                if (obj.qualification2 == true)
                {
                    msg = msg + "BE" + " ";

                }
                if (obj.qualification3 == true)
                {
                    msg = msg + "MBA" + " ";

                }


                if (ModelState.IsValid)
                {

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();


                        SqlCommand cmd = new SqlCommand("sp_inserts", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", obj.ID);
                        cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                        cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                        cmd.Parameters.AddWithValue("@Password", obj.Password);
                        cmd.Parameters.AddWithValue("@ConfirmPassword", obj.ConfirmPassword);
                        cmd.Parameters.AddWithValue("@Fee", obj.Fee);
                        cmd.Parameters.AddWithValue("@Dept", obj.Dept);
                        cmd.Parameters.AddWithValue("@DOB", obj.DOB);
                        cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                        cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                        cmd.Parameters.AddWithValue("@Role", obj.Role);
                        cmd.Parameters.AddWithValue("@Status", obj.Status);
                        cmd.Parameters.AddWithValue("@Qualification", msg);



                        int x = cmd.ExecuteNonQuery();
                        if (x > 0)
                        {


                            return RedirectToAction("Login", "Application");
                        }
                        else
                        {
                            ModelState.AddModelError(" ", "Something went wrong");
                            return View();
                        }
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            finally
            {

            }
            return View();
        }




        public IActionResult ForgetPassword()
        {
            return View();
        }


        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DisplayData()
        {
            List<DisplayModel> obj = getAlldata();
            return View(obj);
        }

        public List<DisplayModel> getAlldata()
        {
            List<DisplayModel> display = new List<DisplayModel>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_getalldatas", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    display.Add(
                        new DisplayModel
                        {
                            RecordID = Convert.ToInt32(dr["RecordID"].ToString()),
                            ID = Convert.ToInt32(dr["ID"].ToString()),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            EmailID = dr["EmailID"].ToString(),
                            Password = dr["Password"].ToString(),
                            ConfirmPassword = dr["ConfirmPassword"].ToString(),
                            DOB = Convert.ToDateTime(dr["DOB"].ToString()),
                            Gender = dr["Gender"].ToString(),
                            Phone = Convert.ToInt32(dr["Phone"].ToString()),
                            Role = dr["Role"].ToString(),
                            Fee = Convert.ToInt32(dr["Fee"].ToString()),
                            // Qualification = Convert.ToBoolean(dr["Qualification"].ToString()),
                            //qualification1 = Convert.ToBoolean(dr["qualification1"].ToString()),
                            // qualification2 = Convert.ToBoolean(dr["qualification2"].ToString()),
                            //qualification3 = Convert.ToBoolean(dr["qualification3"].ToString()),

                            Dept = dr["Dept"].ToString(),
                            Status = dr["Status"].ToString()


                        });
                }

            }
            return display;
        }



        [HttpGet]

        public IActionResult ViewData(int? RecordID)
        {
            ViewModel obj = getdatabyIDS((int)RecordID);
            return View(obj);
        }


        public ViewModel getdatabyIDS(int RecordID)
        {
            ViewModel obj = null;
            try
            {
                if (RecordID == null)
                {

                }
                else
                {
                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_getdatabyIDS", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecordID", RecordID);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            obj = new ViewModel();
                            obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                            obj.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                            obj.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
                            obj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                            obj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                            obj.ConfirmPassword = ds.Tables[0].Rows[i]["ConfirmPassword"].ToString();
                            obj.Fee = Convert.ToInt32(ds.Tables[0].Rows[i]["Fee"].ToString());
                            obj.DOB = Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"].ToString());
                            obj.Dept = ds.Tables[0].Rows[i]["Dept"].ToString();

                            obj.Status = Convert.ToBoolean(ds.Tables[0].Rows[i]["Status"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return obj;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(IFormFile SingleFile)
        {
            if (ModelState.IsValid)
            {
                if (SingleFile != null && SingleFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", SingleFile.FileName);
                    //Using Buffering
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        // The file is saved in a buffer before being processed
                        await SingleFile.CopyToAsync(stream);
                    }
                    //Using Streaming
                    //using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    //{
                    //    await SingleFile.CopyToAsync(stream);
                    //}
                    // Process the file here (e.g., save to the database, storage, etc.)
                    return View("View");
                }
            }
            return View("Index");
        }




        












    }


}


           

    
