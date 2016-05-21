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
  console.log(req.body.tel_snd );
  console.log(req.body.tel_rcv );
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

    var query = abstract_db.mysql_select('DISTINCT(M.idMsg),M.valeur,M.etat,M.datetimeCrea , M.tel_snd , M.tel_rcv'
    ,['Message M','Utilisateur Usearch']
    ,'M.tel_snd = "'+req.body.tel+'" or M.tel_rcv = "'+req.body.tel+'"'
    ,'M.datetimeCrea DESC');
    console.log(query);
    //abstract_db.connection.connect();
    abstract_db.connection.query(query, function(err, result)
    {
      console.log(err);
      console.log(result);
      if(err)
      {
        res.json({ success: false, message: 'Liste message Utilisateur action failed', data: err });
      }
      else {
        res.json({ success: true, message: 'Liste message Utilisateur action suceeded', data: result });
      }
    });
  }
});
