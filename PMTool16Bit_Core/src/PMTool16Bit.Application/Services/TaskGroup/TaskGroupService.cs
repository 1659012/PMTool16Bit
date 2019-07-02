using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using PMTool16Bit.Models;
using System.Collections.Generic;
using System.Linq;

namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class TaskGroupService : AsyncCrudAppService<TaskGroup, TaskGroupDto, int, TaskGroupFilter, TaskGroupCreateDto, TaskGroupUpdateDto>, ITaskGroupService
    {
        //private readonly IUserAppService userAppService;
        //private readonly IRepository<EventTable> eventTableRepository;

        public TaskGroupService(
             IRepository<TaskGroup> repository
            //IUserAppService userAppService,
            //IRepository<EventTable> eventTableRepository

            ) : base(repository)
        {
            //this.eventTableRepository = eventTableRepository;
        }

        protected override IQueryable<TaskGroup> CreateFilteredQuery(TaskGroupFilter input)
        {
            return base.CreateFilteredQuery(input)
                //.Include(p => p.Project)
                .WhereIf(input.ProjectId != null, p => p.ProjectId == input.ProjectId)
                ;
        }

        public List<TaskGroupDropdownDto> GetTaskGroupDropdown(int projectId)
        {
            return Repository
                    .GetAll()
                    .Where(p => p.ProjectId == projectId)
                    .Select(p => new TaskGroupDropdownDto
                    {
                        Id = p.Id,
                        TaskGroupName = p.TaskGroupName
                    })
                    .ToList();
        }
    }
}