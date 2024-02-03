namespace Talabat.Apis.Dtos
{
    public class CustomerBasketDto
    {
        public CustomerBasketDto(string Id)
        {
            this.Id = Id;
        }
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
