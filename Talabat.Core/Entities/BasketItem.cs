﻿namespace Talabat.Core.Entities
{
    public class BasketItem : BaseEntity
    {

        public string Name { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
