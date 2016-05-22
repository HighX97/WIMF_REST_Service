// ***** Config
var pathServer = "../../server/";
var express 	= require('express');
var config = require(pathServer + 'config');
var jwt    = require('jsonwebtoken');
var moduleRoutes = express.Router();
var mysql = require('mysql');
// ***** Exports
module.exports = moduleRoutes;
//Helpers:
var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");
var utilisateur = require("../models/utilisateur");


// ***** Methods

//NONE
moduleRoutes.get('/', function(req, res)
{
    res.json(commonHelper.result_json(err, result,'NONE Utilisateur '));
});

moduleRoutes.post('/new', function(req, res)
{
  if(! utilisateur.verify_body_new(req)){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_insert('Utilisateur',['nom','tel','password'],['"'+req.body.nom+'"','"'+req.body.tel+'"','"'+req.body.password+'"']);
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'New Utilisateur'));
    });
}
});

moduleRoutes.post('/update', function(req, res)
{
  var validationResponse = commonHelper.getValidationResponse();
  if(! utilisateur.verify_body_new(req)){
      res.json(validationResponse);
  }
  else {
    var abstract_db = require("../models/abstract_db");
    var query = abstract_db.mysql_update("Utilisateur",['nom = "'+req.body.nom+'"','password = "'+req.body.password+'"'],'tel = "'+req.body.tel+'"');
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'Update Utilisateur'));
    });
}
});

moduleRoutes.delete('/delete', function(req, res)
{
  if(! utilisateur.verify_body_one(req)){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_delete("Utilisateur",'tel = "'+req.body.tel+'"');
    abstract_db.connection.query(query, function(err, result)
    {
        res.json(commonHelper.result_json(err, result,'Update Utilisateur'));
    });
}
});


moduleRoutes.post('/one', function(req, res)
{
  if(! utilisateur.verify_body_one(req)){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_select("nom, tel, gps_lat, gps_long, datetimeMaj",["Utilisateur"],'tel="'+req.body.tel+'"',"");
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'Utilisateur one action'));
    });
  }
});


moduleRoutes.post('/connect', function(req, res)
{
  if(! utilisateur.verify_body_connect(req)){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_select("*",["Utilisateur"],'tel="'+req.body.tel+'" AND password="'+req.body.password+'"',"");
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'Utilisateur connect action'));
    });
  }
});

moduleRoutes.get('/list', function(req, res)
{
    var query = abstract_db.mysql_select("*",["Utilisateur"],"","");
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'List Utilisateur'));
    });
});
