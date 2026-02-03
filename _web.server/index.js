var http = require("http");
var fs = require("fs");
const url = require("url");

const port = 4250;
const delayTime = 10;
const jsonHeaders = { "Content-Type": "application/json" };
const csvHeaders = { "Content-Type": "text/csv" };

function options(req, res) {
  res.writeHead(204);
  return res.end();
}

function get(req, res) {
  let fileName = `data${url.parse(req.url, true).pathname}.json`;
  let returnHeader = jsonHeaders;

  // return csv file if it is requested
  if (req.headers["content-type"] == "text/csv") {
    fileName = `data${url.parse(req.url, true).pathname}.csv`;
    returnHeader = csvHeaders;
  }

  fs.readFile(fileName, (err, data) => {
    if (err) {
      res.writeHead(404, jsonHeaders);
      return res.end("404 Not Found");
    }
    res.writeHead(200, returnHeader);
    let dataString = data.toString();
    while (dataString.includes("RANDOM")) {
      dataString = dataString.replace("RANDOM", Math.floor(Math.random() * 100));
    }
    res.write(dataString);
    return res.end();
  });
}

function post(req, res) {
  fs.readFile(`data${url.parse(req.url, true).pathname}.post.json`, (err, data) => {
    res.writeHead(200, jsonHeaders);
    if (!err) {
      // if loggin token is requested, then send the data as base64
      if (req.url == "/login") {
        let jwt = JSON.parse(data);
        data = JSON.stringify({
          identityToken:
            Buffer.from(JSON.stringify(jwt.header)).toString("base64") + "." +
            Buffer.from(JSON.stringify(jwt.payload)).toString("base64") + "." +
            jwt.signature
        });
      }
      res.write(data); // return the json file if found
    }
    return res.end();
  });
}

function put_delete(req, res) {
  res.writeHead(200, jsonHeaders);
  return res.end();
}

function delay(time) {
  return new Promise((resolve) => setTimeout(resolve, time));
}

http
  .createServer(function (req, res) {
    // headers for cors
    res.setHeader("Access-Control-Allow-Origin", "*");
    res.setHeader("Access-Control-Request-Method", "*");
    res.setHeader("Access-Control-Allow-Methods", "OPTIONS, GET, POST");
    res.setHeader("Access-Control-Allow-Headers", "*");

    console.log(`${req.url} [${req.method}]`);
    switch (req.method.toUpperCase()) {
      case "OPTIONS":
        return options(req, res);
      case "GET":
        // modify the delay value to mimic real server as needed
        return delay(delayTime).then(() => get(req, res));
      case "POST":
        return delay(delayTime).then(() => post(req, res));
      // return post(req, res);
      default:
        return delay(delayTime).then(() => put_delete(req, res));
      // return put_delete(req, res);
    }
  })
  .listen(port);

console.log(`** server is listening on http://localhost:${port} **`);
