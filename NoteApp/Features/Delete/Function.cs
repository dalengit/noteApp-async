using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace NoteApp.Features.Delete
{
    // Azure function
    public class Function
    {
        private readonly INoteService _noteService;

        public Function(INoteService noteService)
        {
            _noteService = noteService;
        }

        [FunctionName("Delete")]
        public async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/note/{id:guid}")] HttpRequest req,
            Guid id,
            ILogger log)
        {
            _noteService.DeleteNote(id);
            return new OkResult();
        }
    }
}