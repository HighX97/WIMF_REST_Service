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
    res.json({ success: true, message: 'NONE Utilisateur action', data: req.decoded });
});

moduleRoutes.post('/new', function(req, res)
{
  var validationResponse = commonHelper.getValidationResponse();
  var HelperValidator = commonHelper.validator;
  if(! HelperValidator.isAlphanumeric( req.body.nom )
      && req.body.firstName != "" ){
      validationResponse.addError("Le nom doit être une chaine de characters Alphanumerique : " + req.body.nom);
  }

  if(! (HelperValidator.isAlphanumeric( req.body.password)
      && HelperValidator.isLength(req.body.password, {min: 5, max: 10}) ) ){
      validationResponse.addError("Le password doit être une chaine de characters Alphanumerique entre (5 - 10) : " + req.body.password);
  }
  if(! HelperValidator.isAscii( req.body.tel)
      && req.body.tel != "" ){
      validationResponse.addError("Invalid tel: " + req.body.tel);
  }

  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    var abstract_db = require("../models/abstract_db");
    var query = abstract_db.insert('Utilisateur',['nom','tel','password'],['"'+req.body.nom+'"','"'+req.body.tel+'"','"'+req.body.password+'"']);
    console.log(query);
    //connection.connect();
    connection.query(query, function(err, result)
    {
      console.log(result);
      res.json({ success: true, message: 'New Utilisateur action suceeded', data: result });
    });
    //connection.end();
}
});

function insert(table, columns, values)
{
    var query = "INSERT INTO " + table + " ";
    var i = 1;
    for (x in columns)
    {
      if (i == 1)
      {
        query += "(";
      }
      query += columns[x]+" ";
      if (i < columns.length)
      {
        query += ",";
      }
      if (i == columns.length)
      {
        query += ") ";
      }
      i++;
    }
    query += "VALUES ";
    i = 1;
    for (x in values)
    {
      if (i == 1)
      {
        query += "(";
      }
      query += values[x]+" ";
      if (i < values.length)
      {
        query += ",";
      }
      if (i == values.length)
      {
        query += ") ";
      }
      i++;
    }
    return query;
}


moduleRoutes.post('/one', function(req, res)
{
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    /*
    if(! HelperValidator.isNumeric(req.body.idu)
        && req.body.firstName != "" ){
        validationResponse.addError("Idu doit être un entier: " + req.body.idU);
    }
    */
    //connection.connect();
    connection.query('select * from Utilisateur where idU='+req.body.idU, function(err, result)
    {
      console.log(result);
      res.json({ success: true, message: 'Utilisateur one action suceeded', data: result });
    });
    //connection.end();

});

moduleRoutes.get('/list', function(req, res)
{
    //connection.connect();
    connection.query('select * from Utilisateur', function(err, result)
    {
      console.log(result);
      res.json({ success: true, message: 'Utilisateur list action suceeded', data: result });
    });
    //connection.end();

});
