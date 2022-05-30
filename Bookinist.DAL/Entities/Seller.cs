using Bookinist.DAL.Entities.Base;

namespace Bookinist.DAL.Entities
{
    public class Seller : Person
    {
        public override string ToString() => $"Seller {Surname} {Name} {Patronymic}";
    }
}
