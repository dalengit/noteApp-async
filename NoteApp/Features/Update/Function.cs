using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using NoteApp.Features.Create;

namespace NoteApp.Features.Update
{
    public class Function
    {
        private readonly INoteService _noteService;

        public Function(INoteService noteService)
        {
            _noteService = noteService;
        }

        [FunctionName("UpdateNote")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/note/{id:guid}")] UpdateNoteRequest request,
            Guid id,
            ILogger log)
        {
            var note = _noteService.UpdateNote(request, id);
            return new OkObjectResult(note);
        }
    }
}