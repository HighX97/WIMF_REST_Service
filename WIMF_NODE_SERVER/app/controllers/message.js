// ***** Config
var pathServer = "../../server/";
var express 	= require('express');
var config = require(pathServer + 'config');
var jwt    = require('jsonwebtoken');
var moduleRoutes = express.Router();
var mysql = require('mysql');
//Helpers:
var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");
var message = require("../models/message");
// ***** Exports
module.exports = moduleRoutes;
//Helpers:
var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");
var table = 'Message'
//var authenticationHelper   = require('../helpers/authentification');


// ***** Methods

//NONE
//http://localhost:8081/user/
//Valide
moduleRoutes.get('/', function(req, res) {
    res.json({ success: true, message: 'NONE Message action', data: req.decoded });
});

moduleRoutes.post('/new', function(req, res)
{
  var validationResponse = message.verify_body_new(req);
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_insert(table,['valeur','tel_snd','tel_rcv'],['"'+req.body.valeur+'"','"'+req.body.tel_snd+'"','"'+req.body.tel_rcv+'"']);
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'New '+table));
    });
  }
});

moduleRoutes.post('/one', function(req, res)
{
  var validationResponse = message.verify_body_one(req);
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_select('*',[table],'idMsg = '+req.body.idMsg,"");
    abstract_db.connection.query(query, function(err, result)
  {
    res.json(commonHelper.result_json(err, result,'New '+table));
  });
}
});

moduleRoutes.post('/update_state', function(req, res)
{
  var validationResponse = message.verify_body_one(req);
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
  var query = abstract_db.mysql_update(table,['etat = 1','datetimeMaj=CURRENT_TIMESTAMP'],'idMsg = '+req.body.idMsg);
  abstract_db.connection.query(query, function(err, result)
  {
    res.json(commonHelper.result_json(err, result,'New '+table));
  });
}
});

moduleRoutes.delete('/delete', function(req, res)
{
  var validationResponse = message.verify_body_one(req)
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_delete(table,'idMsg = "'+req.body.idMsg+'"');
    abstract_db.connection.query(query, function(err, result)
    {
        res.json(commonHelper.result_json(err, result,'Delete '+table));
    });
}
});

moduleRoutes.post('/list', function(req, res)
{
  var validationResponse = message.verify_body_list(req)
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    //abstract_db.connection.connect();

    var query = abstract_db.mysql_select('DISTINCT(M.idMsg),M.valeur,M.etat,M.datetimeCrea , M.tel_snd , M.tel_rcv'
    ,['Message M','Utilisateur Usearch']
    ,'M.tel_snd = "'+req.body.tel+'" or M.tel_rcv = "'+req.body.tel+'"'
    ,'M.datetimeCrea DESC');
    //abstract_db.connection.connect();
    abstract_db.connection.query(query, function(err, result)
    {
      console.log(err);
      console.log(result);
      res.json(commonHelper.result_json(err, result,'Liste message Utilisateur'));
    });
  }
});
