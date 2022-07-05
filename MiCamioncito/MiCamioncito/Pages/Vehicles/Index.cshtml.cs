using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiCamioncito.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        public List<VehicleInfo> listVehicles = new List<VehicleInfo>(); 
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=micamioncito;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM vehicles";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VehicleInfo VehicleInfo = new VehicleInfo();
                                VehicleInfo.id = "" + reader.GetInt32(0);
                                VehicleInfo.brand = reader.GetString(1);   
                                VehicleInfo.year = reader.GetString(2);
                                VehicleInfo.capacity = reader.GetString(3);
                                VehicleInfo.consume = reader.GetString(4);
                                VehicleInfo.address = reader.GetString(5);
                                VehicleInfo.depreciation = reader.GetString(6);


                                listVehicles.Add(VehicleInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
    public class VehicleInfo
    {
        public String id;
        public String brand;
        public String year;
        public String capacity;
        public String consume;
        public String address;
        public String depreciation;
        public String availiable_at;
        public String availiable_to;

    }
}
