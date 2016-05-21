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
