using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiCamioncito.Pages.Vehicles
{
    public class EditModel : PageModel
    {
        public VehicleInfo vehicleInfo = new VehicleInfo();
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
                    String sql = "SELECT * FROM vehicles WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vehicleInfo.id = "" + reader.GetInt32(0);
                                vehicleInfo.brand = reader.GetString(1);
                                vehicleInfo.year = reader.GetString(2);
                                vehicleInfo.capacity = reader.GetString(3);
                                vehicleInfo.consume = reader.GetString(4);
                                vehicleInfo.address = reader.GetString(5);
                                vehicleInfo.depreciation = reader.GetString(5);

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
            vehicleInfo.id = Request.Form["id"];
            vehicleInfo.brand = Request.Form["brand"];
            vehicleInfo.year = Request.Form["year"];
            vehicleInfo.capacity = Request.Form["capacity"];
            vehicleInfo.consume = Request.Form["consume"];
            vehicleInfo.address = Request.Form["address"];
            vehicleInfo.depreciation = Request.Form["depreciation"];

            if (vehicleInfo.brand.Length == 0 || vehicleInfo.year.Length == 0 ||
                  vehicleInfo.capacity.Length == 0 || vehicleInfo.consume.Length == 0 ||
                  vehicleInfo.address.Length == 0 || vehicleInfo.depreciation.Length == 0)
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
                    String sql = "UPDATE vehicles " +
                                 "SET brand=@brand, year=@year, capacity=@capacity, consume=@consume, address=@address, depreciation=@depreciation " +
                                 "WHERE id=@id ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@brand", vehicleInfo.brand);
                        command.Parameters.AddWithValue("@year", vehicleInfo.year);
                        command.Parameters.AddWithValue("@capacity", vehicleInfo.capacity);
                        command.Parameters.AddWithValue("@consume", vehicleInfo.consume);
                        command.Parameters.AddWithValue("@address", vehicleInfo.address);
                        command.Parameters.AddWithValue("@depreciation", vehicleInfo.depreciation);
                        command.Parameters.AddWithValue("@id", vehicleInfo.id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Vehicles/Index");
        }
    }
}
