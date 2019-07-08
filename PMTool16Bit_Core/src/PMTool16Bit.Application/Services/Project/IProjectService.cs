using Abp.Application.Services;
using System.Collections.Generic;

namespace PMTool16Bit.Services
{
    public interface IProjectService : IApplicationService
    {
        List<int> GetProjectIdListByCurrentUser();

    }
}