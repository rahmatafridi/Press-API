using NJsonSchema;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressDot.Framework.Attribute
{
    public class AuthorizationHeaderParameterOperationProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            if (context.MethodInfo.CustomAttributes.Any(x => x.AttributeType.Name.Equals("AuthorizeAttribute"))
                || (context.ControllerType).BaseType.CustomAttributes.Any(x => x.AttributeType.Name.Equals("AuthorizeAttribute")))
            {

                context.OperationDescription.Operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = "Authorization",
                    Kind = OpenApiParameterKind.Header,
                    Schema = new JsonSchema { Type = JsonObjectType.String },
                    IsRequired = true,
                    Description = "Authorization Token",
                    Default = ""
                });
            }
            return true;

        }
    }
}
