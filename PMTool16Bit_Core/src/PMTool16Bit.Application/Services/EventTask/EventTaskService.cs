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
    public class EventTaskService : AsyncCrudAppService<EventTask, EventTaskDto, int, EventTaskFilter, EventTaskCreateDto, EventTaskUpdateDto>, IEventTaskService
    {
        //private readonly IUserAppService userAppService;
        //private readonly IRepository<EventTable> eventTableRepository;

        public EventTaskService(
             IRepository<EventTask> repository
            //IUserAppService userAppService,
            //IRepository<EventTable> eventTableRepository

            ) : base(repository)
        {           
            //this.eventTableRepository = eventTableRepository;
        }

        protected override IQueryable<EventTask> CreateFilteredQuery(EventTaskFilter input)
        {
            return base.CreateFilteredQuery(input);
              
        }  
    
    }
}
