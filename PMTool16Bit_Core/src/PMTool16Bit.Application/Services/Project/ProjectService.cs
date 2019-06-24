using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class ProjectService : AsyncCrudAppService<Project, ProjectDto, int, ProjectFilter, ProjectCreateDto, ProjectUpdateDto>, IProjectService
    {
        private readonly IRepository<ProjectMember> projectMemberRepository;

        public ProjectService(
            IRepository<Project> repository,
            IRepository<ProjectMember> projectMemberRepository

            ) : base(repository)
        {
            this.projectMemberRepository = projectMemberRepository;
        }

        protected override IQueryable<Project> CreateFilteredQuery(ProjectFilter input)
        {
            return base.CreateFilteredQuery(input)
                .Include(p => p.ProjectOwner)
                //.Include(p => p.ProjectMembers)
                //.ThenInclude(p => p.Member)
                //.Include(p => p.GroupTasks)
                .WhereIf(input.Id != null, p => p.Id == input.Id)
                .WhereIf(input.MemberId != null, p => p.Id == input.MemberId
                                                     ||p.ProjectMembers.Any(q=>q.MemberId==input.MemberId)
                         )
                .WhereIf(input.ProjectOwnerId != null, p => p.ProjectOwnerId == input.ProjectOwnerId)
                .WhereIf(input.ProjectName.IsNotNullOrEmpty(), t => t.ProjectName.Contains(input.ProjectName));
        }

        public override Task<ProjectDto> Update(ProjectUpdateDto input)
        {
            projectMemberRepository.Delete(p => p.ProjectId == input.Id);
            return base.Update(input);
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

        public Task<ProjectDto> GetProjectDetailById(EntityDto<int> input)
        {
            var filter = new ProjectFilter()
            {
                Id = input.Id
            };
            var query = base.CreateFilteredQuery(filter)
                .Include(p => p.ProjectOwner)
                .Include(p => p.ProjectMembers)
                .ThenInclude(p => p.Member)
                .Include(p => p.TaskGroups)
                .ThenInclude(p => p.EventTasks)
                .ThenInclude(q => q.EventTaskMembers)
                .ThenInclude(m => m.Member)
                .Where(p => p.Id == input.Id);
            var result = query.FirstOrDefaultAsync();

            return base.Get(input);
        }       
    }
}