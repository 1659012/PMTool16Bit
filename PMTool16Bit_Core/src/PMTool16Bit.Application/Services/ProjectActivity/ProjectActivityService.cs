using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using PMTool16Bit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PMTool16Bit.Users;

namespace PMTool16Bit.Services
{
    public class ProjectActivityService : IProjectActivityService
    {
        private readonly IRepository<ProjectActivity> repository;
        private readonly IUserAppService userAppService;
        private readonly IProjectService projectService;
        public ProjectActivityService(
            IRepository<ProjectActivity> repository,
            IUserAppService userAppService,
            IProjectService projectService
            )
        {
            this.repository = repository;
            this.userAppService = userAppService;
            this.projectService = projectService;
        }

        public IRepository<ProjectActivity> Repository => repository;

        public List<ProjectActivityDto> GetAllRecentActivities(int maxResultCount = 5)
        {
            var projectIdList = projectService.GetProjectIdListByCurrentUser();
            if (projectIdList.Count == 0)
                return new List<ProjectActivityDto>();

            return Repository.GetAll()
                    .Where(p => projectIdList.Any(q => q == p.ProjectId))
                    .OrderByDescending(p => p.Id)
                    .Take(maxResultCount)
                    .Select(p => new ProjectActivityDto
                    {
                        Id = p.Id,
                        ProjectId = p.ProjectId,
                        Description = p.Description,
                        CreationTime = p.CreationTime
                    })                   
                   
                    .ToList();
        }

    }
}