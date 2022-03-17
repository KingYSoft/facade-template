using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Swagger
{
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParameters = context.ApiDescription.ActionDescriptor.Parameters.Where(n => n.ParameterType == typeof(IFormFile)).ToList();
            if (fileParameters.Count <= 0)
            {
                return;
            }
            var requestBody = operation.RequestBody;
            foreach (var fileParameter in fileParameters)
            {
                if (requestBody.Content.Values.Any(x => x.Schema.Properties.ContainsKey(fileParameter.Name)))
                {
                    requestBody.Content.Values.FirstOrDefault(x => x.Schema.Properties.ContainsKey(fileParameter.Name)).Schema =
                    new OpenApiSchema
                    {
                        Type = "object",
                        Properties = new Dictionary<string, OpenApiSchema>(StringComparer.OrdinalIgnoreCase)
                        {
                            {
                                fileParameter.Name,
                                new OpenApiSchema
                                {
                                        Type = "file",
                                        Format = "binary"
                                }
                            }
                        }
                    };
                }
            }
        }
    }
}
