using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Models;
using PMTool16Bit.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool16Bit.Services
{
    public class ProjectMemberService :  IProjectService
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