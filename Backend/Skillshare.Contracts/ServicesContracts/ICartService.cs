using Stripe.Checkout;
using Skillshare.Contracts.DTOs.CartItem;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface ICartService
    {
        Task<CartForReturn> GetCartsByUser(string userId);

        Task<Session> CheckOut(CheckOutProperties checkOutProperties);

        Task CoursePaymentConfirmation(string userId);
    }
}