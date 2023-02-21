using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace FairTrade.Pages.Vendedores
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<VendedorInfo> listVendedores = new List<VendedorInfo>();
        public void OnGet()
        {
            try {
                string connectionstring = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True; Encrypt=False;";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                   connection.Open();
                    String sql = "SELECT * FROM Vendedores";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VendedorInfo vendedorInfo= new VendedorInfo();
                                vendedorInfo.id = "" + reader.GetInt32(0);
                                vendedorInfo.username= reader.GetString(1);
                                vendedorInfo.nome= reader.GetString(2);
                                vendedorInfo.email= reader.GetString(3);
                                vendedorInfo.password= reader.GetString(4);
                                vendedorInfo.rating= reader.GetInt32(5);
                                vendedorInfo.metodo_pagamento = reader.GetString(6);
                                vendedorInfo.funcionario= reader.GetString(7);
                                vendedorInfo.id_feira= reader.GetInt32(8);
                                vendedorInfo.foto = reader.GetString(9);

                                listVendedores.Add(vendedorInfo);
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

    public class VendedorInfo
    {
        public string id;
        public string username;
        public string nome;
        public string email;
        public string password;
        public int rating;
        public string metodo_pagamento;
        public string funcionario;
        public int id_feira;
        public string foto;
    }
}
