using foodDelivery.Application.DTOs.Vendor;

namespace foodDelivery.Application.Interface;

public interface IVendorService
{
    VendorSignupResponse Signup(VendorSignupRequest request);
    VendorLocationResponse SetLocation(VendorLocationRequest request);
}