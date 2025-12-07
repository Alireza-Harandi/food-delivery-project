using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Domain;

namespace foodDelivery.Application.Interface;

public interface IVendorService
{
    public Task<VendorSignupResponse> SignupAsync(VendorSignupRequest request);
    public Task<RegisterRestaurantResponse> RegisterRestaurantAsync(RegisterRestaurantRequest request);
    public Task<VendorProfileDto> GetProfileAsync();
}