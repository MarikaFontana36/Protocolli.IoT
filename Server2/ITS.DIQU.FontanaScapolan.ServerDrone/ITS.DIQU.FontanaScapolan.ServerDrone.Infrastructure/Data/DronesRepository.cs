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
            //genera la query di get all
            const string query = @"
SELECT 
    id                  as Id,
    velocita            as Speed, 
    posizione           as Position,
    livello_batteria    as BatteryLevel,
    data                as Date,
    id_drone            as IdDrone,
    tempo_utilizzo      as Time
FROM registro";
            //recupera la stringa di connessione
            using var connection = new NpgsqlConnection(_connectionString);
            //restituisce i dati ottenuti dall'esecuzione della query
            return connection.Query<Drone>(query);
        }

        public Drone GetById(int id)
        {
            //genera la query di get by id
            const string query = @"
SELECT 
    id                  as Id,
    velocita            as Speed, 
    posizione           as Position,
    livello_batteria    as BatteryLevel,
    data                as Date,
    id_drone            as IdDrone,
    tempo_utilizzo      as Time
FROM registro
WHERE id = @DroneId";
            //recupera la stringa di connessione
            using var connection = new NpgsqlConnection(_connectionString);
            //restituisce i dati ottenuti dall'esecuzione della query
            return connection.QuerySingleOrDefault<Drone>(query, new { DroneId = id });
        }

        public void Insert(Drone model)
        {
            //genera la query di insert
            const string query = @"
INSERT INTO registro(velocita, posizione, livello_batteria, data, id_drone, tempo_utilizzo)
VALUES (@Speed, @Position, @BatteryLevel, @Date, @IdDrone, @Time)";
            //recupera la stringa di connessione
            using var connection = new NpgsqlConnection(_connectionString);
            //esegue la query
            connection.Execute(query, model);
        }

        public void Update(Drone model)
        {
            //genera la query di update
            const string query = @"
UPDATE registro
SET
    id = @Id,
    velocita = @Speed, 
    posizione = @Position,
    livello_batteria = @BatteryLevel,
    data = @Date,
    id_drone = @IdDrone,
    tempo_utilizzo = @Time
WHERE 
    id = @Id";
            //recupera la stringa di connessione
            using var connection = new NpgsqlConnection(_connectionString);
            //esegue la query
            connection.Execute(query, model);
        }
        public long Count()
        {
            //genera la query di count
            const string query = "SELECT Count(*) FROM registro";
            //recupera la stringa di connessione
            using var connection = new NpgsqlConnection(_connectionString);
            //esegue la query
            return connection.ExecuteScalar<long>(query);
        }

        public void Delete(int id)
        {
            //genera la query di delete
            const string query = @"DELETE FROM registro WHERE id = @id";
            //recupera la stringa di connessione
            using var connection = new NpgsqlConnection(_connectionString);
            //esegue la query
            connection.Execute(query, new { id });
        }

    }
}
