declare var pageController: any;
declare module B2C {
    class Util {
        static IntegerRegEx: RegExp;
        static isNullOrEmpty(str: any): boolean;
        static isEmpty(testValue: any): boolean;
        static isInteger(testValue: any): boolean;
        static setCoverStartDate(controlId: any, valueToSet: any): void;
        static createErrorMessage(text: string, errorClass?: string): JQuery;
        static preventEventChaining(element: JQuery, lockName: string, performFunction: () => void): void;
        static setKendoNumericValue(elementSelector: any, value: any): void;
        static getViewPortWidth(): number;
        static getViewPortHeight(): number;
        static isSmallScreen(): boolean;
        static formatNameAsId(name: string): string;
        static showBusyIndicator(scope?: Object): void;
        static hideBusyIndicator(scope?: Object): void;
        static isIE8(): boolean;
        static CreateCloseIconForDialog(dialog: any): JQuery;
        static getFormSubmitItems(form: JQuery): any;
        static getFunction(code: any, argNames: any): any;
        static addValueToFormData(formData: any, name: string, value: any): any;
    }
}
