using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MightyHomeAutomation.Pages
{
    public class AdminModel : PageModel
    {
        public string Action { get; set; }

        public void OnGet(string action)
        {
            Action = action;
        }
    }
}