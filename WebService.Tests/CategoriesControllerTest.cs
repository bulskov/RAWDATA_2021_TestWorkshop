using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using WebService.Controllers;
using WebService.ViewModels;
using Xunit;

namespace WebService.Tests
{
    public class CategoriesControllerTest
    {
        private readonly Mock<IDataService> _dataServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<LinkGenerator> _linkGeneratorMock;

        public CategoriesControllerTest()
        {
            _dataServiceMock = new Mock<IDataService>();
            _mapperMock = new Mock<IMapper>();
            _linkGeneratorMock = new Mock<LinkGenerator>();
        }

        [Fact]
        public void CreateCategory_ValidNewCategory_DataServiceCreateCategoryMustBeCalledOnce()
        {

            _mapperMock.Setup(x => x.Map<Category>(It.IsAny<CreateCategoryViewModel>())).Returns(new Category());
            _mapperMock.Setup(x => x.Map<CategoryViewModel>(It.IsAny<Category>())).Returns(new CategoryViewModel());

            _linkGeneratorMock.Setup(x => x.GetUriByAddress(
                    It.IsAny<HttpContext>(),
                    It.IsAny<string>(),
                    It.IsAny<RouteValueDictionary>(),
                    default, default, default, default, default, default))
                .Returns("");

            var ctrl = new CategoriesController(_dataServiceMock.Object, _linkGeneratorMock.Object, _mapperMock.Object);
            ctrl.ControllerContext = new ControllerContext();
            ctrl.ControllerContext.HttpContext = new DefaultHttpContext();


            ctrl.CreateCategory(new CreateCategoryViewModel());

            _dataServiceMock.Verify(x => x.CreateCategory(It.IsAny<Category>()), Times.Once);
            
        }
    }
}
