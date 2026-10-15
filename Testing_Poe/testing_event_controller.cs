
using Xunit;
using poepart2.Controllers;
using poepart2.Model;

namespace Testing_Poe
{
    public class EventControllerTests
    {
        private readonly EventController controller;

        public EventControllerTests()
        {
            controller = new EventController();
        }

        // Test 1: Get all events
        [Fact]
        public void test_EventGetAll()
        {
            string result = controller.getAllEvents();

            Assert.Contains(
                "ID: 2003, Name: Updated Chess Event, Location: Braam, Date: 27-07-12",
                result);

            Assert.Contains(
                "ID: 3002, Name: Testing Chess Event, Location: Braam, Date: 27-07-12",
                result);
        }

        // Test 2: Get event by ID
        [Fact]
        public void test_EventGetById()
        {
            string result = controller.getEventById(2003);

            Assert.Equal(
                "ID: 2003, Name: Updated Chess Event, Location: Braam, Date: 27-07-12",
                result.Trim());
        }

        // Test 3: Get event using a non-existing ID
        [Fact]
        public void test_EventGetByInvalidId()
        {
            string result = controller.getEventById(-1);

            Assert.Equal("Event not found.", result);
        }

        // Test 4: Add an event
        [Fact]
        public void test_AddEvent()
        {
            EventModel newEvent = new EventModel
            {
                EventName = "Testing Chess Event",
                EventLocation = "Braam",
                EventDate = "27-07-12",
                categoryID = 1003,
                organizerID = 2003
            };

            string result = controller.addEvent(newEvent);

            Assert.Equal("Event added successfully.", result);
        }

        // Test 5: Update a non-existing event
        [Fact]
        public void test_UpdateNonExistingEvent()
        {
            EventModel updatedEvent = new EventModel
            {
                EventName = "Temporary Test Event",
                EventLocation = "Braam",
                EventDate = "27-07-12",
                categoryID = 1003,
                organizerID = 2003
            };

            string result = controller.updateEvent(-1, updatedEvent);

            Assert.Equal("Event not found.", result);
        }

        // Test 6: Delete a non-existing event
        [Fact]
        public void test_DeleteNonExistingEvent()
        {
            string result = controller.deleteEvent(-1);

            Assert.Equal("Event not found.", result);
        }
    }
}

