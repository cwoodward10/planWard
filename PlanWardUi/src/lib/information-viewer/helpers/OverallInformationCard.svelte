<script lang="ts">
    import { NumberWithCommas } from '$modules/utilities/StringUtilities';
    import { EmojiHappyIcon } from '@rgossiaux/svelte-heroicons/outline';
    import { createEventDispatcher, onDestroy } from 'svelte';

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

    function setValue(inputData: any[]) {
        if (!inputData) {
            return 0;
        }

        const useDataProperty = dataProperty !== null && 
            data.every(d => Object.hasOwn(d, dataProperty)) && 
            data.every(d => d[dataProperty])

        if (!inputData) {
            return 0;
        } else {
            return useDataProperty ? inputData.reduce((total: number, next) => total + next[dataProperty], 0) : data.length;
        }
    }
    $: value = setValue(data);
    $: formattedValue = NumberWithCommas(Math.round(value));

    const eventDispatcher = createEventDispatcher();
    function handleClick() {
        eventDispatcher("click", title);
    }
</script>

<article class="bg-white w-full h-24 px-6 py-3 flex gap-3 align-middle rounded-lg shadow card-color" 
    style="{cssVarStyles}"
    on:click="{handleClick}">
    <section class="flex w-16 flex-shrink-0">
        <slot name="icon">
            <EmojiHappyIcon class="mx-auto" />
        </slot>
    </section>
    <section class="h-full w-full flex flex-col justify-evenly">
        <h4 class="font-medium text-xs base:text-base">{ title }</h4>
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