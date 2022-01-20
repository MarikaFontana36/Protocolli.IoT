using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using System.Collections.Generic;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services
{
    public interface IDronesService
    {
        IEnumerable<Drone> GetAllDrones();
        void Delete(int id);
        public Drone GetByDroneId(int Id);
        long GetCount();
        void Insert(Drone model);
        void Update(Drone model);
    }
}
