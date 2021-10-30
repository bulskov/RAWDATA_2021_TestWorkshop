using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoriesController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();
            var model = categories.Select(CreateCategoryViewModel);
            return Ok(model);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = CreateCategoryViewModel(category);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryViewModel model)
        {
            var category = _mapper.Map<Category>(model);
            
            _dataService.CreateCategory(category);

            return Created(GetUrl(category), CreateCategoryViewModel(category));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (!_dataService.DeleteCategory(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CreateCategoryViewModel model)
        {
            var category = _mapper.Map<Category>(model);
            category.Id = id;

            if (!_dataService.UpdateCategory(category))
            {
                return NotFound();
            }

            return NoContent();
        }


        private CategoryViewModel CreateCategoryViewModel(Category category)
        {
            var model = _mapper.Map<CategoryViewModel>(category);
            model.Url = GetUrl(category);
            return model;
        }

        private string GetUrl(Category category)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
        }
    }
}
