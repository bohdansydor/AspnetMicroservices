using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repository
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasket(string userName);
        public Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        public Task DeleteBasket(string userName);
    }
}
