using System;
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
    public class ProductsControllerTests
    {
        private readonly Mock<IDataService> _dataServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<LinkGenerator> _linkGeneratorMock;

        public ProductsControllerTests()
        {
            _dataServiceMock = new Mock<IDataService>();
            _mapperMock = new Mock<IMapper>();
            _linkGeneratorMock = new Mock<LinkGenerator>();
        }

        [Fact]
        public void GetProduct_ValidProductId_ReturnsOkStatus()
        {
            _dataServiceMock.Setup(x => x.GetProduct(It.IsAny<int>())).Returns(new Product());
            _mapperMock.Setup(x => x.Map<ProductViewModel>(It.IsAny<Product>())).Returns(new ProductViewModel());
            
            var ctrl = CreateProductsController();

            var result = ctrl.GetProduct(1);

            Assert.IsType<OkObjectResult>(result);
        }

        
        [Fact]
        public void GetProduct_InvalidProductId_ReturnsNotFoundStatus()
        {
            var ctrl = new ProductsController(_dataServiceMock.Object, null, null);

            var result = ctrl.GetProduct(-1);

            Assert.IsType<NotFoundResult>(result);
        }

       
        /*
         *
         * Helper methods
         *
         */

        private ProductsController CreateProductsController()
        {
            var ctrl = new ProductsController(_dataServiceMock.Object, _linkGeneratorMock.Object, _mapperMock.Object);
            ctrl.ControllerContext = new ControllerContext();
            ctrl.ControllerContext.HttpContext = new DefaultHttpContext();
            return ctrl;
        }


    }
}
