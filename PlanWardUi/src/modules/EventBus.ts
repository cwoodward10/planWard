import { ParkingCount } from "./store/MainStore";
import { TotalSquareFootage } from "./store/MainStore";

export class EventBus {
    constructor() {
        
    }

    UpdateParkingCount(newCount: number): void {
        ParkingCount.set(newCount);
    }
    
    UpdateTotalSquareFootage(newSF: number): void {
        TotalSquareFootage.set(newSF);
    }
}