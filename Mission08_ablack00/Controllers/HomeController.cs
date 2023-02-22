using Microsoft.AspNetCore.Mvc;

namespace Mission08_ablack00.Controllers
{
    public class HomeController : Controller
    {
        
        private ApplicationDbContext ApplicationDbContext { get; set; }
        
        public HomeController(ApplicationDbContext context)
        {
            ApplicationDbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TaskList()
        {
            var tasks = ApplicationDbContext.Tasks
                .Include(x => x.Category)
                .Where(x => x.Completed == false)
                .ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            ViewData["Title"] = "Add Task";
            return View("AddEdit", new Task());
        }

        [HttpPost]
        public IActionResult AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext.Add(task);
                ApplicationDbContext.SaveChanges();
                // Have some form of alert, either a success screen or some other way
                return RedirectToAction("TaskList");
            }

            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            return View(new Task());
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            ViewData["Title"] = "Edit Task";
            var task = ApplicationDbContext.Tasks.Single(x => x.Id == id);
            return View("AddEdit", task);
        }

        [HttpPost]
        public IActionResult EditTask(Task task)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext.Update(task);
                ApplicationDbContext.SaveChanges();
                // Again, some form of alert
                return RedirectToAction("TaskList");
            }

            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            return View("AddEdit", task);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = ApplicationDbContext.Tasks.Single(x => x.Id == id);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(Task task)
        {
            task = ApplicationDbContext.Tasks.Single(x => x.Id == task.Id);
            // Alert - depending on how we do this, needs to be done before the task is deleted
            ApplicationDbContext.Tasks.Remove(task);
            ApplicationDbContext.SaveChanges();
            return RedirectToAction("TaskList");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
