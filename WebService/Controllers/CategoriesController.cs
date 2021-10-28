using System.Linq;
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

        IDataService _dataService;

        public CategoriesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = _dataService.GetCategories();
            var model = categories.Select(CreateCategoryViewModel);
            return new JsonResult(model);
        }

        [HttpGet("{id}")]
        public JsonResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            var model = CreateCategoryViewModel(category);

            return new JsonResult(model);
        }


        private CategoryViewModel CreateCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Url = "http://localhost:5001/api/categories/" + category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
