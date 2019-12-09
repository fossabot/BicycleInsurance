declare var dataLayer: any[];
declare module B2C {
    class DataLayerManager {
        private static _dataTrackObjectAttributeName;
        static pushDataObjectForDataLayerTrackedElement(element: string): void;
        static pushDataObjectsForDataLayerTrackedElements(scope: string): void;
        private static getDataObject(element);
        static pushObjectToDataLayer(dataObject: any): void;
    }
}
