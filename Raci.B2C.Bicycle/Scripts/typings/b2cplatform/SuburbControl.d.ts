declare module B2C {
    class SuburbControl {
        private _completionListUrl;
        private _zipIdControl;
        private _cityIdControl;
        private _suburbTextControl;
        private _postCodeControl;
        private _resultCallBack;
        private _antiforgeryTokenValue;
        private _includePoBox;
        private _suburbControl;
        private _kendoControl;
        private _dataSource;
        constructor(zipId: any, cityId: any, suburbTextId: any, postCodeId: any, completionListUrl: any, resultCallBack: any, antiforgeryTokenValue: any, includePoBox: any);
        init(): void;
        private onSelect(event);
        getText(): any;
        isTextPostCode(): boolean;
    }
}
