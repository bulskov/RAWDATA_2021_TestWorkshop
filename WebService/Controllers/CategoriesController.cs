﻿using System.Reflection.Metadata.Ecma335;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;

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
            return new JsonResult(category);
        }
    }
}