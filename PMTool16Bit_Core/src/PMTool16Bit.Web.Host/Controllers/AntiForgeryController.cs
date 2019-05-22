using Microsoft.AspNetCore.Antiforgery;
using PMTool16Bit.Controllers;

namespace PMTool16Bit.Web.Host.Controllers
{
    public class AntiForgeryController : PMTool16BitControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
