
// todo:  this needs to be cleaned up and we need to Typescriptify this. 

//var clientToken = "@Model.ClientToken";

function removeValidationErrorFromContainer(container) {
    $(container).find(".field-validation-error").remove();
}

function addValidationErrorToContainer(container, message) {

    var validationControl = $(container).find(".field-validation-error");

    if (validationControl.length === 0) {
        validationControl = $("body").find(".field-validation-error:last").clone();
        validationControl.css("display", "inline-block");
        $(container).append(validationControl);
    }

    validationControl.text(message);
}

function setCardType(type) {
    $("#@cardnumberField").siblings("#payment-method").removeClass();
    $("#@cardnumberField").siblings("#payment-method").addClass("payment-method " + type);
}


function showHostedFieldSuccess(container) {
    var field = $(container).addClass("large-10");
    field.siblings("#tick").show();
}

function hideHostedFieldSuccess(field) {
    field.removeClass("large-10");
    field.siblings("#tick").hide();
}

function onHostedFieldFocus(event) { }
function onHostedFieldBlur(event) { }

function onHostedFieldStateChanged(event) {
    var field = $("#" + event.target.container.id);

    if (event.isValid) {
        showHostedFieldSuccess(field);
        removeValidationErrorFromContainer($(field.parent()));
    }

    if (event.isPotentiallyValid && !event.isValid) {
        hideHostedFieldSuccess(field);
        //removeValidationErrorFromContainer($(field.parent()));
    }

    if (!event.isValid && !event.isPotentiallyValid && !event.isEmpty) {
        hideHostedFieldSuccess(field);

        addValidationErrorToContainer(field.parent(), "Please provide a valid " + $('label[for=' + field.attr("id") + ']').text().toLowerCase());
    }

    if (event.isEmpty) {
        hideHostedFieldSuccess(field);
        //removeValidationErrorFromContainer($(field.parent()));
    }

    if (event.card) {
        console.log(event.card.niceType);
        setCardType(extractClassName(event.card.niceType));
    }
}

function extractClassName(cardType) {
    var result = cardType.toLowerCase();
    return result;
}

function onFieldEvent(event) {
    if (event.type === "focus") {
        onHostedFieldFocus(event);
    } else if (event.type === "blur") {
        onHostedFieldBlur(event);
    } else if (event.type === "fieldStateChange") {
        onHostedFieldStateChanged(event);
    }
}

function onPaymentMethodReceived(object) {
    $("#payment_method_nonce").val(object.nonce);
    pageController.submitForm($('#form0'), 'SubmitAction', 'Submit');
}

function onReady(ready) {
    removeSubmitBindings();
}


function onError(error) {

    // todo refactor this, its quick and dirty.


    if (error.details === undefined) {
        error["details"] = {}
        error.details["invalidFieldKeys"] = ["cvv", "number", "expirationDate"];
    }

    for (var item in error.details.invalidFieldKeys) {
        var field = error.details.invalidFieldKeys[item];
        var jfield;
        var message = "Please enter valid card details.";

        if (field === "number") {
            jField = $("#@cardnumberField");
            message = "Please provide a valid credit card number";
        }
        else if (field === "cvv") {
            jField = $("#@cvvFIeld");
            message = "Please provide a valid CVV";
        }
        else if (field === "expirationDate") {
            jField = $("#@expirationDateField");
            message = "Please provide a valid expiration date";
        }
        addValidationErrorToContainer(jField.parent(), message);
    }


    pageController.unblockUi();

}


/*
 * Remove document submit events, they are interfering with braintree (on submit only) nonce retrieval *(&!*&^!*(*!!!!!!!!
 */
function removeSubmitBindings() {
    $(document).off("submit");
    $('[id$="_submit-action"]').off("submit");
    $('[id$="_submit-action"]').off("click");

    $('[id$="_submit-action"]').off("click.inprogress").on("click.inprogress", function () {
        pageController.blockUi();
    });
}

var braintreeConfig = {
    id: "form0",
    onPaymentMethodReceived: onPaymentMethodReceived,
    onReady: onReady,
    onError: onError,
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
        onFieldEvent: onFieldEvent
    }
}

braintree.setup(clientToken, "custom", braintreeConfig);

// braintree config help: https://developers.braintreepayments.com/javascript+dotnet/guides/hosted-fields/configuration

