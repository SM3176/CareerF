﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerFIZ.Models;
using CareerFIZ.DataContext;

namespace CareerFIZ.Components
{
    public class TimeViewComponent : ViewComponent
    {
        private readonly DataDbContext _context;

        public TimeViewComponent(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Time> timeList = await _context.Times.OrderBy(t => t.Id).ToListAsync();
            return View(timeList);
        }
    }
}
