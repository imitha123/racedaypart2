using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using testingPOE.Controllers;
using poepart2.Model;
using poepart2.Services;

namespace Testing_Poe
{
    public class testing_organizer_controller
    {
        private OrganizerController CreateController(
        ClaimsPrincipal? user = null)
        {
            var passwordService = new PasswordService();


        var settings = new Dictionary<string, string?>
        {
            ["Jwt:Key"] = Convert.ToBase64String(
                System.Security.Cryptography.RandomNumberGenerator.GetBytes(32)),
            ["Jwt:Issuer"] = "Testing_Poe",
            ["Jwt:Audience"] = "Testing_Poe"
        };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            var controller = new OrganizerController(
                passwordService,
                configuration);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };

            if (user != null)
            {
                controller.ControllerContext.HttpContext.User = user;
            }

            return controller;
        }

        // Test 1: Get all organizers.
        // This endpoint requires an authenticated Organizer.
        [Fact]
        public void test_OrganizerGetAll_WithoutAuthentication()
        {
            OrganizerController controller = CreateController();

            var result = controller.GetOrganizers();

            // Direct controller calls do not run the ASP.NET authorization
            // middleware. The action itself does not check the user.
            // This test therefore checks the action response, not JWT security.
            Assert.IsType<OkObjectResult>(result);
        }

        // Test 2: Reject an invalid email during registration.
        [Fact]
        public void test_OrganizerRegister_InvalidEmail()
        {
            OrganizerController controller = CreateController();

            var model = new OrganizerRegisterModel
            {
                Organizername = "David",
                OrganizerSurname = "Brown",
                OrganizerEmail = "david@yahoo.com",
                Password = "Password123!"
            };

            IActionResult result = controller.Register(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 3: Reject an invalid password during registration.
        [Fact]
        public void test_OrganizerRegister_InvalidPassword()
        {
            OrganizerController controller = CreateController();

            var model = new OrganizerRegisterModel
            {
                Organizername = "David",
                OrganizerSurname = "Brown",
                OrganizerEmail = "david@gmail.com",
                Password = "password"
            };

            IActionResult result = controller.Register(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 4: Reject an invalid email during login.
        [Fact]
        public void test_OrganizerLogin_InvalidEmail()
        {
            OrganizerController controller = CreateController();

            var model = new OrganizerLoginModel
            {
                Email = "david@yahoo.com",
                Password = "Password123!"
            };

            IActionResult result = controller.Login(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 5: Reject login with an email that does not exist.
        [Fact]
        public void test_OrganizerLogin_InvalidCredentials()
        {
            OrganizerController controller = CreateController();

            var model = new OrganizerLoginModel
            {
                Email = "nonexistent_test_organizer_2026@gmail.com",
                Password = "WrongPassword123!"
            };

            IActionResult result = controller.Login(model);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        // Test 6: Reject an invalid organizer ID during update.
        [Fact]
        public void test_OrganizerUpdate_InvalidId()
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "2003"),
                new Claim(ClaimTypes.Role, "Organizer")
                },
                "TestAuthentication");

            var user = new ClaimsPrincipal(identity);
            OrganizerController controller = CreateController(user);

            var model = new OrganizerRegisterModel
            {
                Organizername = "Updated Organizer",
                OrganizerSurname = "Test",
                OrganizerEmail = "updated_test_2026@gmail.com",
                Password = "UpdatedPass123!"
            };

            // The action checks that the route ID matches the authenticated
            // organizer ID before performing the database operation.
            IActionResult result = controller.UpdateOrganizer(-1, model);

            Assert.IsType<ForbidResult>(result);
        }

        // Test 7: Reject an invalid organizer ID during deletion.
        [Fact]
        public void test_OrganizerDelete_InvalidId()
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "2003"),
                new Claim(ClaimTypes.Role, "Organizer")
                },
                "TestAuthentication");

            var user = new ClaimsPrincipal(identity);
            OrganizerController controller = CreateController(user);

            IActionResult result = controller.DeleteOrganizer(-1);

            Assert.IsType<ForbidResult>(result);
        }
    }


}
