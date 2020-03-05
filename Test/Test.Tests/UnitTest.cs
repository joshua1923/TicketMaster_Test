using Newtonsoft.Json.Schema;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using Test.ServiceInterface;
using Test.ServiceInterface.Classes;
using Test.ServiceInterface.Interfaces;
using Test.ServiceModel;
using Test.ServiceModel.Models;

namespace Test.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<TestService>();
            appHost.Container.RegisterAutoWiredAs<Rules, IRules>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        
        [Test]
        [TestCase(new int[] { 1, 20 })]
        public void Can_call_TestServices(int[] value)
        {
            // Arrange
            var service = appHost.Container.Resolve<TestService>();

            // Act
            var response = service.Any(new TestRequest { Range = value });

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(TestResponse), response.GetType());
        }

        [Test]
        public void TestServices_InputRangeOf1To20_ReturnSummary()
        {
            // Arrange
            var service = appHost.Container.Resolve<TestService>();
            var expected = new Summary { Fizz = 5, Buzz = 3, FizzBuzz = 1, Integer = 11 };

            // Act
            var response = service.Any(new TestRequest { Range = new[] { 1, 20 } });

            // Assert
            Assert.AreEqual(expected.Fizz, response.Summary.Fizz);
            Assert.AreEqual(expected.Buzz, response.Summary.Buzz);
            Assert.AreEqual(expected.FizzBuzz, response.Summary.FizzBuzz);
            Assert.AreEqual(expected.Integer, response.Summary.Integer);
        }

        [Test]
        public void TestServices_InputRangeOf1To20_ReturnResult()
        {
            // Arrange
            var service = appHost.Container.Resolve<TestService>();
            var expected = "1 2 fizz 4 buzz fizz 7 8 fizz buzz 11 fizz 13 14 fizzbuzz 16 17 fizz 19 buzz";

            // Act
            var response = service.Any(new TestRequest { Range = new[] { 1, 20 } });

            // Assert
            Assert.That(response.Result, Is.EqualTo(expected));
        }
    }
}
