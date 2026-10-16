using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
            IActionResult result = controller.GetAllEvents();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("Updated Chess Event", json);
            Assert.Contains("Testing Chess Event", json);
        }

        // Test 2: Get event by ID
        [Fact]
        public void test_EventGetById()
        {
            IActionResult result = controller.GetEventById(2003);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("Updated Chess Event", json);
            Assert.Contains("Braam", json);
        }

        // Test 3: Get event using an invalid ID
        [Fact]
        public void test_EventGetByInvalidId()
        {
            IActionResult result = controller.GetEventById(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 4: Add an event with invalid data
        [Fact]
        public void test_AddEvent_InvalidData()
        {
            EventModel newEvent = new EventModel
            {
                EventName = "",
                EventLocation = "Braam",
                EventDate = "27-07-12",
                categoryID = 1003,
                organizerID = 2003
            };

            IActionResult result = controller.AddEvent(newEvent);

            Assert.IsType<BadRequestObjectResult>(result);
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

            IActionResult result = controller.UpdateEvent(-1, updatedEvent);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 6: Delete a non-existing event
        [Fact]
        public void test_DeleteNonExistingEvent()
        {
            IActionResult result = controller.DeleteEvent(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }


}
