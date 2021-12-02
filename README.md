# Protocolli.IoT

COMPONENTI:

Davide Scapolan = davide.scapolan@stud.tecnicosuperiorekennedy.it
Marika Fontana = marika.fontana@stud.tecnicosuperiorekennedy.it

SERVER:Per avviare correttamente la soluzione del Server è necessario aggiungere una stringa di connessione agli UserSecret dell'API così composti:

{ "ConnectionStrings": { "Drone": "Server=127.0.0.1;Port=5432;Database=Nome DB;User Id=Nome utente;Password=*Password utente;" } }

N.B. il DB è un DB PostgreSQL fatto girare su Ubuntu montato su WSL2. La connection string andrà modificata nel caso in cui il DB sia montato su un'altra WSL o su un altro sistema operativo

topic:
protocolliIot/drone1/stato

payload:
{
  "Id": 0,
  "Date": "2021-12-02T15:02:00.364Z",
  "Position": "string",
  "Speed": 0,
  "BatteryLevel": 0,
  "IdDrone": 0,
  "Time": 0
}