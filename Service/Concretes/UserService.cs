using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities.UserEntities;
using DTO.User;
using Helper.EncryptionHelper;
using Helper.RoleKeywordsHelper;
using Microsoft.EntityFrameworkCore;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class UserService : BaseService<UserDTO, User, UserDTO>, IUserService
    {
        public UserService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override UserDTO Create(UserDTO dto)
        {
            var username = _dbContext.Users.Where(u => u.Username.ToLower() == dto.Username.ToLower());
            var role = _dbContext.Roles.Where(r => r.Name == RoleKeywords.UserRole).First();
            dto.RoleId = role.Id;

            if (username.Any())
            {
                throw new Exception("Username is already exists!");
            }

            dto.Salt = Encryption.GenerateSalt();
            dto.Hash = Encryption.GenerateHash(dto.Password, dto.Salt);
            return base.Create(dto);
        }

        public UserDTO Login(UserDTO dto)
        {
            var username = _dbContext.Users.Where(u => u.Username.ToLower() == dto.Username.ToLower())
                .Include(u => u.Role);

            if (username.Count() == 1)
            {
                var user = username.FirstOrDefault();
                var hash = Encryption.GenerateHash(dto.Password, user.Salt);

                if (hash == user.Hash)
                {
                    var model = _mapper.Map<User, UserDTO>(username.First());
                    return model;
                }
                else
                {
                    throw new Exception("Password is incorrect!");
                }
            }
            else
            {
                throw new Exception("Username is not found!");
            }
        }
    }
}