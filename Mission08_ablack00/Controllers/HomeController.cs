using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission08_ablack00.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.DateTime;
using Task = Mission08_ablack00.Models.Task;

namespace Mission08_ablack00.Controllers
{
    public class HomeController : Controller
    {
        
        private TaskContext TaskContext { get; set; }
        
        public HomeController(TaskContext context)
        {
            TaskContext = context;
        }

        public IActionResult TaskList()
        {
            var tasks = TaskContext.Tasks
                .Include(x => x.Category)
                .Where(x => x.Completed == false)
                .ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = TaskContext.Categories.ToList();
            ViewData["Title"] = "Add Task";
            Task task = new Task
            {
                DueDate = Now
            };
            return View("AddEdit", task);
        }

        [HttpPost]
        public IActionResult AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                TaskContext.Add(task);
                TaskContext.SaveChanges();
                // Have some form of alert, either a success screen or some other way
                return RedirectToAction("TaskList");
            }

            ViewBag.Categories = TaskContext.Categories.ToList();
            return View("AddEdit",new Task());
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            ViewBag.Categories = TaskContext.Categories.ToList();
            ViewData["Title"] = "Edit Task";
            var task = TaskContext.Tasks.Single(x => x.TaskId == id);
            return View("AddEdit", task);
        }

        [HttpPost]
        public IActionResult EditTask(Task task)
        {
            if (ModelState.IsValid)
            {
                TaskContext.Update(task);
                TaskContext.SaveChanges();
                // Again, some form of alert
                return RedirectToAction("TaskList");
            }

            ViewBag.Categories = TaskContext.Categories.ToList();
            return View("AddEdit", task);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = TaskContext.Tasks.Single(x => x.TaskId == id);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(Task task)
        {
            task = TaskContext.Tasks.Single(x => x.TaskId == task.TaskId);
            // Alert - depending on how we do this, needs to be done before the task is deleted
            TaskContext.Tasks.Remove(task);
            TaskContext.SaveChanges();
            return RedirectToAction("TaskList");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
