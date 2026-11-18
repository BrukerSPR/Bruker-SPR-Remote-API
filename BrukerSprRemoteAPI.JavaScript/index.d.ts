export { BrukerSprRemoteAPI } from './BrukerSprRemoteAPI';
export { Tools } from './Tools';

export class Tools {
    constructor(api: any);
    readonly api: any;
    WaitForReady(pollIntervalMs?: number, onProgress?: (mode: number) => void): Promise<void>;
    waitForReady(pollIntervalMs?: number, onProgress?: (mode: number) => void): Promise<void>;
    StartLogging(
        handlers?: {
            onMessage?: (message: string[]) => void;
            onError?: (error: string) => void;
            onWarning?: (warning: string) => void;
        },
        pollIntervalMs?: number
    ): void;
    startLogging(
        handlers?: {
            onMessage?: (message: string[]) => void;
            onError?: (error: string) => void;
            onWarning?: (warning: string) => void;
        },
        pollIntervalMs?: number
    ): void;
    StopLogging(): void;
    stopLogging(): void;
}
