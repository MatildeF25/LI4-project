using FairTrade.Pages.Vendedores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.SqlClient;

namespace FairTrade.Pages.Feiras
{
    public class FeiraModel : PageModel
    {
        public List<ProdutoInfo> listProdutos = new List<ProdutoInfo>();
        public String erro = "";
        public String sucesso = "";
        public FeiraInfo feiraInfo = new FeiraInfo();   
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Produtos WHERE id_feira=@id_feira";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_feira", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProdutoInfo produtoInfo = new ProdutoInfo();
                                produtoInfo.id = "" + reader.GetInt32(0);
                                produtoInfo.descricao = reader.GetString(1);
                                produtoInfo.preco = reader.GetInt32(2);
                                produtoInfo.rating = reader.GetInt32(3);
                                produtoInfo.nome = reader.GetString(4);
                                produtoInfo.tipo = reader.GetString(5);
                                produtoInfo.stock = reader.GetInt32(6);
                                produtoInfo.numprodstock = reader.GetInt32(7);
                                produtoInfo.id_vendedor = reader.GetInt32(8);
                                produtoInfo.id_feira = reader.GetInt32(9);
                                produtoInfo.foto = reader.GetString(10);

                                listProdutos.Add(produtoInfo);
                            }



                        }


                    }



                }




            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Feiras WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                feiraInfo.id = "" + reader.GetInt32(0);
                                feiraInfo.nome = reader.GetString(1);
                                feiraInfo.descricao = reader.GetString(2);
                                feiraInfo.categoria = reader.GetString(3);
                                feiraInfo.regiao = reader.GetString(4);


                            }



                        }


                    }



                }




            }
            catch (Exception ex)
            {
                erro = ex.Message;

            }
        }

    }
}
