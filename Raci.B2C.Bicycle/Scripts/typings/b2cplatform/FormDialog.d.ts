declare module B2C {
    class FormDialogOptions {
        titleText: any;
        formContainerSelector: any;
        containerId: any;
        cloneContainerId: any;
        onContentChangedEvent: any;
        reloadOnSubmit: any;
        width: any;
        checkViewIsValid: any;
        isSubmitResultScript: any;
        onCancel: any;
        onSubmitResultEmptyEvent: any;
        constructor();
    }
    class FormDialog {
        private _options;
        static init(scope: any): void;
        constructor(options: FormDialogOptions);
        init(): void;
        private kendoWindow();
        private performSubmit();
    }
}
