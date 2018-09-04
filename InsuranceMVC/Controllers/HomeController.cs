using InsuranceMVC.Models;
using InsuranceMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString = @"Data Source=DESKTOP-G9LBFJ9\SQLEXPRESS;Initial Catalog=InsuranceQuote;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Quote(string firstName, string lastName, string emailAddress, string dateOfBirth, string carYear, string carMake, 
                            string carModel, string dui, string speedingTickets, string coverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(dateOfBirth) ||
                string.IsNullOrEmpty(carMake) || string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(dui) || string.IsNullOrEmpty(speedingTickets) ||
                string.IsNullOrEmpty(coverage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                

                string queryString = @"INSERT INTO QuoteInfo (FirstName, LastName, EmailAddress, DateOfBirth,
                                     CarYear, CarMake, CarModel, Dui, SpeedingTickets, Coverage) VALUES
                                     (@FirstName, @LastName, @EmailAddress, @DateOfBirth,
                                     @CarYear, @CarMake, @CarModel, @Dui, @SpeedingTickets, @Coverage)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                    command.Parameters.Add("@DateOfBirth", SqlDbType.VarChar);
                    command.Parameters.Add("@CarYear", SqlDbType.VarChar);
                    command.Parameters.Add("@CarMake", SqlDbType.VarChar);
                    command.Parameters.Add("@CarModel", SqlDbType.VarChar);
                    command.Parameters.Add("@Dui", SqlDbType.VarChar);
                    command.Parameters.Add("@SpeedingTickets", SqlDbType.VarChar);
                    command.Parameters.Add("@Coverage", SqlDbType.VarChar);

                    command.Parameters["@FirstName"].Value = firstName;
                    command.Parameters["@LastName"].Value = lastName;
                    command.Parameters["@EmailAddress"].Value = emailAddress;
                    command.Parameters["@DateOfBirth"].Value = dateOfBirth;
                    command.Parameters["@CarYear"].Value = carYear;
                    command.Parameters["@CarMake"].Value = carMake;
                    command.Parameters["@CarModel"].Value = carModel;
                    command.Parameters["@Dui"].Value = dui;
                    command.Parameters["@SpeedingTickets"].Value = speedingTickets;
                    command.Parameters["@Coverage"].Value = coverage;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                    return View("Success");
            }
        }
        public ActionResult Admin()
       {
            using (InsuranceQuoteEntities db = new InsuranceQuoteEntities())
            {
                var quoteVMs = new List<QuoteVM>();
                foreach (var quote in quoteVMs)
                {
                    var quoteVM = new QuoteVM();
                    quoteVM.FirstName = quote.FirstName;
                    quoteVM.LastName = quote.LastName;
                    quoteVM.EmailAddress = quote.EmailAddress;
                    quoteVMs.Add(quoteVM);
                }

                return View(quoteVMs);
            }

        }
    }
}