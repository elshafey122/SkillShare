using Skillshare.Contracts.DTOs.CartItem;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface ICartItemService
    {
        Task<bool> AddCartItem(CartItemForCreate cartItemForCreate);
        Task<bool> RemoveCartItem(int CartItemId,string userId);
    }
}