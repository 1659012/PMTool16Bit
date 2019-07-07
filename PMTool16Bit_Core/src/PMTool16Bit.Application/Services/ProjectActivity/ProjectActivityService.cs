using Abp.Domain.Repositories;
using Abp.UI;
using PMTool16Bit.Models;

namespace PMTool16Bit.Services
{
    public class ProjectActivityService : IProjectActivityService
    {
        private readonly IRepository<ProjectActivity> repository;

        public ProjectActivityService(
            IRepository<ProjectActivity> repository
            )
        {
            this.repository = repository;
        }

        public IRepository<ProjectActivity> Repository => repository;
       

    }
}