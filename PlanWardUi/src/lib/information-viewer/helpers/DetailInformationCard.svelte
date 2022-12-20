<script lang="ts">
	import { XIcon } from "@rgossiaux/svelte-heroicons/outline";
    import ChartSection from "./detail-card-sections/ChartSection.svelte";
    
    import { createEventDispatcher} from 'svelte';
    
	import { ConvertToDataStructure, DataStructure } from '$modules/chart-utilities/ChartUtilities';
    import type { IAccountingObject } from "$modules/data-models/IAccountingObject";
    import { CurrentDesignOptionFilter } from "$modules/store/FilterHandler";


    export let title: string;
    export let data: IAccountingObject[];
    export let dataProperty: string | null;
    export let color: string = "black";

    $: cssStyleVar = `--detail-card-color: ${color}`;

    const eventDispatcher = createEventDispatcher();
    function handleCloseEvent() {
        eventDispatcher('close');
    }

    let localDesignOptionFilter = ""
    let regionNameFilter = "";
    let regionIdentifierFilter = "";

    function createFilteredDatasets(
            inputData: IAccountingObject[],
            inputGlobalDesignOptionFilter: string,
            inputLocalDesignOptionFilter: string,
            inputRegionNameFilter: string,
            inputRegionIdentifierFilter: string
        ): DataStructure[] {
        const datasets: DataStructure[] = [];

        // check for filters
        const hasGlobalDesignOptionFilter = inputGlobalDesignOptionFilter !== "";
        const hasLocalDesignOptionFilter = hasGlobalDesignOptionFilter || inputLocalDesignOptionFilter !== "";
        const hasRegionNameFilter = (hasGlobalDesignOptionFilter || hasLocalDesignOptionFilter) && inputRegionNameFilter !== "";
        const hasRegionIdentifierFilter = hasRegionNameFilter && inputRegionIdentifierFilter !== "";

        // set actual filter b/c could have case where local is not set but global sets it by default
        let actualDesignOptionFilter = hasLocalDesignOptionFilter && inputLocalDesignOptionFilter === "" ? 
                                        inputGlobalDesignOptionFilter :
                                        inputLocalDesignOptionFilter;

        // set design option b/c it's filter is controlled by the app's state
        const designOptionDataset = ConvertToDataStructure(
            "Design Options",
            inputData, 
            "DesignOption", 
            dataProperty, 
            ["#45dd6b"], 
        );
        const designOptionBorderWidths = designOptionDataset.labels.map(l => l === actualDesignOptionFilter ? 2 : 0);
        designOptionDataset.borderWidth = designOptionBorderWidths;
        datasets.push(designOptionDataset);

        // check if we are zooming in on a particular local design option filter
        if (hasLocalDesignOptionFilter) {
            const byLocalDesignOptionFilter = inputData.filter(d => d.DesignOption === actualDesignOptionFilter);
            // then zoom in to that local filter by adding a data set organized by region name
            if (byLocalDesignOptionFilter.length > 0) {
                const regionNameDataStructure = ConvertToDataStructure(
                    "Region Name",
                    byLocalDesignOptionFilter, 
                    "RegionName", 
                    dataProperty, 
                    ["#ffff6b"]
                );
                const regionNameBorderWidths = regionNameDataStructure.labels.map(l => l === inputRegionNameFilter ? 2 : 0);
                regionNameDataStructure.borderWidth = regionNameBorderWidths;
                datasets.push(regionNameDataStructure);

                // then check if we are zooming in further to a region name
                if (hasRegionNameFilter) {
                    const byRegionNameFilter = byLocalDesignOptionFilter.filter(d => d.RegionName === inputRegionNameFilter);
                    // then zoom in to that region name by adding a data set organized by the region identifiers
                    if (byRegionNameFilter.length > 0) {
                        const regionIdentifierDataStructure = ConvertToDataStructure(
                            "Region Identifier",
                            byRegionNameFilter, 
                            "RegionIdentifier", 
                            dataProperty, 
                            ["#ff46a5"]
                        );
                        const regionIdentifierBorderWidths = regionIdentifierDataStructure.labels.map(l => l === inputRegionIdentifierFilter ? 2 : 0);
                        regionIdentifierDataStructure.borderWidth = regionIdentifierBorderWidths;
                        datasets.push(regionIdentifierDataStructure);
                    }
                }
            }
        }

        // return our data sets
        return datasets;
    }
    $: filteredData = createFilteredDatasets(
            data, 
            $CurrentDesignOptionFilter.value, 
            localDesignOptionFilter, 
            regionNameFilter, 
            regionIdentifierFilter
        );

    function handleChartClick(event: any) {
        const eventDetail: {datasetIndex: number, dataItemIndex: number} = event.detail;

        if (eventDetail == null ||
            filteredData[eventDetail.datasetIndex] == null || 
            filteredData[eventDetail.datasetIndex].data[eventDetail.dataItemIndex] == null) {
                return;
            }
        
        // handle dataset click first
        const selectedDatasetItem = filteredData[eventDetail.datasetIndex].labels[eventDetail.dataItemIndex];
        switch (eventDetail.datasetIndex) {
            // selected design option data set
            case 0:
                localDesignOptionFilter = localDesignOptionFilter === selectedDatasetItem ? "" : selectedDatasetItem;
                break;
            // selected region name data set
            case 1:
                regionNameFilter = regionNameFilter === selectedDatasetItem ? "" : selectedDatasetItem;
                break;
            // selected region identifier data set
            case 2:
                regionIdentifierFilter = regionIdentifierFilter === selectedDatasetItem ? "" : selectedDatasetItem;
                break;
        }
    }
</script>

<article class="card-container bg-white h-full w-full flex flex-col space-y-6 rounded-lg p-5" style="{cssStyleVar}">
    <!-- Header Section -->
    <header class="flex flex-row justify-between">
        <h1 class="title font-title font-medium text-lg">{title}</h1>
        <XIcon class="text-pw-red h-5 w-5 cursor-pointer" on:click={handleCloseEvent}/>
    </header>
    <!-- chart section -->
    <section>
        <ChartSection data={filteredData} on:chartClick={handleChartClick}/>
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