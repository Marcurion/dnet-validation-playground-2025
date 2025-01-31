using Application;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class MyController : Controller
    {

        MyManager _manager;

        public MyController(MyManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            _manager.ChangeSomeData();

            return Ok();
        }
    }
}
