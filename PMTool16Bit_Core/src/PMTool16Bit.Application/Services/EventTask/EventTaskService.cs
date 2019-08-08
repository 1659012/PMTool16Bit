using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Models;
using PMTool16Bit.Users;
using System;
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
            this.projectService = projectService;
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

        public List<EventTaskSimpleDto> GetEventTaskCalendar()
        {
            var projectIdList = projectService.GetProjectIdListByCurrentUser();
            var taskList = new List<EventTaskSimpleDto>();
            if (projectIdList.Count == 0)
            {
                return taskList;
            }
            var result = Repository
                        .GetAll()
                        .Include(p => p.EventTaskMembers)
                        .ThenInclude(p => p.Member)
                        .Include(p => p.TaskGroup)
                        .ThenInclude(p => p.Project)
                        .Where(p => p.IsCompleted == false)
                        .Where(p=> p.DueDate != null)
                        .Where(p => projectIdList.Any(q => q == p.TaskGroup.Project.Id))
                        .Select(p=> new EventTaskSimpleDto
                        {
                            Id = p.Id,
                            ProjectId = p.TaskGroup.ProjectId,
                            ProjectName = p.TaskGroup.Project.ProjectName,
                            TaskGroupName = p.TaskGroup.TaskGroupName,
                            TaskName = p.TaskName,
                            StartDate = p.StartDate,
                            DueDate = p.DueDate                           
                        })
                        .ToList();
            return result;
        }

        public override Task<EventTaskDto> Get(EntityDto<int> input)
        {
            var result = Repository
                        .GetAll()
                        .Include(p => p.EventTaskMembers)
                        .ThenInclude(p => p.Member)
                        .Include(p => p.Todos)
                        .FirstOrDefaultAsync(p => p.Id == input.Id);

            return base.Get(input);
        }

        public async Task UpdateTaskOrder(List<EventTaskOrderDto> eventTaskOrders)
        {
            foreach (var item in eventTaskOrders)
            {
                var eventTask = Repository.FirstOrDefault(item.Id);
                eventTask.TaskOrder = item.TaskOrder;
                var result = await Repository.UpdateAsync(eventTask);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}