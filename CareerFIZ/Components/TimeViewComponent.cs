using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerFIZ.Models;


namespace CareerFIZ.Components
{
    public class TimeViewComponent : ViewComponent
    {
        private readonly jobportaldbContext _context;

        public TimeViewComponent(jobportaldbContext context)
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
