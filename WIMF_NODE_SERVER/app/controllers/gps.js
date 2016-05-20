// ***** Config
var pathServer = "../../server/";
var express 	= require('express');
var config = require(pathServer + 'config');
var jwt    = require('jsonwebtoken');
var moduleRoutes = express.Router();
var mysql = require('mysql');
var connection = mysql.createConnection({
  host : 'localhost',
  user : 'root',
  password : 'password',
  database :  'db_wimf'
})
// ***** Exports
module.exports = moduleRoutes;
//Helpers:
var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");
//var authenticationHelper   = require('../helpers/authentification');


// ***** Methods

//NONE
//http://localhost:8081/user/
//Valide
moduleRoutes.get('/', function(req, res) {
    res.json({ success: true, message: 'NONE Message action', data: req.decoded });
});
