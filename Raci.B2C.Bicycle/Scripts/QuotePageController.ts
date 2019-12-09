module B2C {
    export class QuotePageController extends B2C.PageController {
        constructor() {
            super();
        }

        public init(scope = 'body') {
            super.init(scope);

            this.sliderResizer();
            this.loadMobileMarketingContent("/App_Content/marketingCompetitionMobile.html");
            //
            // Do this last so we can show the loading effect
            //
            this.Accordion = new B2CAccordion(pageController.submitEvent, pageController.backEvent);
            this.rebuildAllSliders();
        }

        private loadRightRailMarketingContent(dataUrl) {
            var target = $("#marketing-area");
            this.loadMarketingContent(dataUrl, target);
        }

        public onFirstBreakpointLarge() {
            super.onFirstBreakpointLarge();
            this.loadRightRailMarketingContent("/App_Content/marketingCompetition.html");
        }

        private loadMobileMarketingContent(dataUrl) {
            var target = $(".mobile-marketing-area");
            this.loadMarketingContent(dataUrl, target);
        }

        public sliderResizer() {

            $(window).resize((<any>$).debounce(500, (e) => {
                this.rebuildAllSliders();
            }));
        }

        private rebuildAllSliders() {
            $("[data-role=slider][data-sum-insured-text-box-id]").each((index, element) => {
                this.rebuildSlider(element);
            });


            $("[data-role=slider][data-slide]").each((index, element) => {
                this.rebuildSlider(element);
            });
        }

        private rebuildSlider(element) {

            var kendoElement = <any>($(element).data("kendoSlider"));
            kendoElement.destroy();

            var input = $(element);
            var parent = $(element).closest(".slider-container");

            $(element).closest(".k-slider").remove();

            parent.append(input);
            input.show();
            kendo.init($(element));

            this.bindSumInsuredChangeEvent();
            this.addSliderTicks(input);
        }

        private bindSumInsuredChangeEvent() {
            $("[data-role=slider][data-sum-insured-text-box-id]").each((index, element) => {

                $(element).data("kendoSlider").bind("slide", (e: kendo.ui.SliderSlideEvent) => {
                    var textBoxId = $(e.sender.element).data("sum-insured-text-box-id");
                    var textBox: kendo.ui.NumericTextBox = <any>($("#" + textBoxId).data("kendoNumericTextBox"));
                    textBox.value(e.value);
                });

                $(element).data("kendoSlider").unbind("change");
                $(element).data("kendoSlider").bind("change", (<any>$).debounce(500, (e) => {
                    //
                    // If the user clicks on the slider we don't get a slide event
                    // so just maje sure the text box is updated before we submit
                    //
                    var textBoxId = $(e.sender.element).data("sum-insured-text-box-id");
                    var textBox: kendo.ui.NumericTextBox = <any>($("#" + textBoxId).data("kendoNumericTextBox"));

                    textBox.value(e.value);
                    this.updatePremium();
                }));
            });

            $("[data-role=numerictextbox][data-sum-insured-slider-id]").each((index, element) => {
                $(element).data("kendoNumericTextBox").unbind("change");
                $(element).data("kendoNumericTextBox").bind("change", (e: kendo.ui.NumericTextBoxEvent) => {
                    if (e.sender.value() == null) {
                        e.sender.value(e.sender.options.min);
                    }

                    var sliderId = $(e.sender.element).data("sum-insured-slider-id");
                    var slider: kendo.ui.Slider = <any>($("#" + sliderId).data("kendoSlider"));
                    slider.value(`${e.sender.value()}`);

                    this.updatePremium();
                });
            });
            $(".customise-your-quote-container input[type='checkbox']").unbind("change");
            $(".customise-your-quote-container input[type='checkbox']").bind("change", (e) => {
                this.updatePremium();
            });
        }

        public updatePremium() {
            this.doPremiumUpdate($('form').data('update-premium-url'));
        }

        private doPremiumUpdate(updateActionUrl) {
            var postData = $('form.insuranceForm').serializeArray();

            pageController.blockUi("Recalculating...");

            (<JqueryBCCCommon>$).ajaxWithVerification({
                url: updateActionUrl,
                async: true,
                type: 'POST',
                data: postData,
                dataType: 'html',
                success: ((data) => {
                    var result = super.ajaxSubmitSuccess(data);

                    if (!Util.isNullOrEmpty(data) && result.type !== "Redirect") {
                        var item = $("#productTabStrip-1");

                        $(item).html(data);

                        pageController.init(item);

                        var amount = $(".price-amount").html();
                        $(".cover-price").html(amount);
                        pageController.unblockUi();
                    }
                })
            });
        }
    }
}
