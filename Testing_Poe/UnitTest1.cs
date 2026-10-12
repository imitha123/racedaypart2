
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


        [Fact]
        public void test_OrganizerAdd()
        {
            // Instance with object
            OrganizerController add_organizer = new OrganizerController();

            // Add request method
            string results = add_organizer.AddOrganizer(
                "David Brown",
                "david@example.com"
            );

            string expected_results = "Organizer added successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_OrganizerGetById()
        {
            // Instance with object
            OrganizerController get_organizer = new OrganizerController();

            // Get request method
            string results = get_organizer.GetOrganizerById(1);

            string expected_results =
                "ID: 1, Name: John Smith, Email: john@example.com";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_OrganizerDelete()
        {
            // Instance with object
            OrganizerController delete_organizer = new OrganizerController();

            // Delete request method
            string results = delete_organizer.DeleteOrganizer(1);

            string expected_results = "Organizer deleted successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_OrganizerUpdate()
        {
            // Instance with object
            OrganizerController update_organizer = new OrganizerController();

            // Update request method
            string results = update_organizer.UpdateOrganizer(
                1,
                "Updated Organizer",
                "updated@example.com"
            );

            string expected_results = "Organizer updated successfully.";

            Assert.Equal(expected_results, results);
        }

        // NEW TEST
        // Participant controller test cases

        [Fact]
        public void test_ParticipantAdd()
        {
            // Instance with object
            ParticipantController add_participant = new ParticipantController();

            // Add request method
            string results = add_participant.AddParticipant(
                "John",
                "Smith"
            );

            string expected_results = "Participant added successfully!";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_ParticipantGet()
        {
            // Instance with object
            ParticipantController get_participant = new ParticipantController();

            // Get request method
            string results = get_participant.GetParticipant(
                "John",
                "Smith"
            );

            string expected_results = "Participant found: John Smith";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_ParticipantUpdate()
        {
            // Instance with object
            ParticipantController update_participant = new ParticipantController();

            // Create updated participant object
            ParticipantModel updatedParticipant = new ParticipantModel
            {
                Participantname = "David",
                Participantsurname = "Brown"
            };

            // Update request method
            string results = update_participant.UpdateParticipant(
                1,
                updatedParticipant
            );

            string expected_results = "Participant updated successfully!";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_ParticipantDelete()
        {
            // Instance with object
            ParticipantController delete_participant = new ParticipantController();

            // Delete request method
            string results = delete_participant.DeleteParticipant(1);

            string expected_results = "Participant deleted successfully!";

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