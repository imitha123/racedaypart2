using Xunit;
using poepart2.Controllers;
using poepart2.Model;

namespace Testing_Poe
{
    public class testing_participant_controller
    {
        // Test getting a participant by email
        [Fact]
        public void test_ParticipantGetByEmail()
        {
            ParticipantController controller = new ParticipantController();


        // Use a participant email that currently exists in the database.
        string results = controller.GetParticipant("Abdi@gmail.com");

            Assert.DoesNotContain("Participant not found.", results);
            Assert.Contains("Participant found:", results);
            Assert.Contains("Email:", results);
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

            Assert.Equal("Participant added successfully!", results);
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

            Assert.Equal(
                "Invalid email. Email must end with @gmail.com.",
                results);
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

            Assert.Equal(
                "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.",
                results);
        }

        // Test getting a participant that does not exist
        [Fact]
        public void test_ParticipantGet_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            string results =
                controller.GetParticipant("nobody123@gmail.com");

            Assert.Equal("Participant not found.", results);
        }

        // Test updating a participant that does not exist
        // This avoids modifying existing participant records.
        [Fact]
        public void test_ParticipantUpdate_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            ParticipantModel updatedParticipant = new ParticipantModel
            {
                Participantname = "Updated",
                Participantsurname = "Participant",
                participantEmail = "updated@gmail.com",
                participantPassword = "UpdatedPass123!"
            };

            string results = controller.UpdateParticipant(
                -1,
                updatedParticipant);

            Assert.Equal("Participant not found.", results);
        }

        // Test deleting a participant that does not exist
        // This avoids deleting a participant referenced by Enrollment.
        [Fact]
        public void test_ParticipantDelete_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.DeleteParticipant(-1);

            Assert.Equal("Participant not found.", results);
        }
    }


}
