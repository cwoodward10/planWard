<script lang="ts">
    import { NumberWithCommas } from '$modules/utilities/StringUtilities';
    import { EmojiHappyIcon } from '@rgossiaux/svelte-heroicons/outline';

    import { createEventDispatcher, onDestroy } from 'svelte';
    import { tweened } from 'svelte/motion';

    export let title: string;
    export let data: any[];
    export let dataProperty: string | null = null;
    export let units: string;
    export let color: string = 'white';
    export let textColor: string = 'black';

    $: cssVarStyles = `
        --card-color:${color};
        --card-text-color:${textColor}
    `;

    /**
     * takes the input data and formats it per the input dataProperty
     * @param inputData
     */
    function setValue(inputData: any[], inputDataProperty: string | null) {
        if (!inputData) {
            return 0;
        }

        const useDataProperty = inputDataProperty !== null && 
            data.every(d => inputDataProperty in d) && 
            data.every(d => d[inputDataProperty])

        if (!inputData) {
            return 0;
        } else {
            return useDataProperty ? inputData.reduce((total: number, next) => total + next[inputDataProperty], 0) : data.length;
        }
    }

    // created as a store so that tweened can be used
    const value = tweened(0);
    $: {
        // reactively set the value as data changes
        value.set(setValue(data, dataProperty));
    };
    $: formattedValue = NumberWithCommas(Math.round($value));

    const eventDispatcher = createEventDispatcher();
    function handleClick() {
        eventDispatcher("click", title);
    }
</script>

<article class="bg-white w-full h-24 px-6 py-3 flex gap-3 align-middle rounded-lg shadow card-color cursor-pointer" 
    style="{cssVarStyles}"
    on:click="{handleClick}">
    <section class="flex w-16 flex-shrink-0 cursor-pointer">
        <slot name="icon">
            <EmojiHappyIcon class="mx-auto" />
        </slot>
    </section>
    <section class="h-full w-full flex flex-col justify-evenly">
        <h4 class="font-medium text-xs base:text-base cursor-pointer">{ title }</h4>
        <div class="flex space-x-2 align-baseline items-baseline">
            <h1 class="font-title font-bold text-2xl base:text-4xl">{ formattedValue }</h1>
            <p class="font-title font-light text-gray-700 whitespace-nowrap text-xs base:text-base">{units}</p>
        </div>
    </section>
</article>

<style>
    .card-color {
        color: var(--card-text-color);
        border-color: var(--card-color);
        border-style: solid;
        border-width: 2px;
    }
</style>