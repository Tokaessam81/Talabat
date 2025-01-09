namespace Talabat.Core.Entities
{
    public class BasketItem
    {
        //Id//مخدتوش من الbaseEntity
        //علشان ال baseentity تبع الحاجات الاساسيه اللي في الداتا بيز دي 
        //انما انا هنا هتعمل مع داتا بيز تانيه
        public int Id { get; set; }

        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }

    }
}