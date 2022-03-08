using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using RestMoney.Dtos;

namespace RestMoney.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet("/")]
        public ActionResult<PingDto> Ping()
        {
            var assembly = Assembly.GetEntryAssembly();
            return new PingDto
            {
                Name = assembly?.GetName().Name ?? "",
                Version = assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? ""
            };
        }
    }
}
