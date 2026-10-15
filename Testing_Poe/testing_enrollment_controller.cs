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


        string addResult = controller.addEnrollment(2002, 2002);

            // If the enrollment already exists, it may violate
            // the table's primary key or unique constraint.
            if (addResult == "Enrollment added successfully.")
            {
                string results = controller.getAllEnrollments();

                Assert.Contains(
                    "EventID: 2002, ParticipantID: 2002",
                    results
                );

                controller.deleteEnrollment(2002, 2002);
            }
            else
            {
                Assert.Fail("Could not create enrollment for testing. " + addResult);
            }
        }

        // Test 2: Get enrollment by event ID and participant ID
        [Fact]
        public void test_EnrollmentGetByIds()
        {
            EnrollmentController controller = new EnrollmentController();

            string addResult = controller.addEnrollment(2002, 2002);

            if (addResult == "Enrollment added successfully.")
            {
                string results = controller.getEnrollment(2002, 2002);

                Assert.Equal(
                    "EventID: 2002, ParticipantID: 2002",
                    results
                );

                controller.deleteEnrollment(2002, 2002);
            }
            else
            {
                Assert.Fail("Could not create enrollment for testing. " + addResult);
            }
        }

        // Test 3: Delete an enrollment
        [Fact]
        public void test_EnrollmentDelete()
        {
            EnrollmentController controller = new EnrollmentController();

            string addResult = controller.addEnrollment(2002, 2002);

            if (addResult == "Enrollment added successfully.")
            {
                string results = controller.deleteEnrollment(2002, 2002);

                Assert.Equal("Enrollment deleted successfully.", results);
            }
            else
            {
                Assert.Fail("Could not create enrollment for testing. " + addResult);
            }
        }
    }


}
