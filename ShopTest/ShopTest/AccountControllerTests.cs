using AuthApp.Controllers;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PTLab2.Controllers;
using PTLab2.Models;
using PTLab2.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShopTest
{
    public class AccountControllerTests
    {
        private readonly Mock<ILogger<AccountController>> _mock = new();
        private readonly IServiceProvider _serviceProvider;

        public AccountControllerTests()
        {
            _serviceProvider = DependencyInjection.InitilizeServices().BuildServiceProvider();
        }

        AccountController SetupController(ShopContext db)
        {
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock
                .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IAuthenticationService)))
                .Returns(authenticationServiceMock.Object);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);

            var controller = new AccountController(_mock.Object, db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };
            return controller;
        }

        [Fact]
        public async Task LoginTest()
        {
            var db = _serviceProvider.GetRequiredService<ShopContext>();

            var controller = SetupController(db);

            var NewUser = new User()
            {
                Mail = "test@mail.ru",
                Password = "test"
            };
            var LoginUser = new LoginModel()
            {
                Email = "test@mail.ru",
                Password = "test"
            };

            db.Users.Add(NewUser);
            await db.SaveChangesAsync();
            db.ChangeTracker.Clear();

            var result = await controller.Login(LoginUser);

            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task RegisterTest()
        {
            var db = _serviceProvider.GetRequiredService<ShopContext>();

            var controller = SetupController(db);

            var Entry = new RegisterModel()
            {
                Email = "NewMail@mail.ru",
                Password = "mypass",
                ConfirmPassword = "mypass",
                FirstName = "Иван",
                LastName = "Иванов"
            };

            var result = await controller.Register(Entry);

            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task LogoutTest()
        {
            var db = _serviceProvider.GetRequiredService<ShopContext>();

            var controller = SetupController(db);

            var result = await controller.Logout();

            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);
            Assert.Equal("Login", redirectResult.ActionName);
        }
    }
}
