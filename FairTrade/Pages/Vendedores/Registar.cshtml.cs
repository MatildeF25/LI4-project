using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace FairTrade.Pages.Vendedores
{
    public class RegistarModel : PageModel
    {
        public VendedorInfo vendedorInfo = new VendedorInfo();
        public String erro = "";
        public String sucesso = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            vendedorInfo.username = Request.Form["username"];
            vendedorInfo.nome = Request.Form["nome"];
            vendedorInfo.email = Request.Form["email"];
            vendedorInfo.password = Request.Form["password"];
            vendedorInfo.rating = Int32.Parse(Request.Form["rating"]);
            vendedorInfo.metodo_pagamento = Request.Form["modo_pagamento"];
            vendedorInfo.funcionario = Request.Form["funcionario"];
            vendedorInfo.id_feira = Int32.Parse(Request.Form["id_feira"]);
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
                using (SqlConnection connection= new SqlConnection(connectionString)) { 
                    connection.Open();
                    String sql = "INSERT INTO Vendedores " +
                                 "(username,nome,email,password,rating,modo_pagamento,funcionario_username,id_feira,foto) VALUES" +
                                 "(@username,@nome,@email,@password,@rating,@modo_pagamento,@funcionario_username,@id_feira,@foto);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", vendedorInfo.username);
                        command.Parameters.AddWithValue("@nome", vendedorInfo.nome);
                        command.Parameters.AddWithValue("@email", vendedorInfo.email);
                        command.Parameters.AddWithValue("@password", vendedorInfo.password);
                        command.Parameters.AddWithValue("@rating", vendedorInfo.rating);
                        command.Parameters.AddWithValue("@modo_pagamento", vendedorInfo.metodo_pagamento);
                        command.Parameters.AddWithValue("@funcionario_username", vendedorInfo.funcionario);
                        command.Parameters.AddWithValue("@id_feira", vendedorInfo.id_feira);
                        command.Parameters.AddWithValue("@foto", vendedorInfo.foto);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch(Exception ex)
            {
                erro = ex.Message;
                return;
            }

            vendedorInfo.username = ""; vendedorInfo.nome = ""; vendedorInfo.email = ""; vendedorInfo.password = "";
            vendedorInfo.rating = 0;  vendedorInfo.metodo_pagamento = ""; vendedorInfo.funcionario = ""; vendedorInfo.id_feira = -1; vendedorInfo.foto = "";
            sucesso = "Novo Vendedor adicionado com sucesso!";
            Response.Redirect("/Vendedores/Index");
        }
    }
}
