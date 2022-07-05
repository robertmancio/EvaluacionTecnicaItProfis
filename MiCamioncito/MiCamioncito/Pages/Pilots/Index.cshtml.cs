using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiCamioncito.Pages.Pilots
{
    public class IndexModel : PageModel
    {
        public List<PilotInfo> listPilots = new List<PilotInfo>(); 
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=micamioncito;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM pilots";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PilotInfo pilotInfo = new PilotInfo();
                                pilotInfo.id = "" + reader.GetInt32(0);
                                pilotInfo.name = reader.GetString(1);   
                                pilotInfo.email = reader.GetString(2);
                                pilotInfo.age = reader.GetString(3);
                                pilotInfo.phone = reader.GetString(4);
                                pilotInfo.address = reader.GetString(5);
                                pilotInfo.availiable_at = reader.GetDateTime(6).ToString();
                                pilotInfo.availiable_to = reader.GetDateTime(7).ToString();

                                listPilots.Add(pilotInfo);
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
    public class PilotInfo
    {
        public String id;
        public String name;
        public String email;
        public String age;
        public String phone;
        public String address;
        public String availiable_at;
        public String availiable_to;
    }
}
