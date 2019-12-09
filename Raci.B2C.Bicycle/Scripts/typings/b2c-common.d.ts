/// <reference path="jquery/jquery.d.ts" />

interface JqueryBCCCommon extends JQueryStatic {
    ajaxWithVerification(settings: JQueryAjaxSettings): JQueryXHR;
}
//
// "Global" functions
//
declare var setSelectedAddressDescription: any;
declare var findAddress: any;
declare var setKendoDropDownListValue: any;