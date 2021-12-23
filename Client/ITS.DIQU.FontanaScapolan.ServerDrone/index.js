const coap = require('coap') // or coap
//const { Pool, Client } = require('pg')

const req = coap.request('coap://localhost/hello',{
    observe: true
})
//const connectionString = 'postgresql://davide:davide@127.0.0.1:5432/db_drone'

async function streamToString(stream) {
    // lets have a ReadableStream as a stream variable
    const chunks = [];

    for await (const chunk of stream) {
        chunks.push(Buffer.from(chunk));
    }

    return Buffer.concat(chunks).toString("utf-8");
}

reqasync.on ('response', (res) => {
    res.pipe(process.stdout)
    string = streamToString(res)
    console.log(string)
    /*const client = new Client({
        connectionString,
      })
    client.connect()
    client.query('SELECT NOW()', (err, res) => {
        console.log(err, res)
        client.end()
      })*/
})

req.end()
