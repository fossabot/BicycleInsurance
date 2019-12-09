declare var Response: any;
declare var pageController: any;
declare var dataLayer: any[];
declare module B2C {
    class PageController {
        _clueTipWidth: number;
        private breakpointLargeFirst;
        private _hasInitializedBreakpoints;
        _dependency: B2C.Dependency;
        _ijento: Ijento;
        Accordion: B2CAccordion;
        constructor();
        init(scope?: string): void;
        private terminateSession();
        private bindDisplayBusyOnClick(scope);
        bindServiceAvailability(): void;
        bindLogoClickEvent(): void;
        /** This is a hack to fix a bug in iOS6, where text placeholders push the
         *  layout too wide when switching orientation on the phone.
         *  Note that this bug goes away in iOS7, and the hack should be removed once
         *  it becomes the predominant iPhone OS.
         */
        refreshPlaceholders(): void;
        setJavascriptCheckValue(): void;
        checkServiceAvailability(e: any, data: any): void;
        private initBusyIndicator();
        supressEnterKeyDefaultBehavior(scope: any): void;
        moveToNextElementOnKeyup(e: any, me: any, nextElement: any): boolean;
        getSessionId(): string;
        getAntiForgeryTokenValue(): any;
        updateFirstQuestionSetContainerClass(): void;
        initialisePageControls(scope: any): void;
        initRadioButtons(scope: any): void;
        initCheckBoxes(scope: any): void;
        qualifyHelpIcons(scope: any): void;
        InitSuburbControls(scope: any): void;
        initDatePickerControls(scope: any): void;
        private setInputFocusStyle();
        private setTextAreaFocusStyle();
        private applyErrorsToKendoControls();
        setupHelpToolTips(scope: any): void;
        setupHelpTooltipsOld(scope: any): void;
        private onHelpShown(clueTipElement, clueTipInnerDiv);
        qualifyTooltipImages(scope: any): void;
        private onTooltipHide(clueTipElement, clueTipInnerDiv);
        hideNonVisibleControls(scope?: string): void;
        private updateCommonWatermarks();
        addSliderTicks(sliderControl: any): void;
        submitForm(aControl: any, aFormInputName: any, aValue: any, sectionId?: any): boolean;
        private addField(form, fieldName, value);
        getRootUrl(): string;
        blockUi(message?: any): void;
        private disableInputType(scope, inputType);
        disableTextInputs(scope?: string): void;
        reenableTextInputs(scope?: string): void;
        blockUiForTooltip(showOverlay: boolean): void;
        unblockUi(fadeOut?: number): void;
        private objectDataAttribute;
        private changeEventAttribute;
        private clickEventAttribute;
        initDataLayerEventHandlers(scope: any): void;
        addObjectDataToDataLayer(scope: any): void;
        private GAEventHandlers(event);
        showInsuranceForm(): void;
        preLoadImages(): void;
        private initResponsiveJs();
        private onBreakpointChanged();
        onBreakpointLarge(): void;
        onOtherBreakpointLarge(): void;
        onFirstBreakpointLarge(): void;
        onBreakpointMedium(): void;
        onBreakpointSmall(): void;
        loadMarketingContent(dataUrl: any, target: any, doneFunction?: Function): void;
        print(url: any, blockUiText?: any, isFormDirty?: boolean): void;
        submitCallback(): void;
        processXhrError401(jsonResponse: any): void;
        processXhrErrors(xhr: any): void;
        ajaxSubmitSuccess(data: any, status?: any, xhr?: any): any;
        getDialogWidth(): number;
        setupSimpleDialog(data: any, title: any, onCloseAction: string): void;
        setupCallbackDialog(data: any, title: any, onCloseAction: string): void;
        showCallMeBackInfo(): void;
        submitFormWithAjax(): void;
        onDialogClose(action?: string): void;
        submitEvent(e: any): boolean;
        backEvent(e: any): boolean;
        static clickCheckbox(elementId: any): void;
        terminateServerSession(url: any): void;
    }
}
