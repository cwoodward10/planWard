import { PlanWardTypes } from "$modules/application/PlanWardTypesEnum";
import type { IBuilding } from "$modules/data-models/IBuilding";
import type { IParking } from "$modules/data-models/IParking";
import { derived, writable } from "svelte/store";
import { AppStateEnum } from "../application/ApplicationStateHelpers";
import type { IPlanWardObject } from "../data-models/IPlanWardObject";

//#region UI state constants

/**
 * Controls the Alert Message Displayed at the top of the UI
 */
export const ApplicationAlert = writable({
    message: "",
    type: "Success" as "Success" | "Warning" | "Error",
    timeout: 0,
});
export const ApplicationAlertMessage = derived(ApplicationAlert, $ApplicationAlert => {
    return $ApplicationAlert.message;
})
export const ApplicationAlertType = derived(ApplicationAlert, $ApplicationAlert => {
    return $ApplicationAlert.type;
})
export const ApplicationAlertTimeout = derived(ApplicationAlert, $ApplicationAlert => {
    return $ApplicationAlert.timeout;
})

/**
 * Controls the state of the App and thus what is show in the UI
 */
export const ApplicationState = writable(AppStateEnum.MainInformation);

//#endregion UI state constants

//#region Rhino state constants

/**
 * Array of Objects that are selected in Rhino. Empty Array if nothing is selected.
 */
export const SelectedRhinoObjects = writable<IPlanWardObject[]>([]);

/**
 * Global List of Active, Eligible PlanWard Objects in the Active RhinoDoc.
 * Requires callback to Rhino to be updated
 */
export const AllPlanWardObjects = writable<IPlanWardObject[]>([]);

/**
 * Reaonly List of Buildings derived from AllPlanWardObjects.
 */
export const PlanWardBuildings = derived(AllPlanWardObjects, $AllPlanWardObjects => {
    return $AllPlanWardObjects.filter(obj => obj.PlanWardType === PlanWardTypes.Building) as IBuilding[];
})

/**
 * Readonly List of Parking Spaces derived from AllPlanWardObjects
 */
export const PlanWardParking = derived(AllPlanWardObjects, $AllPlanWardObjects => {
    return $AllPlanWardObjects.filter(obj => obj.PlanWardType === PlanWardTypes.ParkingSpace) as IParking[]; 
})

//#endregion Rhino state constants
