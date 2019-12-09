var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var B2C;
(function (B2C) {
    var QuotePageController = (function (_super) {
        __extends(QuotePageController, _super);
        function QuotePageController() {
            _super.call(this);
        }
        QuotePageController.prototype.init = function (scope) {
            if (scope === void 0) { scope = 'body'; }
            _super.prototype.init.call(this, scope);
            this.sliderResizer();
            this.loadMobileMarketingContent("/App_Content/marketingCompetitionMobile.html");
            //
            // Do this last so we can show the loading effect
            //
            this.Accordion = new B2C.B2CAccordion(pageController.submitEvent, pageController.backEvent);
            this.rebuildAllSliders();
        };
        QuotePageController.prototype.loadRightRailMarketingContent = function (dataUrl) {
            var target = $("#marketing-area");
            this.loadMarketingContent(dataUrl, target);
        };
        QuotePageController.prototype.onFirstBreakpointLarge = function () {
            _super.prototype.onFirstBreakpointLarge.call(this);
            this.loadRightRailMarketingContent("/App_Content/marketingCompetition.html");
        };
        QuotePageController.prototype.loadMobileMarketingContent = function (dataUrl) {
            var target = $(".mobile-marketing-area");
            this.loadMarketingContent(dataUrl, target);
        };
        QuotePageController.prototype.sliderResizer = function () {
            var _this = this;
            $(window).resize($.debounce(500, function (e) {
                _this.rebuildAllSliders();
            }));
        };
        QuotePageController.prototype.rebuildAllSliders = function () {
            var _this = this;
            $("[data-role=slider][data-sum-insured-text-box-id]").each(function (index, element) {
                _this.rebuildSlider(element);
            });
            $("[data-role=slider][data-slide]").each(function (index, element) {
                _this.rebuildSlider(element);
            });
        };
        QuotePageController.prototype.rebuildSlider = function (element) {
            var kendoElement = ($(element).data("kendoSlider"));
            kendoElement.destroy();
            var input = $(element);
            var parent = $(element).closest(".slider-container");
            $(element).closest(".k-slider").remove();
            parent.append(input);
            input.show();
            kendo.init($(element));
            this.bindSumInsuredChangeEvent();
            this.addSliderTicks(input);
        };
        QuotePageController.prototype.bindSumInsuredChangeEvent = function () {
            var _this = this;
            $("[data-role=slider][data-sum-insured-text-box-id]").each(function (index, element) {
                $(element).data("kendoSlider").bind("slide", function (e) {
                    var textBoxId = $(e.sender.element).data("sum-insured-text-box-id");
                    var textBox = ($("#" + textBoxId).data("kendoNumericTextBox"));
                    textBox.value(e.value);
                });
                $(element).data("kendoSlider").unbind("change");
                $(element).data("kendoSlider").bind("change", $.debounce(500, function (e) {
                    //
                    // If the user clicks on the slider we don't get a slide event
                    // so just maje sure the text box is updated before we submit
                    //
                    var textBoxId = $(e.sender.element).data("sum-insured-text-box-id");
                    var textBox = ($("#" + textBoxId).data("kendoNumericTextBox"));
                    textBox.value(e.value);
                    _this.updatePremium();
                }));
            });
            $("[data-role=numerictextbox][data-sum-insured-slider-id]").each(function (index, element) {
                $(element).data("kendoNumericTextBox").unbind("change");
                $(element).data("kendoNumericTextBox").bind("change", function (e) {
                    if (e.sender.value() == null) {
                        e.sender.value(e.sender.options.min);
                    }
                    var sliderId = $(e.sender.element).data("sum-insured-slider-id");
                    var slider = ($("#" + sliderId).data("kendoSlider"));
                    slider.value("" + e.sender.value());
                    _this.updatePremium();
                });
            });
            $(".customise-your-quote-container input[type='checkbox']").unbind("change");
            $(".customise-your-quote-container input[type='checkbox']").bind("change", function (e) {
                _this.updatePremium();
            });
        };
        QuotePageController.prototype.updatePremium = function () {
            this.doPremiumUpdate($('form').data('update-premium-url'));
        };
        QuotePageController.prototype.doPremiumUpdate = function (updateActionUrl) {
            var _this = this;
            var postData = $('form.insuranceForm').serializeArray();
            pageController.blockUi("Recalculating...");
            $.ajaxWithVerification({
                url: updateActionUrl,
                async: true,
                type: 'POST',
                data: postData,
                dataType: 'html',
                success: (function (data) {
                    var result = _super.prototype.ajaxSubmitSuccess.call(_this, data);
                    if (!B2C.Util.isNullOrEmpty(data) && result.type !== "Redirect") {
                        var item = $("#productTabStrip-1");
                        $(item).html(data);
                        pageController.init(item);
                        var amount = $(".price-amount").html();
                        $(".cover-price").html(amount);
                        pageController.unblockUi();
                    }
                })
            });
        };
        return QuotePageController;
    })(B2C.PageController);
    B2C.QuotePageController = QuotePageController;
})(B2C || (B2C = {}));
//# sourceMappingURL=QuotePageController.js.map