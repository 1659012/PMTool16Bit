using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Y.Web.Host.Swagger
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (
                operation.OperationId.ToLower() == "apiservicesappfileserviceuploaddocumentpost"
                || operation.OperationId.ToLower() == "apiservicesappfileserviceuploadimagepost"
               )
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "file",
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}