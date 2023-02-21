using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace FairTrade.Pages.Feiras
{
    public class EditarModel : PageModel
    {
        public FeiraInfo feiraInfo = new FeiraInfo();
        public String erro = "";
        public String sucesso = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

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
                                feiraInfo.regiao = reader.GetString(3);
                                feiraInfo.categoria = reader.GetString(4);

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
            feiraInfo.id = Request.Form["id"];
            feiraInfo.nome = Request.Form["nome"];
            feiraInfo.descricao = Request.Form["descricao"];
            feiraInfo.regiao = Request.Form["regiao"];
            feiraInfo.categoria = Request.Form["categoria"];

            if (feiraInfo.descricao.Length == 0 || feiraInfo.nome.Length == 0 || feiraInfo.regiao.Length == 0
                || feiraInfo.categoria.Length == 0 )
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
                    String sql = "UPDATE Feiras " +
                                 "SET nome=@nome,descricao=@descricao,regiao=@regiao,categoria=@categoria " +
                                 "WHERE id=@id";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", feiraInfo.nome);
                        command.Parameters.AddWithValue("@descricao", feiraInfo.descricao);
                        command.Parameters.AddWithValue("@regiao", feiraInfo.regiao);
                        command.Parameters.AddWithValue("@categoria", feiraInfo.categoria);
                        command.Parameters.AddWithValue("@id", feiraInfo.id);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return;
            }

            Response.Redirect("/Feiras/Index");

        }
    }
}