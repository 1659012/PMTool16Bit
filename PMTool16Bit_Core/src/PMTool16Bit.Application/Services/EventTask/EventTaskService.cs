using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using PMTool16Bit.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class EventTaskService : AsyncCrudAppService<EventTask, EventTaskDto, int, EventTaskFilter, EventTaskCreateDto, EventTaskUpdateDto>, IEventTaskService
    {
        //private readonly IUserAppService userAppService;
        private readonly IRepository<EventTaskMember> eventTaskMemberRepository;

        private readonly IRepository<Todo> todoRepository;

        public EventTaskService(
            //IUserAppService userAppService,
            IRepository<EventTask> repository,
            IRepository<EventTaskMember> eventTaskMemberRepository,
            IRepository<Todo> todoRepository
            ) : base(repository)
        {
            //this.eventTableRepository = eventTableRepository;
            this.eventTaskMemberRepository = eventTaskMemberRepository;
            this.todoRepository = todoRepository;
        }

        public override Task<EventTaskDto> Update(EventTaskUpdateDto input)
        {
            eventTaskMemberRepository.Delete(p => p.EventTaskId == input.Id);
            todoRepository.Delete(p => p.EventTaskId == input.Id);
            return base.Update(input);
        }

        protected override IQueryable<EventTask> CreateFilteredQuery(EventTaskFilter input)
        {
            return base.CreateFilteredQuery(input);
        }

        public List<EventTaskDropdownDto> GetEventTaskDropdown(int? taskGroupId)
        {
            return Repository
                   .GetAllIncluding(p => p.TaskGroup)
                    .WhereIf(taskGroupId != null, p => p.TaskGroupId == taskGroupId)
                    .Select(p => new EventTaskDropdownDto
                    {
                        Id = p.Id,
                        TaskName = p.TaskName,
                        TaskGroupId = p.TaskGroupId,
                        TaskGroupName = p.TaskGroup.TaskGroupName
                    })
                    .ToList();
        }
    }
}