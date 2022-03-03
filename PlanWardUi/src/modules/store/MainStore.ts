import { writable } from "svelte/store";
import { AppStateEnum } from "../application/ApplicationStateHelpers";
import type { TrackedRhinoObject } from "../data-models/TrackedRhinoObject";

/**
 * Controls the state of the App and thus what is show in the UI
 */
export const ApplicationState = writable(AppStateEnum.MainInformation);

/**
 * Array of Objects that are selected in Rhino. Empty Array if nothing is selected.
 */
export const SelectedRhinoObjects = writable<TrackedRhinoObject[]>([]);

/**
 * Global parking count for the active rhino doc
 */
export const ParkingCount = writable(0);

/**
 * Global Total Square Footage for the active rhino do
 */
export const TotalSquareFootage = writable(0);
