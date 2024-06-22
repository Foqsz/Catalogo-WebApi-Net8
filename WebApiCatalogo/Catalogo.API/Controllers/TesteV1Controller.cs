using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("api/v{version:apiversion}/teste")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class TesteV1Controller : ControllerBase
    {

        [HttpGet]
        public string GetVersion()
        {
            return "Teste V1 - GET - Api Versão 1.0";
        }
    }
}
