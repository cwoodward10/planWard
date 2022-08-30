import MainInformationViewer from "../../lib/information-viewer/MainInformationView.svelte";
import MainAttributeEditor from "../../lib/attribute-editor/MainAttributeEditor.svelte";
import MainSettingsView from "../../lib/settings/MainSettingsView.svelte";

/**
 * Enum used to track App State. Will be referenced by the App's Store.
 * 
 */
export enum AppStateEnum {
    MainInformation,
    MainAttributeEditor,
}

/**
 * Uses the app state enum to return a svelte component that corresponds to it.
 * @param appState 
 * @returns 
 */
export function GetComponentFromAppState(appState: AppStateEnum) {
    switch(appState) {
        case AppStateEnum.MainInformation:
            return MainInformationViewer;
        case AppStateEnum.MainAttributeEditor:
            return MainAttributeEditor;
    }
}