using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Domain;

namespace foodDelivery.Application.Interface;

public interface IVendorService
{
    public VendorSignupResponse Signup(VendorSignupRequest request);
    public RegisterRestaurantResponse RegisterRestaurant(RegisterRestaurantRequest request);
    public Token CheckAccess();
}