using Abp.Application.Services;
using Abp.Domain.Repositories;
using PMTool16Bit.Models;

namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class TodoService : AsyncCrudAppService<Todo, TodoDto, int, TodoDto, TodoDto, TodoDto>, ITodoService
    {
        //private readonly IUserAppService userAppService;
        //private readonly IRepository<EventTable> eventTableRepository;

        public TodoService(
             IRepository<Todo> repository
            //IUserAppService userAppService,
            //IRepository<EventTable> eventTableRepository

            ) : base(repository)
        {
            //this.eventTableRepository = eventTableRepository;
        }
    }
}