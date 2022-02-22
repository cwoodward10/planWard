import { writable } from "svelte/store";

/**
 * Global parking count for the active rhino doc
 */
export const ParkingCount = writable(0);

/**
 * Global Total Square Footage for the active rhino do
 */
export const TotalSquareFootage = writable(0);
