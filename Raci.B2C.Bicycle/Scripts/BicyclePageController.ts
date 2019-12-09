
var pageController: any = new B2C.DefaultPageController();
pageController.init();

module B2Cextended {
    export class BicyclePageController{
 
        public paymentController:PaymentController;

        constructor() {}

        public init() {
            this.initPaymentController();
        }   

        public initPaymentController(): void {
            
            var paymentController = new B2Cextended.PaymentController;
            paymentController.init();
        }
    }
}
