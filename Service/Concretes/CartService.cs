using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities.CartEntities;
using DTO.Cart;
using Microsoft.EntityFrameworkCore;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class CartService : BaseService<CartDTO, Cart, CartDTO>, ICartService
    {
        public CartService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public void AddToCart(CartDTO dto)
        {
            var user = _dbContext.Users.Find(dto.UserId);

            if (user == null)
            {
                throw new Exception("User is not found!");
            }

            var entity = _mapper.Map<Cart>(dto);
            user.Cart.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteFromCart(int cartId)
        {
            var entity = _dbContext.Carts.Find(cartId);

            if (entity == null)
            {
                return;
            }

            _dbContext.Carts.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<CartDTO> GetCart(int userId)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(x => x.Cart)
                .ThenInclude(x => x.Book)
                .First();

            if (user == null)
            {
                throw new Exception("User is not found!");
            }

            var res = _mapper.Map<IEnumerable<CartDTO>>(user.Cart);
            return res;
        }

        public void Buy(int cartId)
        {
            var cart = _dbContext.Carts.Find(cartId);
            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();
        }

        public int GetCountOfBooksInMyCart(int userId)
        {
            return _dbContext.Carts.Where(c => c.UserId == userId).Sum(c => c.Count);
        }
    }
}