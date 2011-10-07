namespace JONMVC.Website.Models.AutoMapperMaps
{
    public abstract class MergeTwoObjectsForAutoMapper<TFirst, TSecond>
    {
        public abstract TFirst First { get; set; }
        public abstract TSecond Second { get; set; }

    }
}