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
            //riceve la richiesta di get all e la passa al repository dedicato restituendo i dati forniti da tale repository
            return _dronesRepository.GetAll();
        }

        public Drone GetByDroneId(int Id)
        {
            //riceve la richiesta di get by id e la passa al repository dedicato restituendo i dati forniti da tale repository
            return _dronesRepository.GetById(Id);
        }

        public void Insert(Drone model)
        {
            //riceve la richiesta di insert e la passa al repository dedicato
            _dronesRepository.Insert(model);
        }

        public void Update(Drone model)
        {
            //riceve la richiesta di update e la passa al repository dedicato
            _dronesRepository.Update(model);
        }

        public long GetCount()
        {
            //riceve la richiesta di count e la passa al repository dedicato
            return _dronesRepository.Count();
        }

        public void Delete(int id)
        {
            //riceve la richiesta di delete e la passa al repository dedicato
            _dronesRepository.Delete(id);
        }
    }
}
