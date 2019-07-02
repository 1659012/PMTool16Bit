using Abp.Domain.Repositories;
using Abp.UI;
using PMTool16Bit.Models;

namespace PMTool16Bit.Services
{
    public class ProjectMemberService : IProjectService
    {
        private readonly IRepository<ProjectMember> repository;

        public ProjectMemberService(
            IRepository<ProjectMember> repository
            )
        {
            this.repository = repository;
        }

        public IRepository<ProjectMember> Repository => repository;

        public ProjectMember Update(ProjectMemberDto input)
        {
            var projectMember = repository
                                .FirstOrDefault(
                                    p => p.MemberId == input.MemberId
                                    && p.ProjectId == input.ProjectId);
            if (projectMember == null)
                return null;
            projectMember.ProjectRole = input.ProjectRole;
            return Repository.Update(projectMember);
        }

        public void Delete(int projectId, int memberId)
        {
            var projectMember = repository.FirstOrDefault(p => p.ProjectId == projectId
                                                           && p.MemberId == memberId);
            if (projectMember == null)
                throw new UserFriendlyException("Can not delete this item");

            Repository.Delete(projectMember);
        }
    }
}