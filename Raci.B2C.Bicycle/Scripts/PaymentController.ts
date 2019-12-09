var pageController: any = new B2C.DefaultPageController();
pageController.init();

module B2Cextended {

    export class PaymentController {

        constructor() {}

        public init() {
        }

        public onReady(ready): void {}

        public onPaymentMethodReceived(object): void {
            $("#payment_method_nonce").val(object.nonce);
            $("#form0").submit();
            alert(object.nonce);
        }

        public braintreeConfig = {
            id: "form0",
            onPaymentMethodReceived: (object) => this.onPaymentMethodReceived(object),
            onReady: (ready) => this.onReady(ready),
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
                onFieldEvent: (event) => this.onFieldEvent(event)
            }
        }

        public onFieldEvent(event): void {
            if (event.type === "focus") {
                this.onHostedFieldFocus(event);
            } else if (event.type === "blur") {
                this.onHostedFieldBlur(event);
            } else if (event.type === "fieldStateChange") {
                this.onHostedFieldStateChanged(event);
            }

        }
        
        public removeValidationErrorFromContainer(container): void {
            $(container).find(".field-validation-error").remove();
        }

        public addValidationErrorToContainer(container): void {
            if ($(container).find(".field-validation-error").length === 0) {
                var validationControl = $("body").find(".field-validation-error:last").clone();
                validationControl.css("display", "block");
                $(container).append(validationControl);
            }
        }

        public setCardType(type): void {
            $("#card-number").siblings("#payment-method").removeClass();
            $("#card-number").siblings("#payment-method").addClass("payment-method " + type);
        }

        public showHostedFieldSuccess(container): void {
            var field = $(container).addClass("large-10");
            field.siblings("#tick").show();
        }

        public hideHostedFieldSuccess(field): void {
            field.removeClass("large-10");
            field.siblings("#tick").hide();
        }

        public onHostedFieldFocus(event): void {}

        public onHostedFieldBlur(event): void {}

        public onHostedFieldStateChanged(event): void {
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
        }

        public extractClassName(cardType): string {
            var result = cardType.toLowerCase();
            return result;
        }
    }
}
