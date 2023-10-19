export interface IFormHandler {
    (target: EventTarget & HTMLInputElement |
             EventTarget & HTMLSelectElement |
             EventTarget & HTMLTextAreaElement): void
}
