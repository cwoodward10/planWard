import {expoInOut} from 'svelte/easing';
import type { TransitionConfig } from 'svelte/transition';

export function scaleIn(
    node: HTMLElement, 
    params: {
        delay: number, 
        duration: number,
        direction: "Vertical" | "Horizontal" | "Both"
    }
): TransitionConfig {
    return {
        delay: params.delay,
        duration: params.duration,
        css: (t: number, u: number) => {
            switch (params.direction) {
                case "Vertical":
                    return `transform: scaleY(${100 * t}%);`;
                case "Horizontal":
                    return `transform: scaleX(${100 * t}%);`;
                case "Both":
                    return `transform: scale(${100 * t}%, ${100 * t}%);`;
            }
        }
    }
}

export function drawer(
    node: HTMLElement, 
    params: {
        delay: number, 
        duration: number,
        orientation: "Vertical" | "Horizontal"
    }
): TransitionConfig {
    return {
        delay: params.delay,
        duration: params.duration,
        css: (t: number, u: number) => {
            switch (params.orientation) {
                case "Vertical":
                    return `max-height: ${100 * t}%;`;
                case "Horizontal":
                    return `max-width: ${100 * t}%;`;
            }
        }
    }
}