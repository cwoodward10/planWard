<script lang="ts">
    import { XIcon } from "@rgossiaux/svelte-heroicons/outline";
    
    import { createEventDispatcher, onMount} from 'svelte';
    import { Chart, registerables } from "chart.js";
    
	import { ConvertToDataStructure, CreateChartConfig, DataStructure } from '$modules/chart-utilities/ChartUtilities';
    import type { IPlanWardObject } from "$modules/data-models/IPlanWardObject";


    export let title: string;
    export let data: IPlanWardObject[];
    export let dataProperty: string | null;
    export let color: string = "black";

    $: cssStyleVar = `--detail-card-color: ${color}`;

    $: filteredData = ConvertToDataStructure(data, dataProperty);

    let chart: Chart;
    let chartCanvas: HTMLCanvasElement;
    // reactively update the config file on data and chartType changes
    $: {
        if (chart) {
            chart.data.datasets = [filteredData];
            chart.update();
        }
    }
    onMount(() => {
        if (chartCanvas) {
            Chart.register(...registerables);
            let config: any = CreateChartConfig([filteredData], "doughnut", "left");
            chart = new Chart(chartCanvas, config);
        }
    })

    const eventDispatcher = createEventDispatcher();
    function handleCloseEvent() {
        eventDispatcher('close');
    }
</script>

<article class="card-container bg-white h-full w-full flex flex-col space-y-6 rounded-lg p-5" style="{cssStyleVar}">
    <!-- Header Section -->
    <header class="flex flex-row justify-between">
        <h1 class="title font-title font-medium text-lg">{title}</h1>
        <XIcon class="text-pw-red h-5 w-5" on:click={handleCloseEvent}/>
    </header>
    <!-- chart section -->
    <section>
        <div id="chart-container">
            <canvas bind:this={chartCanvas} />
        </div>
    </section>
    <!-- filter section -->
    <section></section>
    <!-- table section -->
    <section></section>
</article>

<style>
    .card-container {
        border: var(--detail-card-color) solid 2px;
    }
    .title {
        color: var(--detail-card-color);
    }
</style>