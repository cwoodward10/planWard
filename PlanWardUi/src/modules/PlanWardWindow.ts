import type { EventBus } from "./EventBus";

export interface PlanWardWindow extends Window {
    EventBus: EventBus;
}