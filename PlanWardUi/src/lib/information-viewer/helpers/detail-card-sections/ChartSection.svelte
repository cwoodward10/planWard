<script lang="ts">
    import { onMount, createEventDispatcher } from "svelte";
    import { Chart, InteractionItem, registerables } from "chart.js";
    
	import type { DataStructure } from '$modules/chart-utilities/ChartUtilities';

    export let data: DataStructure[];

    let chart: Chart;
    let config: any;
    let chartCanvas: HTMLCanvasElement;

    const eventDispatch = createEventDispatcher();

    /**
     * Createst the configuration for the chart
     * @param data
     * @param chartType
     * @param legendPosition
     * @param onChartClick
     * @param rotation
     * @param isResponsive
     */
    function CreateChartConfig(
        data: DataStructure[], 
        chartType: string,
        onChartClick: (e: Event) => void = (e: Event) => {},
        rotation: number = 0,
        isResponsive: boolean = true,
    ) {
        // combine all labels of datasets for use in legend
        const allLabels = data.reduce((labels: string[], nextData: DataStructure) => {
            return labels.concat(nextData.labels);
        }, [])

        return {
            type: chartType,
            data: {
                datasets: data,
                labels: allLabels
            },
            options: {
                rotation: rotation,
                responsive: isResponsive,
                onClick: onChartClick,
                plugins: {},
            },
        }
    }

    /**
     * Creates a legend for the Chart. 
     * Requires the chart and the config to have been already created globally in order to work
     * @param legendPosition
     * @param legendAlign
     * @param onLegendClick
     */
    function createChartLegend(
        legendPosition: "left" | "right" | "top" | "bottom",
        legendAlign: "start" | "end" | "center",
        onLegendClick: (e: Event, legendItem: any) => void = (e, legendItem) => {}
    ) {
        return {
            display: true,
            position:legendPosition,
            align: legendAlign,
            onClick: onLegendClick,
            labels: {
                generateLabels: () => {
                    let labels = [];
                    config.data.datasets.forEach((ds: any, iDs: any) => labels = labels.concat(ds.labels.map((l, iLabel) => ({
                        datasetIndex: iDs,
                        labelIndex: iLabel,
                        text: l,
                        fillStyle: ds.backgroundColor[iLabel],
                        hidden: chart ? (chart.getDatasetMeta(iDs).data[iLabel] as any).hidden : false,
                        //strokeStyle: '#fff'
                    }))));
                    return labels;
                }
            }
        }
    }

    /**
     * Takes a click event from the chart and then uses that to set which element has been clicked on
     * also dispatches an event to any parent component that wants to use it.
    */
    const handleChartClick = (e: Event) => {
        const clickedElements = chart.getElementsAtEventForMode(e, 'nearest', { intersect: true }, true);
        if (clickedElements.length === 0) {
            return;
        }
        
        const clickedElement: InteractionItem = clickedElements[0];

        eventDispatch("chartClick", { 
            datasetIndex: clickedElement.datasetIndex, 
            dataItemIndex: clickedElement.index
        })
    }

    // reactively update the config file on data and chartType changes
    $: {
        if (chart) {
            const config = CreateChartConfig(data, "doughnut", handleChartClick);

            const updatedData = config.data;    

            chart.data = updatedData;
            chart.update();
        }
    }
    onMount(() => {
        if (chartCanvas) {
            Chart.register(...registerables);

            config = CreateChartConfig(data, "doughnut", handleChartClick);
            config.options.plugins.legend = createChartLegend("bottom", "center");

            chart = new Chart(chartCanvas, config);
        }
    })
</script>

<div id="chart-container">
    <canvas bind:this={chartCanvas} />
</div>