// ***** Config
var pathServer = "../../server/";
var express 	= require('express');
var config = require(pathServer + 'config');
var jwt    = require('jsonwebtoken');
var moduleRoutes = express.Router();
var mysql = require('mysql');
var connection = mysql.createConnection({
  host : 'localhost',
  user : 'jimmy',
  password : 'ikbal',
  database :  'db_wimf'
})
// ***** Exports
module.exports = moduleRoutes;
//Helpers:
var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");


// ***** Methods

//NONE
//http://localhost:8081/user/
//Valide
moduleRoutes.get('/', function(req, res) {
    res.json({ success: true, message: 'NONE Utilisateur action', data: req.decoded });
});

function verify_body_new(req)
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
  return validationResponse.success;
}

moduleRoutes.post('/new', function(req, res)
{
  if(! verify_body_new(req)){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_insert('Utilisateur',['nom','tel','password'],['"'+req.body.nom+'"','"'+req.body.tel+'"','"'+req.body.password+'"']);
    console.log(query);
    //connection.connect();
    connection.query(query, function(err, result)
    {
      console.log(err);
      console.log(result);
      if(err)
      {
        res.json({ success: false, message: 'New Utilisateur action failed', data: err });
      }
      else {
        res.json({ success: true, message: 'New Utilisateur action suceeded', data: result });
      }


    });
    //connection.end();
}
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
    var query = abstract_db.mysql_insert('Utilisateur',['nom','tel','password'],['"'+req.body.nom+'"','"'+req.body.tel+'"','"'+req.body.password+'"']);
    console.log(query);
    //connection.connect();
    connection.query(query, function(err, result)
    {
      console.log(err);
      console.log(result);
      if(err)
      {
        res.json({ success: false, message: 'New Utilisateur action failed', data: err });
      }
      else {
        res.json({ success: true, message: 'New Utilisateur action suceeded', data: result });
      }


    });
    //connection.end();
}
});

moduleRoutes.post('/update', function(req, res)
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
    var query = abstract_db.mysql_update("Utilisateur",['nom = "'+req.body.nom+'"','password = "'+req.body.password+'"'],'tel = "'+req.body.tel+'"');
    console.log(query);
    //connection.connect();
    connection.query(query, function(err, result)
    {
      console.log(err);
      console.log(result);
      if(err)
      {
        res.json({ success: false, message: 'Update Utilisateur action failed', data: err });
      }
      else {
        res.json({ success: true, message: 'Update Utilisateur action suceeded', data: result });
      }


    });
    //connection.end();
}
});

moduleRoutes.delete('/delete', function(req, res)
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
    var query = abstract_db.mysql_delete("Utilisateur",'tel = "'+req.body.tel+'"');
    console.log(query);
    //connection.connect();
    connection.query(query, function(err, result)
    {
      console.log(err);
      console.log(result);
      if(err)
      {
        res.json({ success: false, message: 'Delete Utilisateur action failed', data: err });
      }
      else {
        res.json({ success: true, message: 'Delete Utilisateur action suceeded', data: result });
      }
    });
    //connection.end();
}
});




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
    var query = abstract_db.mysql_select("*",["Utilisateur"],'where idU='+req.body.idU,"");
    connection.query(query, function(err, result)
    {
      console.log(result);
      res.json({ success: true, message: 'Utilisateur one action suceeded', data: result });
    });

    //connection.end();

});

moduleRoutes.get('/list', function(req, res)
{
    var query = abstract_db.mysql_select("*",["Utilisateur"],"","");
    console.log(query);
    connection.query(query, function(err, result)
    {
      console.log(err);
      console.log(result);
      if(err)
      {
        res.json({ success: false, message: 'List Utilisateur action failed', data: err });
      }
      else {
        res.json({ success: true, message: 'List Utilisateur action suceeded', data: result });
      }
    });
});
