import type { PlanWardTypes } from "$modules/application/PlanWardTypesEnum";

export interface IPlanWardObject {
    /** Persistent Id of the object in Rhino */
    RhinoId: string;
    
    /** Type of PlanWard object as defined from C# */
    PlanWardType: PlanWardTypes;

    /** Name of the Design Option that this Rhino Object belongs to. */
    DesignOption: string | undefined;
}