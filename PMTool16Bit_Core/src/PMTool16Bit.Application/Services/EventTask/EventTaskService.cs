using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using PMTool16Bit.Models;
using PMTool16Bit.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class EventTaskService : AsyncCrudAppService<EventTask, EventTaskDto, int, EventTaskFilter, EventTaskCreateDto, EventTaskUpdateDto>, IEventTaskService
    {
        private readonly IUserAppService userAppService;
        private readonly IProjectService projectService;

        private readonly IRepository<EventTaskMember> eventTaskMemberRepository;        
        private readonly IRepository<Todo> todoRepository;
        private readonly IRepository<TaskGroup> taskGroupRepository;
        private readonly IRepository<Project> projectRepository;
        private readonly IRepository<ProjectActivity> projectActivityRepository;

        public EventTaskService(
            IUserAppService userAppService,
            IProjectService projectService,

            IRepository<EventTask> repository,
            IRepository<EventTaskMember> eventTaskMemberRepository,
            IRepository<Todo> todoRepository,
            IRepository<TaskGroup> taskGroupRepository,
            IRepository<Project> projectRepository,
            IRepository<ProjectActivity> projectActivityRepository
            ) : base(repository)
        {
            this.userAppService = userAppService;
            this.eventTaskMemberRepository = eventTaskMemberRepository;
            this.todoRepository = todoRepository;
            this.taskGroupRepository = taskGroupRepository;
            this.projectRepository = projectRepository;
            this.projectActivityRepository = projectActivityRepository;
        }

        protected override IQueryable<EventTask> CreateFilteredQuery(EventTaskFilter input)
        {
            return base.CreateFilteredQuery(input);
        }

        public override Task<EventTaskDto> Create(EventTaskCreateDto input)
        {
            var project = GetProject(input.TaskGroupId);
            var userName = userAppService.GetCurrentUserName();
            var description = userName + " created task " + input.TaskName + " in project " + project.ProjectName;
            var projectActivity = new ProjectActivity
            {
                ProjectId = project.Id,
                Description = description
            };
            projectActivityRepository.InsertAsync(projectActivity);
            CurrentUnitOfWork.SaveChanges();
            return base.Create(input);
        }

        public override Task<EventTaskDto> Update(EventTaskUpdateDto input)
        {
            eventTaskMemberRepository.Delete(p => p.EventTaskId == input.Id);
            todoRepository.Delete(p => p.EventTaskId == input.Id);

            var project = GetProject(input.TaskGroupId);
            var userName = userAppService.GetCurrentUserName();
            var description = userName + " updated task " + input.TaskName + " in project " + project.ProjectName;
            var projectActivity = new ProjectActivity
            {
                ProjectId = project.Id,
                Description = description
            };
            projectActivityRepository.InsertAsync(projectActivity);
            //CurrentUnitOfWork.SaveChanges();

            return base.Update(input);
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
        private Project GetProject(int taskGroupId)
        {
            var taskGroup = taskGroupRepository.FirstOrDefault(taskGroupId);
            var project = projectRepository.FirstOrDefault(taskGroup.ProjectId);
            return project;
        }

        

        //public List<EventTaskSimpleDto> GetEventTaskCalendar()
        //{
        //    return new List<EventTaskSimpleDto>();
        //}
    }
}