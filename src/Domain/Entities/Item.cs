using System;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Item(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name ne može biti prazan.");
            if (price <= 0)
                throw new DomainException("Price mora biti veća od 0.");

            Name = name;
            Price = price;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new DomainException("Price mora biti veća od 0.");

            Price = newPrice;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Name ne može biti prazan.");

            Name = newName;
        }
    }
}