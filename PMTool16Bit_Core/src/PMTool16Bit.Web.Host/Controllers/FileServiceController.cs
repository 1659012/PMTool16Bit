using Abp.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMTool16Bit.Models.Enum;
using PMTool16Bit.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PMTool16Bit.Controllers
{
    [Route("api/services/app/[controller]/[action]")]
    public class FileServiceController : PMTool16BitControllerBase

    {
        private readonly FileService fileService;
        private IHostingEnvironment hostingEnvironment;
        private ProjectService projectService;

        public FileServiceController(
            FileService fileService,
            IHostingEnvironment hostingEnvironment,
            ProjectService projectService
            )
        {
            this.fileService = fileService;
            this.hostingEnvironment = hostingEnvironment;
            this.projectService = projectService;
        }

        #region Upload/download file

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<object> UploadDocument(IFormFile file, string name)
        {
            if (file.Length > 0)
            {
                string webRootPath = hostingEnvironment.WebRootPath;
                var fileName = $"{DateTime.Now.Ticks}_{file.FileName}";
                var folderPath = $"Uploads\\{DateTime.Today:dd-MM-yyyy}";
                var path = $"{folderPath}\\{fileName}".Replace("\\", "/");
                var filePath = Path.Combine(webRootPath, $"{folderPath}\\{ fileName}");
                Directory.CreateDirectory(Path.Combine(webRootPath, folderPath));

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var fileDto = new FileEntityCreateDto
                {
                    Name = fileName,
                    Path = path,
                    Type = file.ContentType,
                };

                var fileEntity = await fileService.Create(fileDto);
                var baseUrl = await SettingManager.GetSettingValueAsync(SettingKey.App_BaseUrl);
                var entityPath = baseUrl + fileEntity.Path;
                return new { Path = entityPath, fileEntity.Id, name = fileName, fileEntity.Type };
            }
            return null;
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<object> UploadImage(IFormFile file, string name)
        {
            if (file.Length > 0)
            {
                string webRootPath = hostingEnvironment.WebRootPath;
                var fileName = $"{DateTime.Now.Ticks}_{file.FileName}";
                var folderPath = $"images";
                var path = $"{folderPath}/{fileName}";
                var filePath = Path.Combine(webRootPath, $"{folderPath}\\{ fileName}");
                Directory.CreateDirectory(Path.Combine(webRootPath, folderPath));

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                var fileDto = new FileEntityCreateDto
                {
                    Name = fileName,
                    Path = path,
                    Type = file.ContentType
                };

                var fileEntity = await fileService.Create(fileDto);
                var fileTail = fileEntity.Type.Split('/');
                var fileType = fileTail[0];
                if (fileType != "image")
                {
                    throw new EntityNotFoundException("This file is not an image file!");
                }

                var baseUrl = await SettingManager.GetSettingValueAsync(SettingKey.App_BaseUrl);
                var entityPath = Path.Combine(baseUrl, fileEntity.Path);
                return new { Path = entityPath, fileEntity.Id, name = fileName, fileEntity.Type };
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var fileEntity = await fileService.GetFileById(id);
            string webRootPath = hostingEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, fileEntity.Path);
            var net = new System.Net.WebClient();
            var data = net.DownloadData(filePath);
            var content = new System.IO.MemoryStream(data);
            var contentType = fileEntity.Type;
            var fileName = fileEntity.Name;
            return File(content, contentType, fileName);
        }

        #endregion Upload/download file

        #region Export excel

        [HttpGet]
        public async Task<IActionResult> Export<T>(string fileName, List<T> list)
        {
            var abstractDataExport = new AbstractDataExportBridge();
            var workbook = abstractDataExport.Export(list, "sheet1");
            string webRootPath = hostingEnvironment.WebRootPath;
            const string downloadFolder = ".\\Download\\";
            string serverFolder = Path.Combine(webRootPath, downloadFolder);
            Directory.CreateDirectory(serverFolder);
            var oldFiles = Directory.GetFiles(serverFolder);
            if (oldFiles.HasData() && oldFiles.Length > 0)
            {
                foreach (var file in oldFiles)
                    System.IO.File.Delete(file);
            }
            var xfile = new FileStream(Path.Combine(serverFolder, fileName), FileMode.Create, System.IO.FileAccess.Write);
            workbook.Write(xfile);
            xfile.Close();
            var net = new System.Net.WebClient();
            var data = net.DownloadData(Path.Combine(serverFolder, fileName));
            var content = new System.IO.MemoryStream(data);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content, contentType, fileName);
        }

        [HttpGet]
        public async Task<IActionResult> ExportTasksInProject(int projectId)
        {
            var fileName = $"ProjectTasks.xlsx";
            var listExportExcel = projectService.GetTaskListInProject(projectId);
            return await Export(fileName, listExportExcel);
        }

        #endregion Export excel
    }
}