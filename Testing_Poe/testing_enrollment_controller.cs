
using Xunit;
using poepart2.Controllers;

namespace Testing_Poe
{
    public class testing_enrollment_controller
    {
        // Test 1: Get all enrollments
        [Fact]
        public void test_EnrollmentGetAll()
        {
            EnrollmentController controller = new EnrollmentController();

            string results = controller.getAllEnrollments();

            Assert.Contains("EventID: 3002, ParticipantID: 3005", results);
            Assert.Contains("EventID: 3003, ParticipantID: 3006", results);
            Assert.Contains("EventID: 3006, ParticipantID: 2002", results);
        }

        // Test 2: Get enrollment by event ID and participant ID
        [Fact]
        public void test_EnrollmentGetByIds()
        {
            EnrollmentController controller = new EnrollmentController();

            string results = controller.getEnrollment(3002, 3005);

            Assert.Equal(
                "EventID: 3002, ParticipantID: 3005",
                results
            );
        }

        // Test 3: Get a nonexistent enrollment
        [Fact]
        public void test_EnrollmentGet_NotFound()
        {
            EnrollmentController controller = new EnrollmentController();

            string results = controller.getEnrollment(-1, -1);

            Assert.Equal("Enrollment not found.", results);
        }

        // Test 4: Add enrollment
        [Fact]
        public void test_EnrollmentAdd()
        {
            EnrollmentController controller = new EnrollmentController();

            // Use an existing event and participant that are not
            // currently paired in the Enrollment table.
            string results = controller.addEnrollment(3002, 3006);

            Assert.Equal("Enrollment added successfully.", results);

            // Clean up the record created by this test.
            string deleteResult = controller.deleteEnrollment(3002, 3006);

            Assert.Equal("Enrollment deleted successfully.", deleteResult);
        }

        // Test 5: Update a nonexistent enrollment
        [Fact]
        public void test_EnrollmentUpdate_NotFound()
        {
            EnrollmentController controller = new EnrollmentController();

            string results = controller.updateEnrollment(
                -1,
                -1,
                -2,
                -2
            );

            Assert.Equal("Enrollment not found.", results);
        }

        // Test 6: Delete a nonexistent enrollment
        [Fact]
        public void test_EnrollmentDelete_NotFound()
        {
            EnrollmentController controller = new EnrollmentController();

            string results = controller.deleteEnrollment(-1, -1);

            Assert.Equal("Enrollment not found.", results);
        }
    }
}

