using AuthApp.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PTLab2.Controllers;
using PTLab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PTLab2.Views.ViewModels;
using Castle.Components.DictionaryAdapter.Xml;
using System.Security.Principal;
using System.Collections;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ShopTest
{
    public  class ShopControllerTests
    {
        private readonly Mock<ILogger<ShopController>> _mock = new();
        private readonly IServiceProvider _serviceProvider;

        public ShopControllerTests()
        {
            _serviceProvider = DependencyInjection.InitilizeServices().BuildServiceProvider();
        }

        ShopController SetupController(ShopContext db)
        {
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock
                .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IAuthenticationService)))
                .Returns(authenticationServiceMock.Object);

            var viewmock = new Mock<ITempDataDictionaryFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(viewmock.Object);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);

            var identity = new GenericIdentity("mail@mail.ru", "test");
            var contextUser = new ClaimsPrincipal(identity);

            var controller = new ShopController(_mock.Object, db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        RequestServices = serviceProviderMock.Object,
                        User = contextUser
                    }
                }
            };
            return controller;
        }

        [Fact]
        public async Task CreateShopTest()
        {
            var db = _serviceProvider.GetRequiredService<ShopContext>();

            var controller = SetupController(db);

            var result = await controller.CreateShop();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task BuyTest()
        {
            var db = _serviceProvider.GetRequiredService<ShopContext>();

            var controller = SetupController(db);

            var Entry = new Product()
            {
                Id = db.Products.Last().Id + 1,
                Price = 500
            };
            db.Products.Add(Entry);
            await db.SaveChangesAsync();

            var result = await controller.Buy(Entry.Id);

            Assert.IsNotType<NotFoundResult>(result);
        }

        [Fact]
        public async Task RemoveTest()
        {
            var db = _serviceProvider.GetRequiredService<ShopContext>();

            var controller = SetupController(db);

            var Entry = new Product()
            {
                Id = db.Products.Last().Id + 1
            };
            db.Products.Add(Entry);
            await db.SaveChangesAsync();

            var result = await controller.Remove(Entry.Id);

            var viewResult = Assert.IsType<RedirectResult>(result);

            Assert.Null(db.Products.FirstOrDefault(p => p.Id == Entry.Id));
        }

        [Fact]
        public async Task EditTest()
        {
            var db = _serviceProvider.GetRequiredService<ShopContext>();

            var controller = SetupController(db);

            var Entry = new Product()
            {
                Id = db.Products.Last().Id + 1,
                Price = 500
            };
            var EditedEntry = new Product()
            {
                Id = Entry.Id,
                Price = 1500
            };

            db.Products.Add(Entry);
            await db.SaveChangesAsync();
            db.ChangeTracker.Clear();

            var result = await controller.Edit(EditedEntry);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            var updatedDbEntry = db.Products.FirstOrDefault(p => p.Id == Entry.Id);
            Assert.Equal(EditedEntry.Id, updatedDbEntry.Id);

        }
    }
}
