declare module B2C {
    class Dependency {
        _page: B2C.PageController;
        constructor(page: B2C.PageController);
        updateSimpleControlDependencies(event: JQueryEventObject): void;
        updateRadioSetControlDependencies(event: JQueryEventObject): void;
        checkParentQuestionSetContainer(childControl: any): void;
        isVisible(inputName: string): boolean;
        updateFirstQuestionSetContainerClass(): void;
        hideQuestion(elementToHide: any): void;
        showQuestion(elementToShow: any): void;
        toggleQuestion(elementToToggle: any, showQuestion: any): void;
        getDependencyUrl(): string;
        updateControlDependencies(propertyName: any, propertyValue: any): void;
        private escapeExpression(str);
        setupEvents(scope: any): void;
        private setupEventsForElement(element);
    }
}
declare var dependencyController: B2C.Dependency;
