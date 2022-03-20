import type { IPlanWardObject } from "$modules/data-models/IPlanWardObject";
import { PlanWardTypes } from "./PlanWardTypesEnum";

import DesignOptionsForm from "$lib/attribute-editor/forms/DesignOptionsForm.svelte";
import BasicPlanWardForm from "$lib/attribute-editor/forms/BasicPlanWardForm.svelte";
import BuildingForm from "$lib/attribute-editor/forms/BuildingForm.svelte";
import ParkingForm from "$lib/attribute-editor/forms/ParkingForm.svelte";

export const FORM_MULTIPLE_VALUES_STRING: string = "<varies>";

export function GetCorrectAttributeEditorForm(objects: IPlanWardObject[]) {
    // return null if there are no objects in this list
    if (objects.length === 0) {
        return null;
    }

    let firstType = objects[0].PlanWardType;
    // if all objects are the same type, conversion is straightforward
    if (objects.every(o => o.PlanWardType === firstType)) {
        switch (firstType) {
            case PlanWardTypes.DesignOptionContainer:
                return DesignOptionsForm;
            case PlanWardTypes.Building:
                return BuildingForm;
            case PlanWardTypes.ParkingSpace:
                return ParkingForm;
            default:
                return DesignOptionsForm;
        }
    }

    // if contains a DesignOptionContainer, return the container form
    // otherwise return the basic planward form
    if (objects.some(o => o.PlanWardType == PlanWardTypes.DesignOptionContainer)) {
        return DesignOptionsForm;
    } else {
        return BasicPlanWardForm;
    }
}

const DEFAULT_PLACEHOLDER_VALUE: string = "Set Value";
export class FormInputValueHelper {
    //#region design option properties

    placeholderDesignOption: string | null = DEFAULT_PLACEHOLDER_VALUE;
    actualDesignOption: string | null = null;

    //#endregion design option properties
    
    //#region region properties

    placeholderRegionName: string = DEFAULT_PLACEHOLDER_VALUE;
    actualRegionName: string | null = null;

    placeholderRegionIdentifier: string = DEFAULT_PLACEHOLDER_VALUE;
    actualRegionIdentifier: string | null = null;

    //#endregion region properties

    //#region building properties

    placeholderBuildingProgram: string = DEFAULT_PLACEHOLDER_VALUE;
    actualBuildingProgram: string | null = null;

    //#endregion building properties

    //#endregion parking properties

    placeholderParkingCompact: boolean | null = null;
    actualParkingCompact: boolean | null = null;

    placeholderParkingAngled: boolean | null = null;
    actualParkingAngled: boolean | null = null;

    placeholderParkingHandicap: boolean | null = null;
    actualParkingHandicap: boolean | null = null;

    //#endregion parking properties

    constructor() {}

    SetFormText(objects: IPlanWardObject[]) {
        if (objects.length < 1) {
            return [];
        }

        let firstObject: any = objects[0];

        //#region design option
        // set design option
        if (objects.every(o => o.DesignOption == firstObject.DesignOption)) {
            this.actualDesignOption = firstObject.DesignOption;
        } else {
            this.placeholderDesignOption = FORM_MULTIPLE_VALUES_STRING;
        }
        //#endregion design option

        //#region region properties
        // set region name if exists
        if (firstObject.RegionName != undefined) {
            if (objects.every((o : any) => o.RegionName == firstObject.RegionName)) {
                this.actualRegionName = firstObject.RegionName;
            } else {
                this.placeholderRegionName = FORM_MULTIPLE_VALUES_STRING;
            }
        }

        // set region identifier if exists
        if (firstObject.RegionIdentifier != undefined) {
            if (objects.every((o : any) => o.RegionIdentifier == firstObject.RegionIdentifier)) {
                this.actualRegionIdentifier = firstObject.RegionIdentifier;
            } else {
                this.placeholderRegionIdentifier = FORM_MULTIPLE_VALUES_STRING;
            }
        }
        //#endregion region properties

        //#region building properties
        if (firstObject.ProgramType != undefined) {
            if (objects.every((o : any) => o.ProgramType == firstObject.ProgramType)) {
                this.actualBuildingProgram = firstObject.ProgramType;
            } else {
                this.placeholderBuildingProgram = FORM_MULTIPLE_VALUES_STRING;
            }
        }
        //#endregion building properties

        //#region  parking properties
        if (firstObject.IsCompact != undefined) {
            if (objects.every((o : any) => o.IsCompact == firstObject.IsCompact)) {
                this.actualParkingCompact = firstObject.IsCompact;
            }
        }

        if (firstObject.IsAngled != undefined) {
            if (objects.every((o : any) => o.IsAngled == firstObject.IsAngled)) {
                this.actualParkingAngled = firstObject.IsAngled;
            }
        }

        if (firstObject.IsHandicap != undefined) {
            if (objects.every((o : any) => o.IsHandicap == firstObject.IsHandicap)) {
                this.actualParkingHandicap = firstObject.IsHandicap;
            }
        }
        //#endregion parking properties
    }
}