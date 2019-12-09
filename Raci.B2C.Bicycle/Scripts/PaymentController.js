var pageController = new B2C.DefaultPageController();
pageController.init();
var B2Cextended;
(function (B2Cextended) {
    var PaymentController = (function () {
        function PaymentController() {
            var _this = this;
            this.braintreeConfig = {
                id: "form0",
                onPaymentMethodReceived: function (object) { return _this.onPaymentMethodReceived(object); },
                onReady: function (ready) { return _this.onReady(ready); },
                hostedFields: {
                    styles: {},
                    number: {
                        selector: "#card-number",
                        placeholder: "Card Number"
                    },
                    cvv: {
                        selector: "#cvv",
                        placeholder: "CVV"
                    },
                    expirationDate: {
                        selector: "#expiration-date",
                        placeholder: "Expiration Date MM/YY"
                    },
                    onFieldEvent: function (event) { return _this.onFieldEvent(event); }
                }
            };
        }
        PaymentController.prototype.init = function () {
        };
        PaymentController.prototype.onReady = function (ready) { };
        PaymentController.prototype.onPaymentMethodReceived = function (object) {
            $("#payment_method_nonce").val(object.nonce);
            $("#form0").submit();
            alert(object.nonce);
        };
        PaymentController.prototype.onFieldEvent = function (event) {
            if (event.type === "focus") {
                this.onHostedFieldFocus(event);
            }
            else if (event.type === "blur") {
                this.onHostedFieldBlur(event);
            }
            else if (event.type === "fieldStateChange") {
                this.onHostedFieldStateChanged(event);
            }
        };
        PaymentController.prototype.removeValidationErrorFromContainer = function (container) {
            $(container).find(".field-validation-error").remove();
        };
        PaymentController.prototype.addValidationErrorToContainer = function (container) {
            if ($(container).find(".field-validation-error").length === 0) {
                var validationControl = $("body").find(".field-validation-error:last").clone();
                validationControl.css("display", "block");
                $(container).append(validationControl);
            }
        };
        PaymentController.prototype.setCardType = function (type) {
            $("#card-number").siblings("#payment-method").removeClass();
            $("#card-number").siblings("#payment-method").addClass("payment-method " + type);
        };
        PaymentController.prototype.showHostedFieldSuccess = function (container) {
            var field = $(container).addClass("large-10");
            field.siblings("#tick").show();
        };
        PaymentController.prototype.hideHostedFieldSuccess = function (field) {
            field.removeClass("large-10");
            field.siblings("#tick").hide();
        };
        PaymentController.prototype.onHostedFieldFocus = function (event) { };
        PaymentController.prototype.onHostedFieldBlur = function (event) { };
        PaymentController.prototype.onHostedFieldStateChanged = function (event) {
            var field = $("#" + event.target.container.id);
            if (event.isValid) {
                this.showHostedFieldSuccess(field);
                this.removeValidationErrorFromContainer($(field.parent()));
            }
            if (event.isPotentiallyValid && !event.isValid) {
                this.hideHostedFieldSuccess(field);
                this.removeValidationErrorFromContainer($(field.parent()));
            }
            if (!event.isValid && !event.isPotentiallyValid && !event.isEmpty) {
                this.hideHostedFieldSuccess(field);
                this.addValidationErrorToContainer(field.parent());
            }
            if (event.isEmpty) {
                this.hideHostedFieldSuccess(field);
                this.removeValidationErrorFromContainer($(field.parent()));
            }
            if (event.card) {
                console.log(event.card.niceType);
                this.setCardType(this.extractClassName(event.card.niceType));
            }
        };
        PaymentController.prototype.extractClassName = function (cardType) {
            var result = cardType.toLowerCase();
            return result;
        };
        return PaymentController;
    })();
    B2Cextended.PaymentController = PaymentController;
})(B2Cextended || (B2Cextended = {}));
//# sourceMappingURL=PaymentController.js.map