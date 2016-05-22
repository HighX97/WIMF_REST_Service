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

moduleRoutes.get('/', function(req, res) {
    res.json({ success: true, message: 'NONE '+ami.table+' action', data: req.decoded });
});

moduleRoutes.post('/list', function(req, res)
{
  var validationResponse = ami.verify_body_list(req);
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    //abstract_db.connection.connect();
    var query = abstract_db.mysql_select('DISTINCT(Ufind.idU),Ufind.nom,Ufind.tel,A.datetimeCrea,A.etat'
    ,[ami.table+' A','Utilisateur Ufind','Utilisateur Usearch']
    ,'Usearch.tel = "'+req.body.tel+'" and ((A.idU_snd = Usearch.idU and Ufind.idu = A.idU_rcv ) or (A.idU_rcv = Usearch.idU and Ufind.idu = A.idU_snd ) and  Usearch.idU != Ufind.idU)','A.datetimeCrea ASC');
    console.log(query);
    //abstract_db.connection.connect();
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'Amis Utilisateur '));
    });
  }
});

moduleRoutes.post('/new', function(req, res)
{
  var validationResponse = ami.verify_body_new(req);
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_insert(ami.table,['idU_snd','idU_rcv'],['"'+req.body.idU_snd+'"','"'+req.body.idU_rcv+'"']);
    abstract_db.connection.query(query, function(err, result)
    {
      res.json(commonHelper.result_json(err, result,'New '+ami.table));
    });
  }
});




moduleRoutes.post('/update_state', function(req, res)
{
  var validationResponse = ami.verify_body_new(req);
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
  var query = abstract_db.mysql_update(ami.table,['etat = 1','datetimeMaj=CURRENT_TIMESTAMP'],'(idU_snd = '+req.body.idU_snd+' AND '+'idU_rcv = '+req.body.idU_rcv+') OR ('+'idU_snd = '+req.body.idU_rcv+' AND '+'idU_rcv = '+req.body.idU_snd+')');
  abstract_db.connection.query(query, function(err, result)
  {
    res.json(commonHelper.result_json(err, result,'Update_state '+ami.table));
  });
}
});

moduleRoutes.delete('/delete', function(req, res)
{
  var validationResponse = ami.verify_body_new(req)
  if(! validationResponse.success){
      res.json(validationResponse);
  }
  else {
    var query = abstract_db.mysql_delete(ami.table,'(idU_snd = '+req.body.idU_snd+' AND '+'idU_rcv = '+req.body.idU_rcv+') OR ('+'idU_snd = '+req.body.idU_rcv+' AND '+'idU_rcv = '+req.body.idU_snd+')');
    abstract_db.connection.query(query, function(err, result)
    {
        res.json(commonHelper.result_json(err, result,'Delete '+ami.table));
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
