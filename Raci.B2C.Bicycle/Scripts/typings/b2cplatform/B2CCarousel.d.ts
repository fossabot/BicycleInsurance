declare module B2C {
    class B2CCarousel {
        private _namespace;
        private _selectedClass;
        private _selector;
        private _owlCarousel;
        private _carouselItems;
        private _selectedIndex;
        private _itemCount;
        private _onSelectEvent;
        constructor(selector: string);
        refreshCarousel(): void;
        private unbindEvents();
        setOnSelectEvent(selectEvent: (index, element) => void): void;
        setSelectedTab(index: number, animateMoveTo?: boolean): void;
        isItemVisible(index: number): boolean;
        private ensureVisibleTabIsSelected();
        private init();
        getItemCount(): number;
    }
}
