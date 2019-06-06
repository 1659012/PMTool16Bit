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
                .Include(p => p.ProjectMembers)
                .ThenInclude(p =>p.Member)
                .Include(p => p.GroupTasks)
                .WhereIf (input.ProjectOwnerId!=null, p=> p.ProjectOwnerId==input.ProjectOwnerId)
                .WhereIf(input.ProjectName.IsNotNullOrEmpty(), t => t.ProjectName.Contains(input.ProjectName));
        }
    
        public async Task<object> GetProjectDropdown(ProjectFilter input)
        {
            return await Repository.GetAll()
                .WhereIf(input.ProjectOwnerId != null, p => p.ProjectOwnerId == input.ProjectOwnerId)                
                .Select(p => new
                {
                    p.Id,
                    p.ProjectOwnerId,
                    p.ProjectName
                })
                .ToListAsync();
        }

        public async Task<Project> GetById(EntityDto<int> input)
        {
           return await Repository                
                .GetAll()
                .Include(p=>p.ProjectMembers)
                .ThenInclude(p=>p.Member)
                .FirstOrDefaultAsync(p=>p.Id==input.Id);
        }
    }
}
