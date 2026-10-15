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

            Assert.Contains("chess", result);
            Assert.Contains("Braam", result);
        }

        // Test 2: Get event by ID
        [Fact]
        public void test_EventGetById()
        {
            string result = controller.getEventById(2003);

            Assert.Contains("chess", result);
            Assert.Contains("Braam", result);
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

        // Test 5: Update an event
        [Fact]
        public void test_UpdateEvent()
        {
            EventModel updatedEvent = new EventModel
            {
                EventName = "Updated Chess Event",
                EventLocation = "Braam",
                EventDate = "27-07-12",
                categoryID = 1003,
                organizerID = 2003
            };

            string result = controller.updateEvent(2003, updatedEvent);

            Assert.Equal("Event updated successfully.", result);
        }

        // Test 6: Delete an event that does not exist
        [Fact]
        public void test_DeleteNonExistingEvent()
        {
            string result = controller.deleteEvent(-1);

            Assert.Equal("Event not found.", result);
        }
    }


}
