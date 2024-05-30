using WebApiCatalogo.Catalogo.Application.Interface;

namespace WebApiCatalogo.Catalogo.Application.Services
{
    public class MeuServico : IMeuServico
    {
        public string Saudacao(string nome)
        {
            return $"Bem-Vindo, {nome} \n\n {DateTime.Now}";
        }
    }
}
