<script lang="ts">
    import { EmojiHappyIcon } from '@rgossiaux/svelte-heroicons/outline';

    export let title: string;
    export let value: number;
    export let units: string;
    export let color: string = 'white';
    export let textColor: string = 'black';

    $: cssVarStyles = `
        --card-color:${color};
        --card-text-color:${textColor}
    `;

    $: unitStrings = units.split(' ');
</script>

<article class="w-full px-6 py-3 grid grid-cols-12 gap-3 align-middle rounded-lg shadow card-color" 
    style="{cssVarStyles}">
    <section class="flex col-span-2">
        <slot name="icon">
            <EmojiHappyIcon class="mx-auto" />
        </slot>
    </section>
    <section class="h-full col-span-10 flex flex-col justify-evenly">
        <h4 class="font-medium">{ title }</h4>
        <div class="flex space-x-2 align-baseline items-baseline">
            <h1 class="font-title font-bold text-4xl">{ value }</h1>
            <p class="font-title font-light text-gray-700">{units}</p>
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