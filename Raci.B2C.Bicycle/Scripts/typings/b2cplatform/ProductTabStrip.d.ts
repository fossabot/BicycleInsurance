declare module B2C {
    class ProductTabStrip {
        ProductTabStripId: string;
        Kendo: any;
        constructor();
        init(): void;
        GetSumInsuredForTabIndex(tabIndex: any): string;
        SelectedIndex(): any;
        SelectedContentHolder(): any;
        UpdateSumInsuredOnTab(tabIndex: any): void;
    }
}
