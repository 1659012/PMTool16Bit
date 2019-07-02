using Microsoft.AspNetCore.Antiforgery;

namespace PMTool16Bit.Web.Host.Controllers
{
#pragma warning disable CS0436 // Type conflicts with imported type

    public class AntiForgeryController : PMTool16BitControllerBase
#pragma warning restore CS0436 // Type conflicts with imported type
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