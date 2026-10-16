using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Security.Claims;
using poepart2.Controllers;
using poepart2.Model;

namespace Testing_Poe
{
    public class testing_results_controller
    {
        private readonly ResultController controller;


    public testing_results_controller()
        {
            controller = new ResultController();
        }

        // Test 1: Get all results
        [Fact]
        public void test_ResultGetAll()
        {
            // Arrange
            ResultController controller = new ResultController();


// Act
IActionResult result = controller.GetAllResults();

            // Assert
            ObjectResult objectResult = Assert.IsAssignableFrom<ObjectResult>(result);

            Assert.Equal(200, objectResult.StatusCode);
            Assert.NotNull(objectResult.Value);


}



        // Test 2: Get an existing result by ID
        [Fact]
        public void test_ResultGetById()
        {
            IActionResult result = controller.GetResult(2004);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("85", json);
            Assert.Contains("4th", json);
        }

        // Test 3: Get a result using an invalid ID
        [Fact]
        public void test_ResultGetByInvalidId()
        {
            IActionResult result = controller.GetResult(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 4: Reject a result with an invalid position
        [Fact]
        public void test_ResultAdd_InvalidPosition()
        {
            ResultController testController = CreateOrganizerController();

            ResultsModel newResult = new ResultsModel
            {
                Position = "Test Position",
                Score = 999,
                participantId = 3003
            };

            IActionResult result = testController.AddResult(newResult);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 5: Reject a result with a negative score
        [Fact]
        public void test_ResultAdd_NegativeScore()
        {
            ResultController testController = CreateOrganizerController();

            ResultsModel newResult = new ResultsModel
            {
                Position = "3",
                Score = -1,
                participantId = 3003
            };

            IActionResult result = testController.AddResult(newResult);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 6: Reject updating a result with an invalid ID
        [Fact]
        public void test_ResultUpdateInvalidId()
        {
            ResultController testController = CreateOrganizerController();

            ResultsModel updatedResult = new ResultsModel
            {
                Position = "3",
                Score = 95,
                participantId = 3003
            };

            IActionResult result =
                testController.UpdateResult(-1, updatedResult);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 7: Reject deleting a result with an invalid ID
        [Fact]
        public void test_ResultDeleteInvalidId()
        {
            ResultController testController = CreateOrganizerController();

            IActionResult result = testController.DeleteResult(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Helper: create a controller with an Organizer identity.
        // Direct controller calls do not execute the authorization middleware.
        private ResultController CreateOrganizerController()
        {
            var testController = new ResultController();

            var identity = new ClaimsIdentity(
                new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "2003"),
                new Claim(ClaimTypes.Role, "Organizer")
                },
                "TestAuthentication");

            testController.ControllerContext = new ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext
                {
                    User = new ClaimsPrincipal(identity)
                }
            };

            return testController;
        }
    }


}
