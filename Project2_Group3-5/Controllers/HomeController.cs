using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project2_Group3_5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project2_Group3_5.Controllers
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

        public IActionResult TodoList()
        {
            var todos = ApplicationDbContext.Todos
                .Include(x => x.Category)
                .Where(x => x.Completed == false)
                .ToList();
            return View(todos);
        }

        [HttpGet]
        public IActionResult AddTodo()
        {
            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            ViewData["Title"] = "Add Todo";
            return View("AddEdit", new Todo());
        }

        [HttpPost]
        public IActionResult AddTodo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext.Add(todo);
                ApplicationDbContext.SaveChanges();
                // Have some form of alert, either a success screen or some other way
                return RedirectToAction("TodoList");
            }

            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            return View(new Todo());
        }

        [HttpGet]
        public IActionResult EditTodo(int id)
        {
            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            ViewData["Title"] = "Edit Todo";
            var todo = ApplicationDbContext.Todos.Single(x => x.Id == id);
            return View("AddEdit", todo);
        }

        [HttpPost]
        public IActionResult EditTodo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext.Update(todo);
                ApplicationDbContext.SaveChanges();
                // Again, some form of alert
                return RedirectToAction("TodoList");
            }

            ViewBag.Categories = ApplicationDbContext.Categories.ToList();
            return View("AddEdit", todo);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var todo = ApplicationDbContext.Todos.Single(x => x.Id == id);
            return View(todo);
        }

        [HttpPost]
        public IActionResult Delete(Todo todo)
        {
            todo = ApplicationDbContext.Todos.Single(x => x.Id == todo.Id);
            // Alert - depending on how we do this, needs to be done before the todo is deleted
            ApplicationDbContext.Todos.Remove(todo);
            ApplicationDbContext.SaveChanges();
            return RedirectToAction("TodoList");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
