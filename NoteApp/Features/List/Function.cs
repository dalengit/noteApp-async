using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NoteApp.Features.List
{
    // Azure Function
    public class Function
    {
        private readonly INoteService _noteService;

        public Function(INoteService noteService)
        {
            _noteService = noteService;
        }

        [FunctionName("List")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/note")] HttpRequest req,
            ILogger log)
        {
            var note = _noteService.List();
            return new OkObjectResult(note);
        }
    }
}
