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
var ami = require("../models/ami");
// ***** Exports
module.exports = moduleRoutes;
//Helpers:
var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");

moduleRoutes.post('/new', function(req, res)
{
  console.log(req.body.id_snd );
  console.log(req.body.id_rcv );
  console.log(req.body.valeur );
  console.log('---------------------');
  var validationResponse = commonHelper.getValidationResponse();
  if(! message.verify_body_new(req)){
      res.json(validationResponse);
  }
  else {
    var table = 'Message'
    var query = abstract_db.mysql_insert(table,['valeur','tel_snd','tel_rcv'],['"'+req.body.valeur+'"','"'+req.body.tel_snd+'"','"'+req.body.tel_rcv+'"']);
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'New '+table));
    });
  }
});

moduleRoutes.post('/list', function(req, res)
{
  var validationResponse = commonHelper.getValidationResponse();
  var HelperValidator = commonHelper.validator;
  if(! HelperValidator.isAscii( req.body.tel)
      && req.body.tel != "" ){
      validationResponse.addError("Invalid tel: " + req.body.tel);
  }

  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    //abstract_db.connection.connect();
    var query = abstract_db.mysql_select('DISTINCT(Ufind.idU),Ufind.nom,Ufind.tel,A.datetimeCrea,A.etat'
    ,['Amis A','Utilisateur Ufind','Utilisateur Usearch']
    ,'Usearch.tel = "'+req.body.tel+'" and (A.idU_snd = Usearch.idU and Ufind.idu = A.idU_rcv ) or (A.idU_rcv = Usearch.idU and Ufind.idu = A.idU_snd )','A.datetimeCrea ASC');
    console.log(query);
    //abstract_db.connection.connect();
    abstract_db.connection.query(query, function(err, result)
    {
      console.log(new Date());
      console.log(err);
      console.log(result);
      if(err)
      {
        res.json({ success: false, message: 'Amis Utilisateur action failed', data: err });
      }
      else {
        res.json({ success: true, message: 'Amis Utilisateur action suceeded', data: result });
      }
    });
  }
});

//--------------------------------------------------------------
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
