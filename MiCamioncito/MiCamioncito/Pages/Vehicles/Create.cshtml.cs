using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiCamioncito.Pages.Vehicles
{
    public class CreateModel : PageModel
    {
        public List<CargoInfo> listCargo = new List<CargoInfo>();
        public VehicleInfo vehicleInfo = new VehicleInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=micamioncito;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM cargo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CargoInfo CargoInfo = new CargoInfo();
                                CargoInfo.id = "" + reader.GetInt32(0);
                                CargoInfo.type_of_cargo = reader.GetString(1);
                                listCargo.Add(CargoInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void OnPost()
        {
            vehicleInfo.brand = Request.Form["brand"];
            vehicleInfo.year = Request.Form["year"];
            vehicleInfo.capacity = Request.Form["capacity"];
            vehicleInfo.consume = Request.Form["consume"];
            vehicleInfo.address = Request.Form["address"];
            vehicleInfo.depreciation = Request.Form["depreciation"];
            vehicleInfo.availiable_at = Request.Form["availiable_at"];
            vehicleInfo.availiable_to = Request.Form["availiable_to"];
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
                    String sql = "INSERT INTO vehicles" +
                        "(brand, year, capacity, consume, address, depreciation, availiable_at, availiable_to) VALUES" +
                        "(@brand, @year, @capacity, @consume, @address, @depreciation, @availiable_at, @availiable_to);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@brand", vehicleInfo.brand);
                        command.Parameters.AddWithValue("@year", vehicleInfo.year);
                        command.Parameters.AddWithValue("@capacity", vehicleInfo.capacity);
                        command.Parameters.AddWithValue("@consume", vehicleInfo.consume);
                        command.Parameters.AddWithValue("@address", vehicleInfo.address);
                        command.Parameters.AddWithValue("@depreciation", vehicleInfo.depreciation);
                        command.Parameters.AddWithValue("@availiable_at", vehicleInfo.availiable_at);
                        command.Parameters.AddWithValue("@availiable_to", vehicleInfo.availiable_to);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            vehicleInfo.brand = ""; vehicleInfo.year = ""; vehicleInfo.capacity = ""; vehicleInfo.consume = ""; vehicleInfo.address = "";
            successMessage = "El nuevo vehiculo se ha agregado correctamente";
            Response.Redirect("/Vehicles/Index");
        }
    }
    public class CargoInfo
    {
        public String id;
        public String type_of_cargo;
    }
}
