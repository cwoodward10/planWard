<script lang="ts">
    import { createEventDispatcher} from 'svelte';

    import { ArrowLeftIcon } from "@rgossiaux/svelte-heroicons/outline"

    export let title: string;
    export let data: any[];
    export let color: string = "black";

    $: cssStyleVar = `--detail-card-color: ${color}`;

    const eventDispatcher = createEventDispatcher();
    function handleCloseEvent() {
        eventDispatcher('close');
    }
</script>

<article class="card-container h-full w-full flex flex-col rounded-lg p-5" style="{cssStyleVar}">
    <section class="flex flex-row justify-between">
        <h1 class="title font-title font-medium text-lg">{title}</h1>
        <ArrowLeftIcon class="text-pw-red h-5 w-5" on:click={handleCloseEvent}/>
    </section>
</article>

<style>
    .card-container {
        border: var(--detail-card-color) solid 2px;
    }
    .title {
        color: var(--detail-card-color);
    }
</style>