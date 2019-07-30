using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool16Bit.Services
{
    public class FileService : AsyncCrudAppService<FileEntity, FileEntityDto, int, FileFilter, FileEntityCreateDto, FileEntityCreateDto>, IFileService
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

        public Task<FileEntity> GetFileById(int id)
        {
            return base.GetEntityByIdAsync(id);
        }

        public async Task<string> GetFileUrl(int fileId)
        {
            var baseUrl = await SettingManager.GetSettingValueAsync(SettingKey.App_BaseUrl);
            var file = GetFileById(fileId).Result;
            var entityPath = Path.Combine(baseUrl, file.Path);
            return entityPath;
        }

        public async Task<List<FileEntityDto>> GetAllFileFromIds(string idString)
        {
            if (idString.IsNullOrEmpty())
            {
                return new List<FileEntityDto>();
            }

            var ids = idString.Split(',');

            var pictureList = await Repository.GetAll()
                .Where(p => ids.Contains(p.Id.ToString()))
                .Select(p => new FileEntityDto
                {
                    Name = p.Name,
                    Id = p.Id,
                    Path = p.Path
                })
                .ToListAsync();

            var baseUrl = await SettingManager.GetSettingValueAsync(SettingKey.App_BaseUrl);

            pictureList.ForEach(p =>
            {
                p.Path = Path.Combine(baseUrl, p.Path);
            });

            return pictureList;
        }
    }
}