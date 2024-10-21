using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace BetCatalog.WebApi.OpenApi
{
    public class UsersOperationProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            if (context.OperationDescription.Path.StartsWith("/api/users/"))
            {
                FixUserOperationId(context.OperationDescription);
            }

            return true;
        }

        private static void FixUserOperationId(OpenApiOperationDescription operationDescription)
        {
            var operation = operationDescription.Operation;
            var index = operation.OperationId.IndexOf("ApiUsers") + "ApiUsers".Length;
            var operationId = operation.OperationId[index..];
            if (operationDescription.Method.Equals("get", StringComparison.OrdinalIgnoreCase))
            {
                operationId = "Get" + operationId;
            }
            if (operation.Tags.Count > 1)
            {
                operationId = operation.Tags[1];
            }

            operation.OperationId = "Users_" + operationId;
        }
    }
}
