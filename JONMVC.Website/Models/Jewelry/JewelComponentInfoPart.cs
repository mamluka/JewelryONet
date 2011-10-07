namespace JONMVC.Website.Models.Jewelry
{
    public class JewelComponentInfoPart
    {
        public string Title { get; set; }
        public string Property { get; set; }
        public int JewelComponentID { get; set; }
        public JewelComponentInfoPart(string title,string property,int componentid)
        {
            Title = title;
            Property = property;
            JewelComponentID = componentid;
        }
    }
}