# Protocolli.IoT

## COMPONENTI

|Cognome | Nome | E-mail|
|:-:|:-:|:-:|
| Scapolan | Davide | davide.scapolan@stud.tecnicosuperiorekennedy.it |
| Fontana | Marika | marika.fontana@stud.tecnicosuperiorekennedy.it |

## SERVER

Per avviare correttamente la soluzione del Server è necessario aggiungere una stringa di connessione agli UserSecret dell'API così composti:

{ "ConnectionStrings": { "Drone": "Server=127.0.0.1;Port=5432;Database=Nome DB;User Id=Nome utente;Password=Password utente;" } }

## N.B.

Il DB è un DB PostgreSQL fatto girare su Ubuntu montato su WSL2. La connection string andrà modificata nel caso in cui il DB sia montato su un'altra WSL o su un altro sistema operativo

## UTILIZZO AMQP
### SERVER & DRONE
Abbiamo deciso di utilizzare AMQP come coda del Drone per gestire l'assenza di connessione in quanto ci sembra il metodo più efficace per risolvere il problema della connessione eventuale.

###SERVER2 & DRONE2

Nel caso in cui decidessimo di utilizzare AMQP per la comunicazione tra Drone e Server, creeremmo due code diverse: una per l'invio della telemetria ("sensor") e l'altra per l'invio dei comandi ("command"). Il Drone avrebbe un metodo BasicPublish sulla coda "sensor" e un metodo BasicConsume sulla cosa "command", mentre il Server avrebbe un programma che consuma i dati nella coda "sensor" e una Windows Form che invia i comandi sulla coda "command".

## TOPICS

	protocolliIot/drone1/stato
		CleanSession = false
		QoS = 0
	protocolliIot/drone1/comando/accensione
		QoS = 1
		RetainFlag = true
	protocolliIot/drone1/comando/led
		QoS = 1
		RetainFlag = true
	protocolliIot/drone1/comando/base
		QoS = 1
		RetainFlag = true

## PAYLOAD

###	protocolliIot/drone1/stato
	{
	  "Id": 0,
	  "Date": "2021-12-02T15:02:00.364Z",
	  "Position": "string",
	  "Speed": 0,
	  "BatteryLevel": 0,
	  "IdDrone": 0,
	  "Time": 0
	}

###	protocolliIot/drone1/comando/accensione, comando/led:
	0/1

###	protocolliIot/drone1/comando/base:
	1
	
## SICUREZZA

Per quanto riguarda la sicurezza, considerando che i droni si connettono via SIM, il livello Network non è sicuramente praticabile; a livello di Transport è possibile criptare la comunicazione e ottenere una maggiore protezione.

## AUTENTICAZIONI

Ogni client ha delle credenziali proprie che vengono utilizzate per far riconoscere il client dal broker e conferire autorizzazioni specifiche ad ognuno.

## AUTORIZZAZIONI

###	Lettura
I droni hanno accesso a protocolliIot/drone(numero drone) mentre l'applicazione server ha accesso a protocolliIot/+/stato.

###	Scrittura
I droni hanno accesso a protocolliIot/drone(numero drone)/stato mentre l'applicazione server per i comandi ha accesso a protocolliIot/+/comando/# ed è accessibile solo a determinati utenti.
