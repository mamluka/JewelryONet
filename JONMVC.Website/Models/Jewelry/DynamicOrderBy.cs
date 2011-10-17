namespace JONMVC.Website.Models.Jewelry
{
    public class DynamicOrderBy
    {
        private readonly string field;
        private readonly string direction;


        public DynamicOrderBy(string field, string direction)
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

        public string SQLString
        {
            get
            {
                return string.Format("{0} {1}", Field, Direction);
            }
        }

        public override bool Equals(object obj)
        {
            var other = (DynamicOrderBy) obj;
            if (other.Field  != this.Field || other.Direction != this.direction)
            {
                return false;
            }

            return true;
        }
    }
}