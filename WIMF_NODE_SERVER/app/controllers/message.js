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

    var query = abstract_db.mysql_select('DISTINCT(M.idMsg),M.valeur,M.etat,M.datetimeCrea , M.idU_snd , M.idU_rcv'
    ,['Message M','Utilisateur Usearch']
    ,'Usearch.tel = "'+req.body.tel+'" and (M.idU_snd = Usearch.idU or M.idU_rcv = Usearch.idU)'
    ,'M.datetimeCrea DESC');
    console.log(query);
    //abstract_db.connection.connect();
    abstract_db.connection.query(query, function(err, result)
    {
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
