using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using PMTool16Bit.Authorization.Users;

namespace PMTool16Bit.Users
{  
    public class UserDropdownDto
    {
        public long Id { get; set; }

        //public string UserName { get; set; }
        
        //public string Name { get; set; }

        //public string Surname { get; set; }

        public string FullName { get; set; }


        public string EmailAddress { get; set; }
      
    }
}
