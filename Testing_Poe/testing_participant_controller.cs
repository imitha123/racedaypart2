using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using poepart2.Controllers;
using poepart2.Model;
using poepart2.Services;

namespace Testing_Poe
{
    public class testing_participant_controller
    {
        private ParticipantController CreateController(
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

            var controller = new ParticipantController(
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

        // Test 1: Reject registration with an invalid email
        [Fact]
        public void test_ParticipantAdd_InvalidEmail()
        {
            ParticipantController controller = CreateController();

            IActionResult result = controller.AddParticipant(
                "David",
                "Brown",
                "david@yahoo.com",
                "Password123!");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 2: Reject registration with an invalid password
        [Fact]
        public void test_ParticipantAdd_InvalidPassword()
        {
            ParticipantController controller = CreateController();

            IActionResult result = controller.AddParticipant(
                "David",
                "Brown",
                "david@gmail.com",
                "password");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 3: Reject registration when the participant name is missing
        [Fact]
        public void test_ParticipantAdd_MissingName()
        {
            ParticipantController controller = CreateController();

            IActionResult result = controller.AddParticipant(
                "",
                "Brown",
                "david@gmail.com",
                "Password123!");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 4: Reject login with an invalid email
        [Fact]
        public void test_ParticipantLogin_InvalidEmail()
        {
            ParticipantController controller = CreateController();

            var request = new ParticipantLoginRequest
            {
                Email = "david@yahoo.com",
                Password = "Password123!"
            };

            IActionResult result = controller.Login(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 5: Reject login when email or password is missing
        [Fact]
        public void test_ParticipantLogin_MissingCredentials()
        {
            ParticipantController controller = CreateController();

            var request = new ParticipantLoginRequest
            {
                Email = "",
                Password = ""
            };

            IActionResult result = controller.Login(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 6: Reject login when the participant does not exist
        [Fact]
        public void test_ParticipantLogin_InvalidCredentials()
        {
            ParticipantController controller = CreateController();

            var request = new ParticipantLoginRequest
            {
                Email = "nobody_test_987654@gmail.com",
                Password = "WrongPassword123!"
            };

            IActionResult result = controller.Login(request);

            // A missing account should produce 401 if the database
            // connection and query work successfully.
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        // Test 7: Reject updating another participant's account
        [Fact]
        public void test_ParticipantUpdate_WrongOwner()
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "3005"),
                new Claim(ClaimTypes.Role, "Participant")
                },
                "TestAuthentication");

            var user = new ClaimsPrincipal(identity);
            ParticipantController controller = CreateController(user);

            var updatedParticipant = new ParticipantModel
            {
                Participantname = "Updated",
                Participantsurname = "Participant",
                participantEmail = "updated_test_2026@gmail.com",
                participantPassword = "UpdatePass123!"
            };

            // The authenticated participant is 3005, but the requested
            // account is -1. The action should forbid the operation.
            IActionResult result =
                controller.UpdateParticipant(-1, updatedParticipant);

            Assert.IsType<ForbidResult>(result);
        }

        // Test 8: Reject deleting another participant's account
        [Fact]
        public void test_ParticipantDelete_WrongOwner()
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "3005"),
                new Claim(ClaimTypes.Role, "Participant")
                },
                "TestAuthentication");

            var user = new ClaimsPrincipal(identity);
            ParticipantController controller = CreateController(user);

            // The authenticated participant is 3005, but the requested
            // account is -1. The action should forbid the operation.
            IActionResult result = controller.DeleteParticipant(-1);

            Assert.IsType<ForbidResult>(result);
        }
    }


}
