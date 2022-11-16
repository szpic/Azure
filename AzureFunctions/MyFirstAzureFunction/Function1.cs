using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using MyFirstAzureFunction.ErrorDto;

namespace MyFirstAzureFunction
{
    public static class Function1
    {
        [FunctionName("HttpExample1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)][FromBody] User user,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            (bool isUserValid, Errors errors) = IsUserValid(user);
            if (!isUserValid)
            {
                string message = JsonConvert.SerializeObject(errors);
                return new BadRequestObjectResult(message);
            }
            string name = user?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name as a body json"
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        public static (bool isValid, Errors errors) IsUserValid(User user)
        {
            if (user is null)
            {
                return (false, new Errors() { error = new System.Collections.Generic.List<Error> { new Error { message = "User is empty" } } });
            }
            Errors _errors = new();
            List<Error> _error = new();
            _errors.error = _error;
            if (string.IsNullOrWhiteSpace(user.name))
            {
                _errors.error.Add(new Error { message = "User name is empty" });
            }
            if (string.IsNullOrEmpty(user.surname))
            {
                _errors.error.Add(new Error { message = "User surname is empty" });
            }
            if (_errors.error.Count > 0)
            {
                return (false, _errors);
            }
            return (true, null);
        }
    }
}
