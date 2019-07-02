using Abp.AutoMapper;
using PMTool16Bit.Authorization.Users;
using System;

namespace PMTool16Bit.Users
{
    [AutoMapFrom(typeof(User))]
    public class UserSimpleDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}