using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MVCDemo.Models
{
    public class RegistrationBusinessModel // with using ADO.net without entity frame work
    {
        public IEnumerable<Registration> Registrationsallusers { get; set; }

        public Registration Currentuser { get; set; }

        public IEnumerable<Registration> Registrations
        {
            get
            {

                String CS = ConfigurationManager.ConnectionStrings["RegistrationContext"].ConnectionString;
                List<Registration> registrations = new List<Registration>();


                using (SqlConnection con = new SqlConnection(CS))
                {
                    string query = "getregdata";// procedures 
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while(rd.Read())
                    {
                        Registration reg = new Registration();
                        //reg.ID= Convert.ToInt32(rd["ID"]);
                        reg.Firstname = rd["Firstname"].ToString();
                        reg.Lastname = rd["Lastname"].ToString();
                        reg.Email_username = rd["Email_username"].ToString();
                        reg.Password = rd["Password"].ToString();
                        reg.Confirmpassword = rd["Confirmpassword"].ToString();
                        reg.Month = rd["Month"].ToString();
                        reg.Day= Convert.ToInt32(rd["Day"]);
                        reg.year = Convert.ToInt32( rd["year"]);
                        reg.Gender = rd["Gender"].ToString();
                        reg.Country = rd["Country"].ToString();
                        reg.Question = rd["Question"].ToString();
                        reg.Answer = rd["Answer"].ToString();
                        registrations.Add(reg);
                    }
                }
                return registrations;
            }
        }
        public void addusers(Registration reg)
        {

            String CS = ConfigurationManager.ConnectionStrings["RegistrationContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "register";// procedures using 
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Firstname", reg.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", reg.Lastname);
                cmd.Parameters.AddWithValue("@Email_username", reg.Email_username);
                cmd.Parameters.AddWithValue("@Password", reg.Password);
                cmd.Parameters.AddWithValue("@Confirmpassword", reg.Confirmpassword);
                cmd.Parameters.AddWithValue("@Month", reg.Month);
                cmd.Parameters.AddWithValue("@Day", reg.Day);
                cmd.Parameters.AddWithValue("@year", reg.year);
                cmd.Parameters.AddWithValue("@Gender", reg.Gender);
                cmd.Parameters.AddWithValue("@Country", reg.Country);
                cmd.Parameters.AddWithValue("@Question", reg.Question);
                cmd.Parameters.AddWithValue("@Answer", reg.Answer);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }



        }

    }
   
}