using DataAccess.Entities.CartEntities;
using DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface ICartService : IBaseService<CartDTO, Cart, CartDTO>
    {
        void AddToCart(CartDTO dto);
        void DeleteFromCart(int cartId);
        IEnumerable<CartDTO> GetCart(int userId);
        void Buy(int cartId);
        int GetCountOfBooksInMyCart(int userId);
    }
}