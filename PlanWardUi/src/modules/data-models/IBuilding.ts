import type { IAccountingObject } from "./IAccountingObject";
import type { IPlanWardObject } from "./IPlanWardObject";

export interface IBuilding extends IPlanWardObject, IAccountingObject {
    ProgramType: string,
    Area: number
}