using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("api/v{version:apiversion}/teste")]
    [ApiController]
    [ApiVersion("2.0")]
    public class TesteV2Controller : ControllerBase
    {
        [HttpGet]
        public string GetVersion()
        {
            return "Teste V2 - GET - Api Versão 2.0";
        }
    }
}
