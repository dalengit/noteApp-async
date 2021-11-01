using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace NoteApp.Features.Create
{
    public class Function
    {
        // Azure function to add note
        // Field
        private readonly INoteService _noteService;

        // Constructor
        public Function(INoteService noteService)
        {
            _noteService = noteService;
        }

        [FunctionName("AddNote")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/note")] NoteRequest request,
            ILogger log)
        {
            // Method
            var id = _noteService.AddNote(request);
            return new OkObjectResult(id);
        }
    }
}