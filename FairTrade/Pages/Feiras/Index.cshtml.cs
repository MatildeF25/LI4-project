using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace FairTrade.Pages.Feiras
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<FeiraInfo> listFeiras = new List<FeiraInfo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Feiras";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FeiraInfo feiraInfo = new FeiraInfo();
                                feiraInfo.id = "" + reader.GetInt32(0);
                                feiraInfo.nome = reader.GetString(1);
                                feiraInfo.descricao = reader.GetString(2);
                                feiraInfo.categoria = reader.GetString(3);
                                feiraInfo.regiao = reader.GetString(4);

                                listFeiras.Add(feiraInfo);
                            }
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.ToString());

            }
        }
    }

    public class FeiraInfo
    {
        public string id;
        public string nome;
        public string descricao;
        public string regiao;
        public string categoria;

    }
}
   
