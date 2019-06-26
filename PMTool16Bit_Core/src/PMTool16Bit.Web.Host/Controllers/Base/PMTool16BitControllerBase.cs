﻿using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool16Bit.Web.Host.Controllers
{
    public abstract class PMTool16BitControllerBase : AbpController
    {
        protected PMTool16BitControllerBase()
        {
            LocalizationSourceName = PMTool16BitConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}