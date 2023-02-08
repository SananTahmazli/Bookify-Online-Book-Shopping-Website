using AutoMapper;
using DataAccess.Entities.BookEntities;
using DataAccess.Entities.CartEntities;
using DataAccess.Entities.UserEntities;
using DTO.Book;
using DTO.Cart;
using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Config.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<BookDTO, Book>();
            CreateMap<Book, BookDTO>();

            CreateMap<CartDTO, Cart>();
            CreateMap<Cart, CartDTO>();
        }
    }
}