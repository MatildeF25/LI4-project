using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace FairTrade.Pages.Vendedores
{
    public class EditarModel : PageModel
    {
        public VendedorInfo vendedorInfo = new VendedorInfo();
        public String erro = "";
        public String sucesso = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try 
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True; Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Vendedores WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader()) 
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

        public void OnPost() 
        {
            vendedorInfo.id = Request.Form["id"];
            vendedorInfo.username = Request.Form["username"];
            vendedorInfo.nome = Request.Form["nome"];
            vendedorInfo.email = Request.Form["email"];
            vendedorInfo.password = Request.Form["password"];
            vendedorInfo.rating = Int32.Parse(Request.Form["rating"]);
            vendedorInfo.metodo_pagamento = Request.Form["modo_pagamento"];
            vendedorInfo.funcionario = Request.Form["funcionario"];
            vendedorInfo.foto = Request.Form["foto"];

            if (vendedorInfo.username.Length == 0 || vendedorInfo.nome.Length == 0 || vendedorInfo.email.Length == 0
                || vendedorInfo.password.Length == 0 || vendedorInfo.metodo_pagamento.Length == 0 || vendedorInfo.funcionario.Length == 0 || vendedorInfo.foto.Length == 0)
            {
                erro = "É necessário preencher todos os campos";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BD;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Vendedores " +
                                 "SET username=@username,nome=@nome,email=@email,password=@password,rating=@rating,modo_pagamento=@modo_pagamento,funcionario_username=@funcionario_username,id_feira=@id_feira,foto=@foto " + 
                                 "WHERE id=@id";
                                 

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", vendedorInfo.username);
                        command.Parameters.AddWithValue("@nome", vendedorInfo.nome);
                        command.Parameters.AddWithValue("@email", vendedorInfo.email);
                        command.Parameters.AddWithValue("@password", vendedorInfo.password);
                        command.Parameters.AddWithValue("@rating", vendedorInfo.rating);
                        command.Parameters.AddWithValue("@modo_pagamento", vendedorInfo.metodo_pagamento);
                        command.Parameters.AddWithValue("@funcionario_username", vendedorInfo.funcionario);
                        command.Parameters.AddWithValue("@id", vendedorInfo.id);
                        command.Parameters.AddWithValue("@id_feira", vendedorInfo.id_feira);
                        command.Parameters.AddWithValue("@foto", vendedorInfo.foto);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return;
            }

            Response.Redirect("/Vendedores/Index");

        }
    }
}
