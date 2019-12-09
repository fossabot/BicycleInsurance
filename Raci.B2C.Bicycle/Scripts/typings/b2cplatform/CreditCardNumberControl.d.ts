declare module B2C {
    class CreditCardNumberControl {
        private static _namespace;
        private static KEY_TAB;
        private static KEY_SHIFT;
        private static KEY_CAPSLOCK;
        static initControlsInScope(scope: string): void;
        private static initControl(control);
        private static unbindControl(control);
        private static moveToNextElementOnKeyup(e, me, nextElement);
    }
}
