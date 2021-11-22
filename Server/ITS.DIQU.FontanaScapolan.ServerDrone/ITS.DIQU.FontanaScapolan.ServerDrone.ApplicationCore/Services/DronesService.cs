using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Data;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using System.Collections.Generic;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Services
{
    public class DronesService : IDronesService
    {
        private readonly IDronesRepository _dronesRepository;

        public DronesService(IDronesRepository dronesRepository)
        {
            _dronesRepository = dronesRepository;
        }
        public IEnumerable<Drone> GetAllDrones()
        {
            return _dronesRepository.GetAll();
        }

        public Drone GetByDroneId(int Id)
        {
            return _dronesRepository.GetById(Id);
        }

        public void Insert(Drone model)
        {
            _dronesRepository.Insert(model);
        }

        public void Update(Drone model)
        {
            _dronesRepository.Update(model);
        }

        public long GetCount()
        {
            return _dronesRepository.Count();
        }

        public void Delete(int id)
        {
            _dronesRepository.Delete(id);
        }
    }
}
