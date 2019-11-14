using Abp.Data;
using Abp.EntityFrameworkCore;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PMTool16Bit.EntityFrameworkCore.Repositories
{
    public class TestRepository: PMTool16BitRepositoryBase<EventTask>
    {
        private readonly IActiveTransactionProvider transactionProvider;
        private readonly IObjectMapper objectMapper;

        public TestRepository(IDbContextProvider<PMTool16BitDbContext> dbContextProvider,
            IActiveTransactionProvider transactionProvider,
            IObjectMapper objectMapper)
            : base(dbContextProvider)
        {
            this.transactionProvider = transactionProvider;
            this.objectMapper = objectMapper;
        }

        //public string GetExecuteStoredProc()
        //{
        //    var commandText = "Select * from EventTasks";           
        //    var events = Context.Database.ExecuteSqlCommand(commandText);
        //    return events;
        //}
    }
}
