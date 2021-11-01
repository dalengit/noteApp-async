using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace NoteApp.Features.GetById
{
    // Azure function
    public class Function
    {
        private readonly INoteService _noteService;

        public Function(INoteService noteService)
        {
            _noteService = noteService;
        }

        [FunctionName("GetById")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/note/{id:guid}")] HttpRequest req,
            Guid id,
            ILogger log)
        {
            var note = _noteService.GetById(id);
            return new OkObjectResult(note);
        }
    }
}
