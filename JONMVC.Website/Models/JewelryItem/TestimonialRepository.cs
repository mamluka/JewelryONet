using System;
using System.Collections.Generic;
using AutoMapper;
using JONMVC.Website.Models.DB;
using System.Linq;
namespace JONMVC.Website.Models.JewelryItem
{
    class TestimonialRepository : ITestimonialRepository
    {
        private readonly IMappingEngine mapper;

        public TestimonialRepository(IMappingEngine mapper)
        {
            this.mapper = mapper;
        }

        public List<Testimonial> GetRandomTestimonails(int howMany)
        {
            using (var db = new JONEntities())
            {
                var testimonialsFromDB = db.usr_TESTIMONIALS.OrderBy(x => Guid.NewGuid()).Take(howMany).ToList();
                return mapper.Map<List<usr_TESTIMONIALS>, List<Testimonial>>(testimonialsFromDB);
            }
           
        }
    }
}