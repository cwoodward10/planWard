import type { IPlanWardObject } from "./data-models/IPlanWardObject";
import { AllPlanWardObjects, ApplicationAlert, SelectedRhinoObjects } from "./store/MainStore";

export class EventBus {
    constructor() {
        
    }

    SendApplicationMessage(
        message: string, 
        type: "Success" | "Warning" | "Error",
        timeout: number,
        ): void {
        ApplicationAlert.set({
            message: message,
            type: type,
            timeout: timeout,
        })
    }

    SetSelectedObjects(data: string): void {
        let parsed: IPlanWardObject[] = JSON.parse(data);
        if (parsed) {
            SelectedRhinoObjects.set(parsed);
        }
    }

    UpdatePlanWardDataAccounting(data: string): void {
        try {
            let parsed = JSON.parse(data);
            if (parsed) {
                AllPlanWardObjects.set(parsed);
                this.SendApplicationMessage("UI Up-to-Date", "Success", 500);
            }
        } catch {
            this.SendApplicationMessage("Could not Update UI", "Error", 800);
        }
    }
}