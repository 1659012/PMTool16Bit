using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class ProjectService : AsyncCrudAppService<Project, ProjectDto, int, ProjectFilter, ProjectCreateDto, ProjectUpdateDto>, IProjectService
    {
        //private readonly IUserAppService userAppService;
        //private readonly IRepository<EventTable> eventTableRepository;

        public ProjectService(
             IRepository<Project> repository
            //IUserAppService userAppService,
            //IRepository<EventTable> eventTableRepository

            ) : base(repository)
        {           
            //this.eventTableRepository = eventTableRepository;
        }

        protected override IQueryable<Project> CreateFilteredQuery(ProjectFilter input)
        {
            return base.CreateFilteredQuery(input)
                .Include(p => p.EventTables)
                .WhereIf (input.AdminId!=null, p=> p.AdminId==input.AdminId)
                .WhereIf(!string.IsNullOrEmpty(input.ProjectName), t => t.ProjectName.Contains(input.ProjectName));
        }
    
        public async Task<object> GetProjectDropdown(ProjectFilter input)
        {
            return await Repository.GetAll()
                .WhereIf(input.AdminId != null, p => p.AdminId == input.AdminId)                
                .Select(p => new
                {
                    p.Id,
                    p.AdminId,
                    p.ProjectName
                })
                .ToListAsync();
        }

    
    }
}
