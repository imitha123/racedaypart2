using poepart2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Poe
{
    public class testing_enrollment_controller
    {
        
// Enrollment controller test cases

[Fact]
public void test_EnrollmentGetAll()
        {
            EnrollmentController get_all_enrollments = new EnrollmentController();

            string results = get_all_enrollments.getAllEnrollments();

            string expected_results =
                "EventID: 2002, ParticipantID: 2002\n";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EnrollmentGetByIds()
        {
            EnrollmentController get_enrollment = new EnrollmentController();

            string results = get_enrollment.getEnrollment(2002, 2002);

            string expected_results =
                "EventID: 2002, ParticipantID: 2002";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EnrollmentDelete()
        {
            EnrollmentController delete_enrollment = new EnrollmentController();

            string results = delete_enrollment.deleteEnrollment(2002, 2002);

            string expected_results = "Enrollment deleted successfully.";

            Assert.Equal(expected_results, results);
        }


    }
}
