import type { IPlanWardObject } from "./IPlanWardObject";

export interface IAccountingObject extends IPlanWardObject {
    /**
     * Name of the Region Name that this object belongs to within a Design Option.
     * ex: The "A" in building "A2"
     */
     RegionName: string | undefined;

     /**
      * An Identifier to differentiate itself from other objects within the Region.
      * ex: The "2" in building "A2"
      */
     RegionIdentifier: string | undefined;
}