import type { IPlanWardObject } from "./data-models/IPlanWardObject";
import { ParkingCount, SelectedRhinoObjects } from "./store/MainStore";
import { TotalSquareFootage } from "./store/MainStore";

export class EventBus {
    constructor() {
        
    }

    SetSelectedObjects(data: string): void {
        console.log(data);
        let parsed: IPlanWardObject[] = JSON.parse(data);
        if (parsed) {
            SelectedRhinoObjects.set(parsed);
        }
    }

    UpdateParkingCount(data: string): void {
        console.log(data);
        let parsed = JSON.parse(data);
        if (!Number.isNaN(parsed)) {
            ParkingCount.set(parsed);
        }
    }
    
    UpdateTotalSquareFootage(data: string): void {
        console.log(data);
        let parsed = JSON.parse(data);
        if (!Number.isNaN(parsed)) {
            TotalSquareFootage.set(parsed);
        }
    }
}