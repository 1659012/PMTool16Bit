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
    public class GroupTaskService : AsyncCrudAppService<GroupTask, GroupTaskDto, int, GroupTaskFilter, GroupTaskCreateDto, GroupTaskUpdateDto>, IGroupTaskService
    {
        //private readonly IUserAppService userAppService;
        //private readonly IRepository<EventTable> eventTableRepository;

        public GroupTaskService(
             IRepository<GroupTask> repository
            //IUserAppService userAppService,
            //IRepository<EventTable> eventTableRepository

            ) : base(repository)
        {           
            //this.eventTableRepository = eventTableRepository;
        }

        protected override IQueryable<GroupTask> CreateFilteredQuery(GroupTaskFilter input)
        {
            return base.CreateFilteredQuery(input)
                //.Include(p => p.Project)
                .WhereIf (input.ProjectId!=null, p=> p.ProjectId==input.ProjectId)
                ;
        }  
    
    }
}
