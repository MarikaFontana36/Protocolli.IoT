using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using System.Collections.Generic;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Data
{
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(TPrimaryKey id);
        void Insert(TEntity model);
        void Update(TEntity model);
        void Delete(TPrimaryKey id);
        long Count();
    }
}
