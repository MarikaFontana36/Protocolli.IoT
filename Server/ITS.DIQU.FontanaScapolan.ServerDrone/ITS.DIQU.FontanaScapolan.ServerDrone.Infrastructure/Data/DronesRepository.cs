using Dapper;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.Infrastructure.Data
{
    public class DronesRepository : IDronesRepository
    {
        private readonly string _connectionString;

        public DronesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Drone");
        }
        public IEnumerable<Drone> GetAll()
        {
            const string query = @"
SELECT 
    id                  as Id,
    velocita            as Speed, 
    posizione           as Position,
    livello_batteria    as BatteryLevel,
    data                as Date
FROM registro";

            using var connection = new NpgsqlConnection(_connectionString);
            return connection.Query<Drone>(query);
        }

        public Drone GetById(int id)
        {
            const string query = @"
SELECT 
    id                  as Id,
    velocita            as Speed, 
    posizione           as Position,
    livello_batteria    as BatteryLevel,
    data                as Date
FROM registro
WHERE id = @DroneId";

            using var connection = new NpgsqlConnection(_connectionString);
            return connection.QuerySingleOrDefault<Drone>(query, new { DroneId = id });
        }

        public void Insert(Drone model)
        {
            const string query = @"
INSERT INTO registro(velocita, posizione, livello_batteria, data)
VALUES (@Speed, @Position, @BatteryLevel, @Date)";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Execute(query, model);
        }

        public void Update(Drone model)
        {
            const string query = @"
UPDATE registro
SET
    id = @Id,
    velocita = @Speed, 
    posizione = @Position,
    livello_batteria = @BatteryLevel,
    data = @Date
WHERE 
    id = @Id";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Execute(query, model);
        }
        public long Count()
        {
            const string query = "SELECT Count(*) FROM registro";

            using var connection = new NpgsqlConnection(_connectionString);
            return connection.ExecuteScalar<long>(query);
        }

        public void Delete(int id)
        {
            const string query = @"DELETE FROM registro WHERE id = @id";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Execute(query, new { id });
        }

    }
}
