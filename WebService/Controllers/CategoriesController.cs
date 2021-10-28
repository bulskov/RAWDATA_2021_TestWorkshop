using System.Reflection.Metadata.Ecma335;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;
using WebService.ViewModels;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {

        IDataService dataService = new DataService();

        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = dataService.GetCategories();
            return new JsonResult(categories);
        }

        [HttpGet("{id}")]
        public JsonResult GetCategory(int id)
        {
            var category = dataService.GetCategory(id);

            var model = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return new JsonResult(model);
        }
    }
}
