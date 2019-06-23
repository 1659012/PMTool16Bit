using Abp.Application.Services;
using Abp.Domain.Repositories;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMTool16Bit.Services
{
    public class FileService : AsyncCrudAppService<FileEntity, FileEntityDto, int,FileFilter, FileEntityCreateDto, FileEntityCreateDto>, IFileService
    {
        public FileService(
           IRepository<FileEntity> repository

           ) : base(repository)
        {
          
        }
        protected override IQueryable<FileEntity> CreateFilteredQuery(FileFilter input)
        {
            return base.CreateFilteredQuery(input);
        }
    }
}
