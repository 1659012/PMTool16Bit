using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Models;
using PMTool16Bit.Services;

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
        
        public string AvatarUrl { get; set; }

    }
}
