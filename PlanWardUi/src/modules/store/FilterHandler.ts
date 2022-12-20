import { get, writable } from "svelte/store";

import type { IAccountingObject } from '../data-models/IAccountingObject';
import type { DataStructure } from '../chart-utilities/ChartUtilities';

/** Standard default design option filter option to show all design options */
export const DESIGNOPTION_ALL_FILTER = 'All';

/** Type for each filter option */
export type FilterOption = { id: number, value: string };

/** Filter Option describing the currently selected Design Option */
export const CurrentDesignOptionFilter = writable<FilterOption>({id: 0, value: DESIGNOPTION_ALL_FILTER});