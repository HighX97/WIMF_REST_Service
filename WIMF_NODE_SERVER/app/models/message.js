var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");
var validator = require('validator');
var SHAREDDATA   = require('../helpers/data');

var message = function()
{

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
    return validationResponse.success;
  }

  this.verify_body_one = function(req)
  {
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    if(! HelperValidator.isAscii( req.body.tel)
        && req.body.tel != "" ){
        validationResponse.addError("Invalid tel: " + req.body.tel);
    }
    return validationResponse.success;
  }

  this.verify_body_connect = function(req)
  {
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    if(! HelperValidator.isAscii( req.body.tel)
        && req.body.tel != "" ){
        validationResponse.addError("Invalid tel: " + req.body.tel);
    }
    if(! (HelperValidator.isAlphanumeric( req.body.password)
        && HelperValidator.isLength(req.body.password, {min: 5, max: 10}) ) ){
        validationResponse.addError("Le password doit être une chaine de characters Alphanumerique entre (5 - 10) : " + req.body.password);
    }
    return validationResponse.success;
  }
}

module.exports = new message();
