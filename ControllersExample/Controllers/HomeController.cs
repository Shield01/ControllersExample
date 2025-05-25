using Microsoft.AspNetCore.Mvc;
using ControllersExample.Models;

namespace ControllersExample.Controllers
{
    public class HomeController: Controller
    {
        //Demonstrating the use of ContentResult
        [Route("/")]
        public ContentResult Index()
        {
            return Content("<h1>Welcome home</h1>", "text/html");
        }

        //Demonstrating the use of JsonResult
        [Route("about")]
        public JsonResult About()
        {
            Person person = new Person() { 
                Id = Guid.NewGuid(),
                FirstName = "King", 
                LastName = "Hussein", 
                Age = 15
            };

            return Json(person);
        }


        [Route("contact-us/{mobile:regex(^\\d{{11}}$)}")]
        public string Contact()
        {
            return "How would you like to contact us?";
        }

        //Demonstrating the use of FileResults

        [Route("file-download")]
        public VirtualFileResult FileDownload() {
            return File("/sample.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            return PhysicalFile(@"c:\Users\User\Downloads\sample.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"c:\Users\User\Downloads\sample.pdf");
            return File(bytes, "application/pdf");
        }

        //Demonstrating the use of IActionResult, which is the recommended return types for controller methods in AspNetCore
        [Route("book")]
        public IActionResult GetBook()
        {
            //Book Id should be provided
            if (!Request.Query.ContainsKey("bookid"))
            {
                return BadRequest("Book id is not supplied");
            }

            //Book id can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookId"])))
            {
                return BadRequest("Book id can't be null or empty");
            }

            //Book id should be between 1 and 1000
            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookId"]);

            if (bookId <= 0)
            {
                return BadRequest("Book id cannot be less than or equal to zero");
            }

            if (bookId > 1000)
            {
                return NotFound("Book id cannot be greater than 1000");
            }

            // Is logged in should be true
            if (!Convert.ToBoolean(Request.Query["isloggedin"]))
            {
                return Unauthorized("User must be authenticated");
            }

            // All conditions are met, redirect to the new endpoint

            //return RedirectToAction("Books", "Store", new { id = 1 }); //Status code will be 302

            //return RedirectToActionPermanent("Books", "Store", new { id = 1 }); // Status code will be 301

            //Demonstrate the use of LocalRedirectResult
            //return new LocalRedirectResult($"store/books/{bookId}", true);
            //return LocalRedirect($"store/books/{bookId}"); // Status code will be 302
            //return LocalRedirectPermanent($"store/books/{bookId}"); // Status code will be 301

            return Redirect($"store/books/{bookId}");
        }
    }
}
