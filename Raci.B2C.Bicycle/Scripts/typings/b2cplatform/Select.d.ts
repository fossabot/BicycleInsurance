declare module B2C {
    class Select {
        private static _desktopChangingDataName;
        private static _nativeChangingDataName;
        static init(scope: any): void;
        static setNativeComboBoxColor(currentValue: any, selectedElement: any, isCustomCombo: any): void;
        static setComboBoxColor(currentValue: any, wrapper: any, isCustomCombo: any): void;
        static createNativeSelectElement(element: Element, kendoDropDown: any, nativeName: any, nativeId: any): JQuery;
        static onDataSourceItemsChanged(e: any): void;
        static clearSelectOptions(element: any, select: any): void;
        static createSelectOptions(element: any, select: any, dataSource: any): void;
        static createLabelForNativeControl(element: Element, nativeId: string): void;
    }
}
