using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    [TestFixture]
    public class TestimonialRepositoryTests : JewelryItemTestsBase
    {
        [Test]
        public void GetRandomTestimonails_ShouldReturnThreeTestimonials()
        {
            //Arrange
            var testimonialRepository = new FakeTestimonialRepository(mapper);
            var howMany = 3;
            //Act
            var testamonials = testimonialRepository.GetRandomTestimonails(howMany);
            //Assert
            testamonials.Should().HaveCount(3);

        }

        [Test]
        public void GetRandomTestimonails_ShouldReturnThreeTestimonialsWithNonRepeatingIDS()
        {
            //Arrange
            var testimonialRepository = new FakeTestimonialRepository(mapper);
            var howMany = 3;
            //Act
            var testamonials = testimonialRepository.GetRandomTestimonails(howMany);
            //Assert
            testamonials.Should().OnlyHaveUniqueItems();

        }
    }
}