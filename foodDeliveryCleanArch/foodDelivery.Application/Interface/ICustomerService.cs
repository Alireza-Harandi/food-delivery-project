using foodDelivery.Application.DTOs.Customer;

namespace foodDelivery.Application.Interface;

public interface ICustomerService
{
    public CustomerSignupResponse Signup(CustomerSignupRequest request);
    public AddToOrderResponse AddToOrder(AddToOrderRequest request);
    public void SetOrderQuantity(SetOrderQuantityDto request);
    public CustomerOrderDto GetOrders(Guid orderId);
    public FinalizeOrderResponse FinalizeOrder(FinalizeOrderRequest request);
    public void ReportRestaurant(ReportRestaurantDto request);
    public void DeleteOrder(Guid orderId);
}
