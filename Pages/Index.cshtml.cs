using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MightyHomeAutomation.Persistence;

namespace MightyHomeAutomation.Pages
{
    public class IndexModel : PageModel
    {
        public Configuration Configuration { get; }

        public IndexModel(Configuration configuration) => Configuration = configuration;

        public void OnGet()
        {

        }
    }
}
