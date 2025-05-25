using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/books/{id}")]
        public IActionResult Books()
        {
            int id = Convert.ToInt32(Request.RouteValues["id"]);
            return Content($"<h1>Welcome to the new book store</h1> <p>You are viewing the details of book with id:{id}</p>", "text/html");
        }
    }
}
