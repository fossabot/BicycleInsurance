declare module B2C {
    class B2CAccordion {
        private _openedClass;
        private _closedClass;
        private _loadingClass;
        private _loadingAndOpened;
        private _allPanels;
        private _allHeadings;
        private _selectedAccordion;
        private _erroredPanel;
        private _submitEvent;
        private _backEvent;
        constructor(submitEvent: (e) => void, backEvent: (e) => void);
        private indexAccordions();
        hideAccordions(idsToHide: any): void;
        private hideAccordion(id);
        refreshAccordions(): void;
        claerErrorsOnSelectedAccordian(): void;
        clearAllErrors(): void;
        private unbindEvents();
        private configureAccordionButtons();
        private init(accordionHeader, accordionPanel);
        private showPanel(accordionHeader, accordionPanel, slideLastAccordionOut?, slideNewAccordionIn?);
        private onHeadingClick(e);
        getIndexOfNextVisibleAccordion(currentIndex: any): any;
        getIndexOfPreviousVisibleAccordion(currentIndex: any): number;
        getCurrentOpenAccordionIndex(): number;
        openAccordionAtIndex(index: any, slideLastAccordionOut?: boolean, slideNewAccordionIn?: boolean): void;
        openAccordionWithId(id: any, slideLastAccordionOut?: boolean, slideNewAccordionIn?: boolean): void;
        onContinueClick(e: any): boolean;
        onBackClick(e: any): boolean;
        clearSelectedAccordian(): void;
    }
}
