﻿using Bookinist.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookinist.DAL.Entities
{
    public class Deal : Entity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public virtual Book Book { get; set; }

        public virtual Seller Seller { get; set; }

        public virtual Buyer Buyer { get; set; }

        public override string ToString() => $"The deal for the book {Book} sale between {Seller} " +
                                             $"and {Buyer} at a price of {Price:C}";
    }
}
