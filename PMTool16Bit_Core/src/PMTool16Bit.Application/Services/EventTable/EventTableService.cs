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
    public class EventTableService : AsyncCrudAppService<EventTable, EventTableDto, int, EventTableFilter, EventTableCreateDto, EventTableUpdateDto>, IEventTableService
    {
        //private readonly IUserAppService userAppService;
        //private readonly IRepository<EventTable> eventTableRepository;

        public EventTableService(
             IRepository<EventTable> repository
            //IUserAppService userAppService,
            //IRepository<EventTable> eventTableRepository

            ) : base(repository)
        {           
            //this.eventTableRepository = eventTableRepository;
        }

        protected override IQueryable<EventTable> CreateFilteredQuery(EventTableFilter input)
        {
            return base.CreateFilteredQuery(input)
                //.Include(p => p.Project)
                .WhereIf (input.ProjectId!=null, p=> p.ProjectId==input.ProjectId)
                .WhereIf(!string.IsNullOrEmpty(input.TableName), t => t.TableName.Contains(input.TableName));
        }
    
        public async Task<object> GetProjectDropdown(EventTableFilter input)
        {
            return await Repository.GetAll()
                .WhereIf(input.ProjectId != null, p => p.ProjectId == input.ProjectId)                
                .Select(p => new
                {
                    p.Id,
                    p.ProjectId,
                    p.TableName
                })
                .ToListAsync();
        }

    
    }
}
