namespace CartService.Services.Services.Product
{
    public class Product
    {
        public int Id { get; }

        public string Name { get; }

        public decimal Cost { get; }

        public bool ForBonusPoints { get; }

        public Product(int id, string name, decimal cost, bool forBonusPoints)
        {
            Id = id;
            Name = name;
            Cost = cost;
            ForBonusPoints = forBonusPoints;
        }
    }
}