var express 	= require('express');
var app         = express();
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');
var morgan      = require('morgan');
var mongoose    = require('mongoose');
var fs = require('fs');

var pathAngular2 = "../../frontend/angular2_TS/";

var config = require('./config'); // get our config file

//var IndexController   = require('../app/controllers/index');

var UtilisateurController   = require('../app/controllers/utilisateur');
var MessageController   = require('../app/controllers/message');
var AmiController   = require('../app/controllers/ami');
//var GpsController   = require('../app/controllers/gps');

var port = process.env.PORT || 8585;

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.use(cookieParser());

app.use('/utilisateur', UtilisateurController);
app.use('/ami', AmiController);
app.use('/message', MessageController);
//app.use('/gps', GpsController);

app.use(morgan('dev'));

app.set('view engine', 'ejs');

/*
app.get('/', function(req, res) {
	console.log(pathAngular2+'index.html');
	res.writeHead(200, {'Content-Type': 'text/html'});
	//fs.createReadStream(pathAngular2+'index.html').pipe(res);
	//res.send('Hello! The API is at http://localhost:' + port + '/api');
});
*/

app.listen(port);
var d = new Date();
console.log('WIMF nodejs server started at '+d+' http://localhost:' + port);
