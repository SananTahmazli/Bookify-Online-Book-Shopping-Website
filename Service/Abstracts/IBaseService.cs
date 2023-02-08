using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface IBaseService<TRequestDTO, TEntity, TResponseDTO>
    {
        IEnumerable<TResponseDTO> GetAll();
        TResponseDTO Get(int id);
        TResponseDTO Create(TRequestDTO dto);
        TResponseDTO Update(TRequestDTO dto);
        void Delete(int id);
    }
}