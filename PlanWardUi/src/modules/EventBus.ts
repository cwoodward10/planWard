import { ParkingCount } from "./store/MainStore";
import { TotalSquareFootage } from "./store/MainStore";

export class EventBus {
    constructor() {
        
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