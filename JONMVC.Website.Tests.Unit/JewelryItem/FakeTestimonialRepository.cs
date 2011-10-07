using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.JewelryItem;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    public class FakeTestimonialRepository : ITestimonialRepository
    {
        private readonly IMappingEngine mapper;
        private static readonly Fixture fixture = new Fixture();

        private readonly List<usr_TESTIMONIALS> dbmock = new List<usr_TESTIMONIALS>
                                                    {
                                                        {
                                                            fixture.Build<usr_TESTIMONIALS>().With(x => x.id, 1).Without
                                                            (x => x.EntityKey).Without(x => x.sys_COUNTRY).Without(
                                                                x => x.sys_COUNTRYReference).CreateAnonymous()
                                                            },
                                                        {
                                                            fixture.Build<usr_TESTIMONIALS>().With(x => x.id, 2).Without
                                                            (x => x.EntityKey).Without(x => x.sys_COUNTRY).Without(
                                                                x => x.sys_COUNTRYReference).CreateAnonymous()
                                                            },
                                                        {
                                                            fixture.Build<usr_TESTIMONIALS>().With(x => x.id, 3).Without
                                                            (x => x.EntityKey).Without(x => x.sys_COUNTRY).Without(
                                                                x => x.sys_COUNTRYReference).CreateAnonymous()
                                                            },
                                                        {
                                                            fixture.Build<usr_TESTIMONIALS>().With(x => x.id, 4).Without
                                                            (x => x.EntityKey).Without(x => x.sys_COUNTRY).Without(
                                                                x => x.sys_COUNTRYReference).CreateAnonymous()
                                                            },
                                                        {
                                                            fixture.Build<usr_TESTIMONIALS>().With(x => x.id, 5).Without
                                                            (x => x.EntityKey).Without(x => x.sys_COUNTRY).Without(
                                                                x => x.sys_COUNTRYReference).CreateAnonymous()
                                                            },
                                                        {
                                                            fixture.Build<usr_TESTIMONIALS>().With(x => x.id, 6).Without
                                                            (x => x.EntityKey).Without(x => x.sys_COUNTRY).Without(
                                                                x => x.sys_COUNTRYReference).CreateAnonymous()
                                                            },
                                                    };

        public FakeTestimonialRepository(IMappingEngine mapper)
        {
            this.mapper = mapper;
        }

        public List<Testimonial> GetRandomTestimonails(int howMany)
        {
            var testimonialsFromDB = dbmock.OrderBy(x => Guid.NewGuid()).Take(howMany).ToList();
            return mapper.Map<List<usr_TESTIMONIALS>, List<Testimonial>>(testimonialsFromDB);

        }
    }
}