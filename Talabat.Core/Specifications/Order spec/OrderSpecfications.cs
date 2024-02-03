using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_spec
{
    public class OrderSpecfications : BaseSpecification<Order>
    {
        public OrderSpecfications(string email) : base(o => o.BuyerEmail == email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);


            AddOrderByDescending(o => o.OrderDate);
        }


        public OrderSpecfications(string email, int id) : base(o => o.BuyerEmail == email && o.Id == id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);



        }
    }
}
