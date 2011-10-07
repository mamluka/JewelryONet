namespace JONMVC.Website.Models.Jewelry
{
    public class JewelryDynamicOrderBy
    {
        private readonly string field;
        private readonly string direction;


        public JewelryDynamicOrderBy(string field, string direction)
        {
            this.field = field;
            this.direction = direction;
        
        }


        public string Direction
        {
            get { return direction; }
            
        }

        public string Field
        {
            get { return field; }

        }

        public override bool Equals(object obj)
        {
            var other = (JewelryDynamicOrderBy) obj;
            if (other.Field  != this.Field || other.Direction != this.direction)
            {
                return false;
            }

            return true;
        }
    }
}