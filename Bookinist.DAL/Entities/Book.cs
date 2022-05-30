using Bookinist.DAL.Entities.Base;

namespace Bookinist.DAL.Entities
{
    public class Book : NamedEntity
    {
        //public Deal Deal { get; set; }

        public Category Category { get; set; }

        public override string ToString() => $"Book {Name}";
    }
}
