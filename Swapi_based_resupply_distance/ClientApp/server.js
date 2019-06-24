'use strict';

const express = require('express');

// Constants
const PORT = 80;
const HOST = '10.1.0.4';

// App
const app = express();
app.get('/', (req, res) => {
  res.send('SwapiChallenge\n');
});

app.listen(PORT, HOST);
console.log(`Running on http://${HOST}:${PORT}`);