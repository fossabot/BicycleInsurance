///<reference path="typings/jquery/jquery.d.ts"/>
var B2CExtended;
(function (B2CExtended) {
    var ProductTabStrip = (function () {
        function ProductTabStrip() {
            this.ProductTabStripId = "#productTabStrip";
        }
        ProductTabStrip.prototype.init = function () {
            this.Kendo = $(this.ProductTabStripId).data("kendoTabStrip");
        };
        ProductTabStrip.prototype.GetSumInsuredForTabIndex = function (tabIndex) {
            var content = this.Kendo.contentHolder(tabIndex);
            var sumInsured = $(content).find(".price-amount").html();
            return sumInsured;
        };
        ProductTabStrip.prototype.SelectedIndex = function () {
            return this.Kendo.select().index();
        };
        ProductTabStrip.prototype.SelectedContentHolder = function () {
            return this.Kendo.contentHolder(this.SelectedIndex());
        };
        ProductTabStrip.prototype.UpdateSumInsuredOnTab = function (tabIndex) {
            var sumInsured = this.GetSumInsuredForTabIndex(tabIndex);
            var tab = $("#productTabStrip > .k-tabstrip-items > .k-item")[tabIndex];
            var priceElement = $(tab).find(".cover-price");
            priceElement.html(sumInsured);
        };
        return ProductTabStrip;
    })();
    B2CExtended.ProductTabStrip = ProductTabStrip;
})(B2CExtended || (B2CExtended = {}));
;
//# sourceMappingURL=ProductTabStrip.js.map