using Abp.Application.Services;
using Abp.Domain.Repositories;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
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
    }
}