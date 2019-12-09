///<reference path="typings/jquery/jquery.d.ts"/>

module B2CExtended {
    export class ProductTabStrip {

        public ProductTabStripId: string = "#productTabStrip";
        public Kendo;

        constructor() {}

        public init() {
            this.Kendo = $(this.ProductTabStripId).data("kendoTabStrip");
        }

        public GetSumInsuredForTabIndex(tabIndex) {
            var content = this.Kendo.contentHolder(tabIndex);
            var sumInsured = $(content).find(".price-amount").html();

            return sumInsured;
        }

        public SelectedIndex() {
            return this.Kendo.select().index();
        }

        public SelectedContentHolder() {
            return this.Kendo.contentHolder(this.SelectedIndex());
        }

        public UpdateSumInsuredOnTab(tabIndex) {
            var sumInsured = this.GetSumInsuredForTabIndex(tabIndex);
            var tab = $("#productTabStrip > .k-tabstrip-items > .k-item")[tabIndex];
            var priceElement = $(tab).find(".cover-price");

            priceElement.html(sumInsured);
        }
    }
};