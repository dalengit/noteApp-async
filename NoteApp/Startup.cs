using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoteApp;
using NoteApp.Data;
using NoteApp.Features;

[assembly: WebJobsStartup(typeof(Startup))]

namespace NoteApp
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddDbContext<DataContext>(opt =>
                opt.UseSqlServer("Persist Security Info=False;Trusted_Connection=True;Data Source=BRI48665L\\SQLEXPRESS;Initial Catalog=SandboxNoteApp;"));

            //"Persist Security Info=False;Trusted_Connection=True;database=SandboxNoteApp;server=BRI48665L\\sqlexpress"
        }
    }
}
