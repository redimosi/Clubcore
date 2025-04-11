using Clubcore.Domain.AggregatesModel;

namespace Clubcore.Tests
{
    [TestFixture]
    public class PhoneNumberTests
    {
        [TestCase("+41791234567", true)] // Already in international format
        [TestCase("0041791234567", true)] // Starts with 00
        [TestCase("0791234567", true)] // Local Swiss number
        [TestCase("079 123 45 67", true)] // Local Swiss number with spaces
        [TestCase("1234567890", false)] // Invalid number
        public void PhoneNumberIsValid(string input, bool expected)
        {
            // Act
            var result = PhoneNumber.IsValid(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("+41791234567", "+41791234567")] // Already in international format
        [TestCase("0041791234567", "+41791234567")] // Starts with 00
        [TestCase("0791234567", "+41791234567")] // Local Swiss number
        [TestCase("079 123 45 67", "+41791234567")] // Local Swiss number with spaces
        [TestCase("1234567890", null)] // Invalid number

        public void PhoneNumberCorrectFormat(string input, string? expected)
        {
            if (expected == null)
            {
                Assert.Throws<ArgumentException>(() => new PhoneNumber(input));
            }
            else
            {
                // Act
                var result = new PhoneNumber(input);

                // Assert
                Assert.That(result.Number, Is.EqualTo(expected));
            }

        }
    }
}
