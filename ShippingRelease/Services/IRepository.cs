using ShippingRelease.Models;
namespace ShippingRelease.Services;

public interface IRepository
{
    Task<User> Login(string email, string password);
    Task<User> Register(User user);
    Task<ShippingReleaseModel> SearchSkidData(string skidSN);
    Task<ShipSkid> PostShipData(ShipSkid shipSkid);
}
