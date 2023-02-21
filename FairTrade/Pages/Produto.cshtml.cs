using FairTrade.Pages.Feiras;
using FairTrade.Pages.Vendedores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace FairTrade.Pages
{
    public class ProdutoModel : PageModel
    {
        public ProdutoInfo produtoInfo = new ProdutoInfo();
        public String erro = "";
        public String sucesso = "";
        public FeiraInfo feiraInfo = new FeiraInfo();
        public VendedorInfo vendedorInfo = new VendedorInfo();
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Produtos WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
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
                            }



                        }


                    }



                }




            }
            catch (Exception ex)
            {
                erro = ex.Message;

            }

            string id3 = "" + produtoInfo.id_feira;
           
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Feiras WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id3);
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

            string id2 = "" + produtoInfo.id_vendedor;
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Vendedores WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id2);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                vendedorInfo.id = "" + reader.GetInt32(0);
                                vendedorInfo.username = reader.GetString(1);
                                vendedorInfo.nome = reader.GetString(2);
                                vendedorInfo.email = reader.GetString(3);
                                vendedorInfo.password = reader.GetString(4);
                                vendedorInfo.rating = reader.GetInt32(5);
                                vendedorInfo.metodo_pagamento = reader.GetString(6);
                                vendedorInfo.funcionario = reader.GetString(7);
                                vendedorInfo.id_feira = reader.GetInt32(8);
                                vendedorInfo.foto = reader.GetString(9);


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
