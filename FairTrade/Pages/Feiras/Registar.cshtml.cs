using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace FairTrade.Pages.Feiras
{
    public class RegistarModel : PageModel
    {
        public FeiraInfo feiraInfo = new FeiraInfo();
        public String erro = "";
        public String sucesso = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            feiraInfo.nome = Request.Form["nome"];
            feiraInfo.categoria = Request.Form["categoria"];
            feiraInfo.regiao = Request.Form["regiao"];
            feiraInfo.descricao = Request.Form["descricao"];

            if (feiraInfo.nome.Length == 0 || feiraInfo.categoria.Length == 0 || feiraInfo.regiao.Length == 0
                || feiraInfo.descricao.Length == 0 )
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
                    String sql = "INSERT INTO Feiras " +
                                 "(nome,descricao,regiao,categoria) VALUES" +
                                 "(@nome,@descricao,@regiao,@categoria);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@categoria", feiraInfo.categoria);
                        command.Parameters.AddWithValue("@nome", feiraInfo.nome);
                        command.Parameters.AddWithValue("@descricao", feiraInfo.descricao);
                        command.Parameters.AddWithValue("@regiao", feiraInfo.regiao);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return;
            }

            feiraInfo.nome = ""; feiraInfo.descricao = ""; feiraInfo.regiao = ""; feiraInfo.categoria = "";
            sucesso = "Nova Feira adicionada com sucesso!";
            Response.Redirect("/Feiras/Index");
        }
    }
}
