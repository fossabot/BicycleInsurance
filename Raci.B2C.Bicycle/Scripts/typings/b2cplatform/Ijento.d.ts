declare var SITEINTEL: any;
declare module B2C {
    interface IjentoSettings {
        taggingServer: string;
        trackerUrl: string;
        trackAbandonment: boolean;
        trackFormId: string;
        trackParameters: IjentoTrackParameters[];
    }
    interface IjentoTrackParameters {
        Key: string;
        Value: any;
    }
    class Ijento {
        private _tracker;
        init(scope: any): void;
        private trackAdditionalParameters(trackParameters);
        doTrackingOnField(item: any): void;
        private setupFormAbandonmentTracking(formElement);
    }
}
