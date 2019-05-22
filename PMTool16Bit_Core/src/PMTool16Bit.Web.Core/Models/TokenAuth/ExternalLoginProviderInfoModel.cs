using Abp.AutoMapper;
using PMTool16Bit.Authentication.External;

namespace PMTool16Bit.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
