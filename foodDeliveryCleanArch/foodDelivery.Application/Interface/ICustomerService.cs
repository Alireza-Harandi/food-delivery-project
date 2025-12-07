using foodDelivery.Application.DTOs.Customer;

namespace foodDelivery.Application.Interface;

public interface ICustomerService
{
    public Task<CustomerSignupResponse> SignupAsync(CustomerSignupRequest request);
    public Task<AddToOrderResponse> AddToOrderAsync(AddToOrderRequest request);
    public Task SetOrderQuantityAsync(SetOrderQuantityDto request);
    public Task<CustomerOrderDto> GetOrdersAsync(Guid orderId);
    public Task<FinalizeOrderResponse> FinalizeOrderAsync(FinalizeOrderRequest request);
    public Task ReportRestaurantAsync(ReportRestaurantDto request);
    public Task DeleteOrderAsync(Guid orderId);
    public Task SubmitRatingAsync(SubmitRatingDto request);
    public Task<CustomerProfileDto> GetProfileAsync();
}