var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");
var validator = require('validator');
var SHAREDDATA   = require('../helpers/data');

var message = function()
{

  this.table = "Message";
  this.verify_body_new = function(req)
  {
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    console.log(req.body.tel_snd );
    console.log(req.body.tel_rcv );
    console.log(req.body.valeur );
    if(! HelperValidator.isAlphanumeric( req.body.tel_snd )
        && req.body.firstName != "" ){
        validationResponse.addError("tel_snd : doit être une chaine de characters Alphanumerique : " + req.body.tel_snd);
    }

    if(! (HelperValidator.isAlphanumeric( req.body.tel_rcv)) ){
        validationResponse.addError("tel_rcv : doit être une chaine de characters Alphanumerique : " + req.body.tel_rcv);
    }
    if(! req.body.valeur != "" ){
        validationResponse.addError("valeur: ne doit pas être la chaine vide" + req.body.valeur);
    }
    return validationResponse;
  }

  this.verify_body_one = function(req)
  {
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    if(! HelperValidator.isNumeric( req.body.idMsg)
        && req.body.idMsg != "" ){
        validationResponse.addError("Invalid idMsg: " + req.body.idMsg);
    }
    return validationResponse;
  }

  this.verify_body_list = function(req)
  {
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    if(! HelperValidator.isAlphanumeric( req.body.tel)
        && req.body.idMsg != "" ){
        validationResponse.addError("Invalid tel: " + req.body.tel);
    }
    return validationResponse;
  }

}

module.exports = new message();
