using System.Collections.Generic;

namespace JONMVC.Website.Models.JewelryItem
{
    public interface ITestimonialRepository
    {
        List<Testimonial> GetRandomTestimonails(int howMany);
    }
}