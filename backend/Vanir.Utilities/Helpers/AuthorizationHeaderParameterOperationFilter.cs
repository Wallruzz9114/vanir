using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Vanir.Utilities.Helpers
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasDeclaringTypes = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
            var isAuthorized = hasDeclaringTypes || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (isAuthorized)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                var oauthParameter = new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Access Token",
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" }
                };

                operation.Parameters.Add(oauthParameter);
            }
        }
    }
}