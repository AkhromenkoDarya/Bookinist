using Bookinist.DAL.Entities.Base;

namespace Bookinist.DAL.Entities
{
    public class Buyer : Person
    {
        //public Deal Deal { get; set; }

        public override string ToString() => $"Buyer {Surname} {Name} {Patronymic}";
    }
}
