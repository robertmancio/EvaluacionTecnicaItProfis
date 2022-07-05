using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiCamioncito.Pages.Pilots
{
    public class CreateModel : PageModel
    {
        public PilotInfo pilotInfo = new PilotInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            pilotInfo.name = Request.Form["name"];
            pilotInfo.email = Request.Form["email"];
            pilotInfo.age = Request.Form["age"];
            pilotInfo.phone = Request.Form["phone"];
            pilotInfo.address = Request.Form["address"];
            pilotInfo.availiable_at = Request.Form["availiable_at"];
            pilotInfo.availiable_to = Request.Form["availiable_to"];
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
                    String sql = "INSERT INTO pilots" +
                        "(name, email, age, phone, address, availiable_at, availiable_to) VALUES" +
                        "(@name, @email, @age, @phone, @address, @availiable_at, @availiable_to);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", pilotInfo.name);
                        command.Parameters.AddWithValue("@email", pilotInfo.email);
                        command.Parameters.AddWithValue("@age", pilotInfo.age);
                        command.Parameters.AddWithValue("@phone", pilotInfo.phone);
                        command.Parameters.AddWithValue("@address", pilotInfo.address);
                        command.Parameters.AddWithValue("@availiable_at", pilotInfo.availiable_at);
                        command.Parameters.AddWithValue("@availiable_to", pilotInfo.availiable_to);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            pilotInfo.name = ""; pilotInfo.email = ""; pilotInfo.age = ""; pilotInfo.phone = ""; pilotInfo.address = "";
            successMessage = "El nuevo piloto se ha agregado correctamente";
            Response.Redirect("/Pilots/Index");
        }
    }
}
