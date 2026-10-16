using Xunit;
using Microsoft.AspNetCore.Mvc;
using poepart2.Controllers;
using System.Text.Json;

namespace Testing_Poe
{
    public class testing_enrollment_controller
    {
        // Test 1: Get all enrollments
        [Fact]
        public void test_EnrollmentGetAll()
        {
            EnrollmentController controller = new EnrollmentController();


        IActionResult result = controller.GetAllEnrollments();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("3002", json);
            Assert.Contains("3005", json);
            Assert.Contains("3003", json);
            Assert.Contains("3006", json);
        }

        // Test 2: Get enrollment by event ID and participant ID
        [Fact]
        public void test_EnrollmentGetByIds()
        {
            EnrollmentController controller = new EnrollmentController();

            IActionResult result = controller.GetEnrollment(3002, 3005);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("3002", json);
            Assert.Contains("3005", json);
        }

        // Test 3: Get an enrollment with invalid IDs
        [Fact]
        public void test_EnrollmentGet_InvalidIds()
        {
            EnrollmentController controller = new EnrollmentController();

            IActionResult result = controller.GetEnrollment(-1, -1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 4: Add enrollment
        [Fact]
        public void test_EnrollmentAdd()
        {
            EnrollmentController controller = new EnrollmentController();

            // Use an existing event and participant that are not
            // already paired in the Enrollment table.
            IActionResult result = controller.AddEnrollment(3002, 3006);

            CreatedAtActionResult createdResult =
                Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(201, createdResult.StatusCode);

            // Clean up the record created by this test.
            IActionResult deleteResult =
                controller.DeleteEnrollment(3002, 3006);

            Assert.IsType<OkObjectResult>(deleteResult);
        }

        // Test 5: Update an enrollment that does not exist
        [Fact]
        public void test_EnrollmentUpdate_NotFound()
        {
            EnrollmentController controller = new EnrollmentController();

            IActionResult result = controller.UpdateEnrollment(
                -1,
                -1,
                -2,
                -2
            );

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 6: Delete an enrollment with invalid IDs
        [Fact]
        public void test_EnrollmentDelete_InvalidIds()
        {
            EnrollmentController controller = new EnrollmentController();

            IActionResult result = controller.DeleteEnrollment(-1, -1);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

}
