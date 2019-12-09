declare module B2C {
    class PaymentPageController extends B2C.PageController {
        constructor();
        init(scope?: string): void;
        setupLivePersonClickEvents(): void;
        getSessionId(): string;
        changeButtonText(): void;
    }
}
declare var pageController: any;
