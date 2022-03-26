import type { IPlanWardObject } from "./data-models/IPlanWardObject";
import { AllPlanWardObjects, SelectedRhinoObjects } from "./store/MainStore";

export class EventBus {
    constructor() {
        
    }

    SetSelectedObjects(data: string): void {
        let parsed: IPlanWardObject[] = JSON.parse(data);
        if (parsed) {
            SelectedRhinoObjects.set(parsed);
        }
    }

    UpdatePlanWardDataAccounting(data: string): void {
        let parsed = JSON.parse(data);
        if (parsed) {
            AllPlanWardObjects.set(parsed);
        }
    }
}