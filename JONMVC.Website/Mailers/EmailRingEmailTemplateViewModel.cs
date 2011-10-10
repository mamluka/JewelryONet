using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Mailers
{
    public class EmailRingEmailTemplateViewModel
    {
        public string Description { get; set; }
        public string ItemNumber { get; set; }
        public string Price { get; set; }
        public string YourName { get; set; }

        public string YourEmail { get; set; }

        public string ID { get; set; }

        public string Message { get; set; }

        public string FriendEmail { get; set; }

        public string FriendName { get; set; }

        public string Icon { get; set; }

        public JewelMediaType MediaSet { get; set; }
    }
}