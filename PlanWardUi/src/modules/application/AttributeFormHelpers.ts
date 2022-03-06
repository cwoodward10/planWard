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