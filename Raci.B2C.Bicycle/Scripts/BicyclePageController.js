var pageController = new B2C.DefaultPageController();
pageController.init();
var B2Cextended;
(function (B2Cextended) {
    var BicyclePageController = (function () {
        function BicyclePageController() {
        }
        BicyclePageController.prototype.init = function () {
            this.initPaymentController();
        };
        BicyclePageController.prototype.initPaymentController = function () {
            var paymentController = new B2Cextended.PaymentController;
            paymentController.init();
        };
        return BicyclePageController;
    })();
    B2Cextended.BicyclePageController = BicyclePageController;
})(B2Cextended || (B2Cextended = {}));
//# sourceMappingURL=BicyclePageController.js.map