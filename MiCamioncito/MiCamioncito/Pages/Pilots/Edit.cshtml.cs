using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiCamioncito.Pages.Pilots
{
    public class EditModel : PageModel
    {
        public PilotInfo pilotInfo = new PilotInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=micamioncito;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM pilots WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pilotInfo.id = "" + reader.GetInt32(0);
                                pilotInfo.name = reader.GetString(1);
                                pilotInfo.email = reader.GetString(2);
                                pilotInfo.age = reader.GetString(3);
                                pilotInfo.phone = reader.GetString(4);
                                pilotInfo.address = reader.GetString(5);

                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            pilotInfo.id = Request.Form["id"];
            pilotInfo.name = Request.Form["name"];
            pilotInfo.email = Request.Form["email"];
            pilotInfo.age = Request.Form["age"];
            pilotInfo.phone = Request.Form["phone"];
            pilotInfo.address = Request.Form["address"];

            if (pilotInfo.name.Length == 0 || pilotInfo.email.Length == 0 ||
               pilotInfo.age.Length == 0 || pilotInfo.phone.Length == 0 ||
               pilotInfo.address.Length == 0)
            {
                errorMessage = "Todos los campos son requeridos";
                return;
            }
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=micamioncito;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE pilots " +
                                 "SET name=@name, email=@email, age=@age, phone=@phone, address=@address " +
                                 "WHERE id=@id ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", pilotInfo.name);
                        command.Parameters.AddWithValue("@email", pilotInfo.email);
                        command.Parameters.AddWithValue("@age", pilotInfo.age);
                        command.Parameters.AddWithValue("@phone", pilotInfo.phone);
                        command.Parameters.AddWithValue("@address", pilotInfo.address);
                        command.Parameters.AddWithValue("@id", pilotInfo.id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Pilots/Index");
        }
    }
}
