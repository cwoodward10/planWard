import type { IAccountingObject } from "./IAccountingObject";
import type { IPlanWardObject } from "./IPlanWardObject";

export interface IParking extends IPlanWardObject, IAccountingObject {
    IsCompact: boolean | null,
    IsAngled: boolean | null,
    IsHandicap: boolean | null,
}