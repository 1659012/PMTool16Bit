using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Authorization;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using PMTool16Bit.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class ProjectService : AsyncCrudAppService<Project, ProjectDto, int, ProjectFilter, ProjectCreateDto, ProjectUpdateDto>, IProjectService
    {
        private readonly IRepository<ProjectMember> projectMemberRepository;
        private readonly IUserAppService userAppService;
        private readonly IObjectMapper objectMapper;


        public ProjectService(
            IRepository<Project> repository,
            IRepository<ProjectMember> projectMemberRepository,
            IUserAppService userAppService,
            IObjectMapper objectMapper

            ) : base(repository)
        {
            DeletePermissionName = PermissionNames.ProjectDeletePermission;
            this.projectMemberRepository = projectMemberRepository;
            this.userAppService = userAppService;
            this.objectMapper = objectMapper;
        }

        protected override IQueryable<Project> CreateFilteredQuery(ProjectFilter input)
        {
            if (input.ProjectName.IsNotNullOrEmpty())
                input.ProjectName = input.ProjectName.Trim();

            return base.CreateFilteredQuery(input)
                .Include(p => p.ProjectOwner)
                //.Include(p => p.ProjectMembers)
                //.ThenInclude(p => p.Member)
                //.Include(p => p.GroupTasks)
                .AsNoTracking()
                .WhereIf(input.Id != null, p => p.Id == input.Id)
                .WhereIf(input.MemberId != null, p => p.Id == input.MemberId
                                                     || p.ProjectMembers.Any(q => q.MemberId == input.MemberId)
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

        public override Task<ProjectDto> Get(EntityDto<int> input)
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

                .Include(p => p.TaskGroups)
                .ThenInclude(p => p.EventTasks)
                .ThenInclude(m => m.Todos)
                .Where(p => p.Id == input.Id);

            var project = query.FirstOrDefaultAsync();
            var result = objectMapper.Map<Task<ProjectDto>>(project);
            return result;
            //return base.Get(input);
        }

        public List<UserDropdownDto> GetProjectMembers(int projectId)
        {
            return Repository
                    .GetAll()
                    .AsNoTracking()
                    .Include(p => p.ProjectMembers)
                    .ThenInclude(q => q.Member)
                    .FirstOrDefault(p => p.Id == projectId)
                    .ProjectMembers
                    .Select(p => new UserDropdownDto
                    {
                        Id = p.Member.Id,
                        FullName = p.Member.FullName,
                        EmailAddress = p.Member.EmailAddress
                    })
                    .ToList();
        }

        public List<EventTaskExcelDto> GetTaskListInProject(int projectId)
        {
            var project = Repository
                    .GetAll()
                    .Include(p => p.TaskGroups)
                    .ThenInclude(p => p.EventTasks)
                    .ThenInclude(q => q.EventTaskMembers)
                    .ThenInclude(m => m.Member)
                    .FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return new List<EventTaskExcelDto>();
            }

            var taskList = new List<EventTaskExcelDto>();

            foreach (var taskGroup in project.TaskGroups)
            {
                foreach (var eventTask in taskGroup.EventTasks)
                {
                    var eventTaskDto = new EventTaskExcelDto
                    {
                        ProjectName = project.ProjectName,
                        TaskGroupName = taskGroup.TaskGroupName,
                        TaskName = eventTask.TaskName,
                        Description = eventTask.Description,
                        StartDate = eventTask.StartDate,
                        DueDate = eventTask.DueDate,
                        IsCompleted = eventTask.IsCompleted,
                        PriorityLevel = PriorityLevels.EnumToString(eventTask.PriorityLevel),
                        AssignedMembers = GetEventTaskMemberNames(eventTask.EventTaskMembers.ToList())
                    };
                    taskList.Add(eventTaskDto);
                }
            }

            return taskList;
        }

        private string GetEventTaskMemberNames(List<EventTaskMember> eventTaskMembers)
        {
            var result = "";
            foreach (var eventTask in eventTaskMembers)
            {
                result += eventTask.Member.Name + ", ";
            }
            if (result.Length > 2)
                result = result.Remove(result.Length - 2);
            return result;
        }

        private double? ConvertDateTimeToUTC(DateTime? date)
        {
            if (date == null)
                return null;

            DateTime dt1970 = new DateTime(1970, 1, 1);
            TimeSpan span = DateTime.Parse(date.ToDateString()) - dt1970;
            return span.TotalMilliseconds;
        }

        public List<int> GetProjectIdListByCurrentUser()
        {
            var userId = userAppService.GetCurrentUserId() ?? default(long);
            if (userId == default(long))
                return new List<int>();
            return Repository.GetAll()
                    .Include(p => p.ProjectMembers)
                    .Where(p => p.ProjectMembers.Any(q => q.MemberId == userId))
                    .Select(p => p.Id)
                    .ToList();
        }

        public List<EventTaskGanttDto> GetGanttChartData(int projectId)
        {
            var project = Repository
                    .GetAll()
                    .Include(p => p.TaskGroups)
                    .ThenInclude(p => p.EventTasks)
                    .ThenInclude(q => q.EventTaskMembers)
                    .ThenInclude(m => m.Member)
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return new List<EventTaskGanttDto>();
            }

            var taskList = new List<EventTaskGanttDto>();
            int pointWidth = 3;//
            foreach (var taskGroup in project.TaskGroups)
            {
                var parentTask = new EventTaskGanttDto
                {
                    Id = "Parent" + taskGroup.Id.ToString(),
                    Name = taskGroup.TaskGroupName,
                    Start = null,
                    End = null,
                    Assignee = "",
                    Parent = "",
                    Dependency = "",
                    Completed = "",
                    PointWidth = pointWidth.ToString()
                };

                taskList.Add(parentTask);

                foreach (var eventTask in taskGroup.EventTasks)
                {
                    var eventTaskDto = new EventTaskGanttDto
                    {
                        Id = eventTask.Id.ToString(),
                        Name = eventTask.TaskName,
                        Start = ConvertDateTimeToUTC(eventTask.StartDate),
                        End = ConvertDateTimeToUTC(eventTask.DueDate),
                        Assignee = GetEventTaskMemberNames(eventTask.EventTaskMembers.ToList()),
                        Parent = parentTask.Id,
                        Dependency = eventTask.TaskDependencyId.ToString(),
                        Completed = "",
                        PointWidth = ""
                    };
                    taskList.Add(eventTaskDto);
                }
            }

            return taskList;
        }

        //[AbpAuthorize(PermissionNames.ProjectDeletePermission)]
        public override Task Delete(EntityDto<int> input)
        {
            //CheckPermission(PermissionNames.ProjectDeletePermission);
            //if (!PermissionChecker.IsGranted(PermissionNames.ProjectDeletePermission))
            //{
            //    throw new UserFriendlyException("You are not authorized to delete project!");
            //}
            PermissionChecker.Authorize(PermissionNames.ProjectDeletePermission);
            return base.Delete(input);
        }
       
    }
}