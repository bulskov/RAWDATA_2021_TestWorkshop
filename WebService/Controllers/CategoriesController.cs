using System.Linq;
using System.Reflection.Metadata.Ecma335;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebService.ViewModels;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = _dataService.GetCategories();
            var model = categories.Select(CreateCategoryViewModel);
            return new JsonResult(model);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
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
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id}),
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
