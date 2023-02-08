using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class BaseService<TRequestDTO, TEntity, TResponseDTO>
        : IBaseService<TRequestDTO, TEntity, TResponseDTO> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IMapper _mapper;

        public BaseService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TResponseDTO> GetAll()
        {
            var entityList = _dbSet.ToList();
            var responseDTO = _mapper.Map<IEnumerable<TResponseDTO>>(entityList);
            return responseDTO;
        }

        public virtual TResponseDTO Get(int id)
        {
            var entity = _dbSet.Find(id);
            var responseDTO = _mapper.Map<TEntity, TResponseDTO>(entity);
            return responseDTO;
        }

        public virtual TResponseDTO Create(TRequestDTO dto)
        {
            var entity = _mapper.Map<TRequestDTO, TEntity>(dto);
            entity.CreatedTime = DateTime.Now;
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            var responseDTO = _mapper.Map<TEntity, TResponseDTO>(entity);
            return responseDTO;
        }

        public virtual TResponseDTO Update(TRequestDTO dto)
        {
            var entity = _mapper.Map<TRequestDTO, TEntity>(dto);
            entity.UpdatedTime = DateTime.Now;
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            var responseDTO = _mapper.Map<TEntity, TResponseDTO>(entity);
            return responseDTO;
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}