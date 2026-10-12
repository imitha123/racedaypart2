
using poepart2.Controllers;
using poepart2.Model;
using testingPOE.Controllers;
namespace poepart2
{
    public class UnitTest1
    {
        // category controller test cases
        [Fact]
        public void test_CategoryGetAll()
        {
            // instance with object
            CategoryController get_all_categories = new CategoryController();

            // get request method
            string results = get_all_categories.getAllCategories();

            string expected_results = "ID: 1, Name: Electronics, Description: Electronic products\n" +
                                      "ID: 2, Name: Furniture, Description: Furniture products\n";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_CategoryGetById()
        {
            // instance with object
            CategoryController get_category = new CategoryController();

            // get request method
            string results = get_category.getCategory(1);

            string expected_results =
                "ID: 1, Name: Electronics, Description: Electronic products";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_CategoryAdd()
        {
            // instance with object
            CategoryController add_category = new CategoryController();

            // add request method
            string results = add_category.addCategory(
                "Clothing",
                "Clothing products"
            );

            string expected_results = "Category added successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_CategoryDelete()
        {
            // instance with object
            CategoryController delete_category = new CategoryController();

            // delete request method
            string results = delete_category.deleteCategory(1);

            string expected_results = "Category deleted successfully.";

            Assert.Equal(expected_results, results);
        }

        // NEW TEST
        // Event controller test cases

        [Fact]
        public void test_EventGetAll()
        {
            // Instance with object
            EventController get_all_events = new EventController();

            // Get request method
            string results = get_all_events.getAllEvents();

            string expected_results =
                "ID: 1, Name: Music Festival, Location: Johannesburg, Date: 2026-11-15\n" +
                "ID: 2, Name: Sports Event, Location: Cape Town, Date: 2026-12-10\n";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EventGetById()
        {
            // Instance with object
            EventController get_event = new EventController();

            // Get request method
            string results = get_event.getEventById(1);

            string expected_results =
                "ID: 1, Name: Music Festival, Location: Johannesburg, Date: 2026-11-15";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EventAdd()
        {
            // Instance with object
            EventController add_event = new EventController();

            // Create event object
            EventModel newEvent = new EventModel
            {
                EventName = "Coding Workshop",
                EventLocation = "Pretoria",
                EventDate = "2026-12-20"
            };

            // Call add event method
            string results = add_event.addEvent(newEvent);

            string expected_results = "Event added successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EventUpdateById()
        {
            // Instance with object
            EventController update_event = new EventController();

            // Create updated event object
            EventModel updatedEvent = new EventModel
            {
                EventName = "Updated Music Festival",
                EventLocation = "Durban",
                EventDate = "2026-12-25"
            };

            // Call update event method
            string results = update_event.updateEvent(1, updatedEvent);

            string expected_results = "Event updated successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EventUpdateUsingModelId()
        {
            // Instance with object
            EventController update_event = new EventController();

            // Create updated event object
            EventModel updatedEvent = new EventModel
            {
                ÉventId = 1,
                EventName = "Updated Sports Event",
                EventLocation = "Bloemfontein",
                EventDate = "2026-12-30"
            };

            // Call update event method
            string results = update_event.updateEvent(updatedEvent);

            string expected_results = "Event updated successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EventDelete()
        {
            // Instance with object
            EventController delete_event = new EventController();

            // Call delete event method
            string results = delete_event.deleteEvent(1);

            string expected_results = "Event deleted successfully.";

            Assert.Equal(expected_results, results);
        }

        // NEW TEST
        // Organizer controller test cases
        [Fact]
        public void test_OrganizerGetAll()
        {
            // Instance with object
            OrganizerController get_all_organizers = new OrganizerController();

            // Get request method
            string results = get_all_organizers.getOrganizer();

            string expected_results =
                "ID: 1, Name: John Smith, Email: john@example.com\n" +
                "ID: 2, Name: Mary Jones, Email: mary@example.com\n";

            Assert.Equal(expected_results, results);
        }


        // Test getting all organizers
        [Fact]
        public void testOrganizerGetAll()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.getOrganizer();

            string expected_results =
                "ID: 1, Name: John Smith, Email: john@gmail.com\n" +
                "ID: 2, Name: Mary Jones, Email: mary@gmail.com\n";

            Assert.Equal(expected_results, results);
        }

        // Test adding an organizer with valid email and password
        [Fact]
        public void test_OrganizerAdd()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David Brown",
                "david@gmail.com",
                "Password123!");

            string expected_results = "Organizer added successfully.";

            Assert.Equal(expected_results, results);
        }

        // Test getting an organizer using email and password
        [Fact]
        public void test_OrganizerGetByEmailAndPassword()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.GetOrganizerByEmailAndPassword(
                "john@gmail.com",
                "Password123!");

            string expected_results =
                "Organizer found: ID: 1, Name: John Smith, Email: john@gmail.com";

            Assert.Equal(expected_results, results);
        }

        // Test rejecting an invalid email when adding an organizer
        [Fact]
        public void test_OrganizerAdd_InvalidEmail()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David Brown",
                "david@yahoo.com",
                "Password123!");

            string expected_results =
                "Invalid email. Email must end with @gmail.com.";

            Assert.Equal(expected_results, results);
        }

        // Test rejecting an invalid password when adding an organizer
        [Fact]
        public void test_OrganizerAdd_InvalidPassword()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David Brown",
                "david@gmail.com",
                "password");

            string expected_results =
                "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.";

            Assert.Equal(expected_results, results);
        }

        // Test getting an organizer with incorrect credentials
        [Fact]
        public void test_OrganizerGetByIncorrectCredentials()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.GetOrganizerByEmailAndPassword(
                "john@gmail.com",
                "WrongPassword123!");

            string expected_results = "Invalid email or password.";

            Assert.Equal(expected_results, results);
        }

        // Test deleting an organizer
        [Fact]
        public void test_OrganizerDelete()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.DeleteOrganizer(1);

            string expected_results = "Organizer deleted successfully.";

            Assert.Equal(expected_results, results);
        }

        // Test updating an organizer
        [Fact]
        public void test_OrganizerUpdate()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.UpdateOrganizer(
                1,
                "Updated Organizer",
                "updated@gmail.com",
                "UpdatedPass123!");

            string expected_results = "Organizer updated successfully.";

            Assert.Equal(expected_results, results);
        }

        // NEW TEST
        // Participant controller test cases

        // Test getting a participant by email
        [Fact]
        public void test_ParticipantGetByEmail()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.GetParticipant("john@gmail.com");

            string expected_results =
                "Participant found: John Smith, Email: john@gmail.com";

            Assert.Equal(expected_results, results);
        }

        // Test adding a participant with valid email and password
        [Fact]
        public void test_ParticipantAdd()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.AddParticipant(
                "David",
                "Brown",
                "david@gmail.com",
                "Password123!");

            string expected_results =
                "Participant added successfully!";

            Assert.Equal(expected_results, results);
        }

        // Test rejecting an invalid email
        [Fact]
        public void test_ParticipantAdd_InvalidEmail()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.AddParticipant(
                "David",
                "Brown",
                "david@yahoo.com",
                "Password123!");

            string expected_results =
                "Invalid email. Email must end with @gmail.com.";

            Assert.Equal(expected_results, results);
        }

        // Test rejecting an invalid password
        [Fact]
        public void test_ParticipantAdd_InvalidPassword()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.AddParticipant(
                "David",
                "Brown",
                "david@gmail.com",
                "password");

            string expected_results =
                "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.";

            Assert.Equal(expected_results, results);
        }

        // Test getting a participant that does not exist
        [Fact]
        public void test_ParticipantGet_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            string results =
                controller.GetParticipant("nobody123@gmail.com");

            string expected_results = "Participant not found.";

            Assert.Equal(expected_results, results);
        }

        // Test updating a participant
        [Fact]
        public void test_ParticipantUpdate()
        {
            ParticipantController controller = new ParticipantController();

            var updatedParticipant = new poepart2.Model.ParticipantModel
            {
                Participantname = "Updated",
                Participantsurname = "Participant",
                participantEmail = "updated@gmail.com",
                participantPassword = "UpdatedPass123!"
            };

            string results = controller.UpdateParticipant(
                1,
                updatedParticipant);

            string expected_results =
                "Participant updated successfully!";

            Assert.Equal(expected_results, results);
        }

        // Test deleting a participant
        [Fact]
        public void test_ParticipantDelete()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.DeleteParticipant(1);

            string expected_results =
                "Participant deleted successfully!";

            Assert.Equal(expected_results, results);
        }

        // NEW TEST
        // Enrollment controller test cases
        [Fact]
        public void test_EnrollmentGetAll()
        {
            // Instance with object
            EnrollmentController get_all_enrollments = new EnrollmentController();

            // Get request method
            string results = get_all_enrollments.getAllEnrollments();

            string expected_results =
                "EventID: 1, ParticipantID: 1\n" +
                "EventID: 2, ParticipantID: 2\n";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EnrollmentGetByIds()
        {
            // Instance with object
            EnrollmentController get_enrollment = new EnrollmentController();

            // Get request method
            string results = get_enrollment.getEnrollment(1, 1);

            string expected_results = "EventID: 1, ParticipantID: 1";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_EnrollmentDelete()
        {
            // Instance with object
            EnrollmentController delete_enrollment = new EnrollmentController();

            // Delete request method
            string results = delete_enrollment.deleteEnrollment(1, 1);

            string expected_results = "Enrollment deleted successfully.";

            Assert.Equal(expected_results, results);
        }

        // NEW TEST
        // Results controller test cases
        [Fact]
        public void test_ResultGetAll()
        {
            // Instance with object
            ResultController get_all_results = new ResultController();

            // Get request method
            string results = get_all_results.getAllResults();

            string expected_results =
                "ID: 1, Position: First, Score: 100\n" +
                "ID: 2, Position: Second, Score: 80\n";

            Assert.Equal(expected_results, results);
        }

        [Fact]
        public void test_ResultGetById()
        {
            // Instance with object
            ResultController get_result = new ResultController();

            // Get request method
            string results = get_result.getResult(1);

            string expected_results =
                "ID: 1, Position: First, Score: 100";

            Assert.Equal(expected_results, results);
        }

        [Fact]
        public void test_ResultUpdate()
        {
            // Instance with object
            ResultController update_result = new ResultController();

            // Create an object with updated result details
            ResultsModel updatedResult = new ResultsModel
            {
                Position = "Updated First",
                Score = 95
            };

            // Update request method
            string results = update_result.updateResult(1, updatedResult);

            string expected_results = "Result updated successfully.";

            Assert.Equal(expected_results, results);
        }

        [Fact]
        public void test_ResultDelete()
        {
            // Instance with object
            ResultController delete_result = new ResultController();

            // Delete request method
            string results = delete_result.deleteResult(1);

            string expected_results = "Result deleted successfully.";

            Assert.Equal(expected_results, results);
        }
    }
}