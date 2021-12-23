# Protocolli.IoT

## COMPONENTI

|Cognome | Nome | E-mail|
|:-:|:-:|:-:|
| Scapolan | Davide | davide.scapolan@stud.tecnicosuperiorekennedy.it |
| Fontana | Marika | marika.fontana@stud.tecnicosuperiorekennedy.it |

## DATABASE

Il DB è un DB PostgreSQL fatto girare su Ubuntu montato su WSL2. La connection string andrà modificata nel caso in cui il DB sia montato su un'altra WSL o su un altro sistema operativo

## SCELTA SERVER

Abbiamo deciso di montare il server sul dispositivo edge in modo che il [Client](./Client)  possa fare richieste di tipo osservabili.