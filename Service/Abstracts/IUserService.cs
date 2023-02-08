using DataAccess.Entities.UserEntities;
using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface IUserService : IBaseService<UserDTO, User, UserDTO>
    {
        UserDTO Login(UserDTO dto);
    }
}