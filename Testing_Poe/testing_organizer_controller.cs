
using Xunit;
using testingPOE.Controllers;

namespace Testing_Poe
{
    public class testing_organizer_controller
    {
        // Test 1: Get all organizers
        [Fact]
        public void test_OrganizerGetAll()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.getOrganizer();

            Assert.Contains("ID: 2003", results);
            Assert.Contains("Name: Updated Organizer", results);
            Assert.Contains("Email: updated@gmail.com", results);
        }

        // Test 2: Add an organizer with valid email and password
        [Fact]
        public void test_OrganizerAdd()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "TestOrganizer",
                "organizer_test_2026@gmail.com",
                "Password123!",
                "TestSurname");

            Assert.Equal("Organizer added successfully.", results);
        }

        // Test 3: Get an organizer using email and password
        [Fact]
        public void test_OrganizerGetByEmailAndPassword()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.GetOrganizerByEmailAndPassword(
                "updated@gmail.com",
                "UpdatedPass123!");

            Assert.Equal(
                "Organizer found: ID: 2003, Name: Updated Organizer, Email: updated@gmail.com",
                results);
        }

        // Test 4: Reject an invalid email when adding an organizer
        [Fact]
        public void test_OrganizerAdd_InvalidEmail()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David",
                "david@yahoo.com",
                "Password123!",
                "Brown");

            Assert.Equal(
                "Invalid email. Email must end with @gmail.com.",
                results);
        }

        // Test 5: Reject an invalid password when adding an organizer
        [Fact]
        public void test_OrganizerAdd_InvalidPassword()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David",
                "david@gmail.com",
                "password",
                "Brown");

            Assert.Equal(
                "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.",
                results);
        }

        // Test 6: Reject incorrect credentials
        [Fact]
        public void test_OrganizerGetByIncorrectCredentials()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.GetOrganizerByEmailAndPassword(
                "updated@gmail.com",
                "WrongPassword123!");

            Assert.Equal("Invalid email or password.", results);
        }

        // Test 7: Delete a non-existing organizer
        [Fact]
        public void test_OrganizerDeleteNonExisting()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.DeleteOrganizer(-1);

            Assert.Equal("Organizer not found.", results);
        }

        // Test 8: Update a non-existing organizer
        [Fact]
        public void test_OrganizerUpdateNonExisting()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.UpdateOrganizer(
                -1,
                "Updated Organizer",
                "updated_test_2026@gmail.com",
                "UpdatedPass123!");

            Assert.Equal("Organizer not found.", results);
        }
    }
}

