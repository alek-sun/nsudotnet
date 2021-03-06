﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Task4Web.Models;

namespace Task4Web.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly Task4Web.Models.EffectiveWorkerContext _context;

        public DetailsModel(Task4Web.Models.EffectiveWorkerContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                        .Include(s => s.Enrollments)
                            .ThenInclude(e => e.Worker)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Id == id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
