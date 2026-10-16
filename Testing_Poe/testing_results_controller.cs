using Xunit;
using Microsoft.AspNetCore.Mvc;
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

        // Test 1: Get all results without relying on fixed IDs.
        [Fact]
        public void test_ResultGetAll()
        {
            IActionResult result = controller.GetAllResults();

            Assert.IsType<OkObjectResult>(result);
        }

        // Test 2: Reject a result ID less than or equal to zero.
        [Fact]
        public void test_ResultGetByInvalidId()
        {
            IActionResult result = controller.GetResult(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 3: Reject a result with an invalid position.
        [Fact]
        public void test_ResultAdd_InvalidPosition()
        {
            ResultController testController = CreateOrganizerController();

            ResultsModel newResult = new ResultsModel
            {
                Position = 0,
                Score = 999,
                participantId = 3003
            };

            IActionResult result = testController.AddResult(newResult);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 4: Reject a result with a negative score.
        [Fact]
        public void test_ResultAdd_NegativeScore()
        {
            ResultController testController = CreateOrganizerController();

            ResultsModel newResult = new ResultsModel
            {
                Position = 3,
                Score = -1,
                participantId = 3003
            };

            IActionResult result = testController.AddResult(newResult);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 5: Reject updating a result with an invalid ID.
        [Fact]
        public void test_ResultUpdateInvalidId()
        {
            ResultController testController = CreateOrganizerController();

            ResultsModel updatedResult = new ResultsModel
            {
                Position = 3,
                Score = 95,
                participantId = 3003
            };

            IActionResult result =
                testController.UpdateResult(-1, updatedResult);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 6: Reject deleting a result with an invalid ID.
        [Fact]
        public void test_ResultDeleteInvalidId()
        {
            ResultController testController = CreateOrganizerController();

            IActionResult result = testController.DeleteResult(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Helper: create a controller with an Organizer identity.
        // Direct method calls do not execute authorization middleware.
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
