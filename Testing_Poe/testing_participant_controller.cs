
using Xunit;
using poepart2.Controllers;
using poepart2.Model;

namespace Testing_Poe
{
    public class testing_participant_controller
    {
        // Test 1: Get a participant by an existing email
        [Fact]
        public void test_ParticipantGetByEmail()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.GetParticipant("maneli04@gmail.com", "Imitha123@18");

            Assert.Equal("Participant found: Imitha               Maneli              , Email: maneli04@gmail.com  ", results);
           
        }

        // Test 2: Add a participant with a valid email and password
        [Fact]
        public void test_ParticipantAdd()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.AddParticipant(
                "TestName",
                "TestSurname",
                "test_2026@gmail.com",
                "Password123!");

            Assert.Equal("Participant added successfully!", results);
        }

        // Test 3: Reject an invalid email
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

        // Test 4: Reject an invalid password
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

        // Test 5: Get a participant that does not exist
        [Fact]
        public void test_ParticipantGet_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            string results =
                controller.GetParticipant("nobody123456789@gmail.com", "SomePassword123!");

            Assert.Equal("Participant not found.", results);
        }

        // Test 6: Update a participant that does not exist
        [Fact]
        public void test_ParticipantUpdate_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            ParticipantModel updatedParticipant = new ParticipantModel
            {
                Participantname = "Updated",
                Participantsurname = "Participant",
                participantEmail = "2026@gmail.com",
                participantPassword = "UpdatePass123!"
            };

            string results = controller.UpdateParticipant(
                -1,
                updatedParticipant);

            Assert.Equal("Participant not found.", results);
        }

        // Test 7: Delete a participant that does not exist
        [Fact]
        public void test_ParticipantDelete_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.DeleteParticipant(-1);

            Assert.Equal("Participant not found.", results);
        }
    }
}

