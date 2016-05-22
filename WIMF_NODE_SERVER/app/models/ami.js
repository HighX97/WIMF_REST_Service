var commonHelper   = require('../helpers/common');
var abstract_db = require("../models/abstract_db");
var validator = require('validator');
var SHAREDDATA   = require('../helpers/data');

var ami = function()
{

  this.table = "Amis";
  this.verify_body_new = function(req)
  {
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    if(! (HelperValidator.isNumeric( req.body.idU_snd )
        && req.body.idU_snd != "" )){
        validationResponse.addError("idU_snd : doit être un int " + req.body.idU_snd);
    }

    if(! (HelperValidator.isNumeric( req.body.idU_rcv)
        && req.body.idU_rcv != "")){
        validationResponse.addError("idU_rcv : doit être un int " + req.body.idU_rcv);
    }
    return validationResponse;
  }

  this.verify_body_list = function(req)
  {
    var validationResponse = commonHelper.getValidationResponse();
    var HelperValidator = commonHelper.validator;
    if(! HelperValidator.isAscii( req.body.tel)
        && req.body.tel != "" ){
        validationResponse.addError("Invalid tel: " + req.body.tel);
    }
    return validationResponse;
  }

}

module.exports = new ami();
