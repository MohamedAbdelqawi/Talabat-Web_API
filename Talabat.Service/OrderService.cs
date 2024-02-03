using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.Order_spec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOFWork _unitOFWork;

        //private readonly IGenericRepositry<Product> _productsrepo;
        //private readonly IGenericRepositry<DeliveryMethod> _deliverymethodrepo;
        //private readonly IGenericRepositry<Order> _ordersrepo;

        public OrderService(IBasketRepository basketRepository,
            IUnitOFWork unitOFWork
            //IGenericRepositry<Product> productsrepo
            //, IGenericRepositry<DeliveryMethod> deliverymethodrepo, IGenericRepositry<Order> ordersrepo
            )
        {
            _basketRepository = basketRepository;
            _unitOFWork = unitOFWork;
            //_productsrepo = productsrepo;
            //_deliverymethodrepo = deliverymethodrepo;
            //_ordersrepo = ordersrepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId, Address ShippingAddress)
        {

            var basket = await _basketRepository.GetBasketAsync(basketId);
            var orderItems = new List<OrderItem>();
            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var productrepo = _unitOFWork.Repository<Product>();
                    if (productrepo is not null)
                    {
                        var product = await productrepo.GetByIdAsync(item.Id);
                        if (product is not null)
                        {
                            var prodcutitemordered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                            var orderitem = new OrderItem(prodcutitemordered, product.Price, item.Quantity);
                            orderItems.Add(orderitem);
                        }

                    }

                }

            }

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);


            DeliveryMethod deliveryMetod = new DeliveryMethod();
            var deliveryMetodrepo = _unitOFWork.Repository<DeliveryMethod>();
            if (deliveryMetodrepo is not null)
                deliveryMetod = await deliveryMetodrepo.GetByIdAsync(DeliveryMethodId);
            var order = new Order(buyerEmail, ShippingAddress, deliveryMetod, orderItems, subtotal);
            var ordersrepo = _unitOFWork.Repository<Order>();
            if (ordersrepo is not null)
            {
                await ordersrepo.Add(order);

                var reslut = await _unitOFWork.Complete();
                if (reslut > 0)
                {
                    return order;
                }

            }
            return null;
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecfications(buyerEmail);
            var orders = _unitOFWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecfications(buyerEmail, orderId);
            var order = await _unitOFWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (order is null) return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliverymethod = await _unitOFWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliverymethod;
        }


    }
}
