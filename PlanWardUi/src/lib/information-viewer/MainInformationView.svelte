<script lang="ts">
  import ListBox from '$lib/component-library/ListBox.svelte';
  import OverallInformationCard from './helpers/OverallInformationCard.svelte';
  import DetailInformationCard from './helpers/DetailInformationCard.svelte';
  import BuildingIcon from '$lib/icons/BuildingIcon.svelte';
  import ParkingIcon from '$lib/icons/ParkingIcon.svelte';

	import { PlanWardBuildings, PlanWardParking } from '$modules/store/MainStore';
	import { AllDesignOptions } from '$modules/store/MainStore';
  import type { IPlanWardObject } from '$modules/data-models/IPlanWardObject';

  import type { ComponentType } from 'svelte';
  import { expoInOut } from 'svelte/easing';
  import { fade } from 'svelte/transition';

  const DESIGNOPTION_ALL_FILTER = 'All';
  let currentDesignOptionFilter = { id:0, value: DESIGNOPTION_ALL_FILTER};
  $: designOptionFilters = [DESIGNOPTION_ALL_FILTER]
    .concat($AllDesignOptions)
    .map((optionName: string, index: number) => { return {id: index, value: optionName}} );

  function handleFilterChanged(event: any) {
    const newFilter = designOptionFilters.find(dof => dof.id === event.detail);
    if (newFilter) {
      currentDesignOptionFilter = newFilter;
    }
  }

  $: filteredBuildings = $PlanWardBuildings.filter(b => {
    return currentDesignOptionFilter.id === 0 || currentDesignOptionFilter.value === b.DesignOption;
  })
  $: filteredParking = $PlanWardParking.filter(p => {
    return currentDesignOptionFilter.id === 0 || currentDesignOptionFilter.value === p.DesignOption;
  })

  type  dataStructure = {
    name: string,
    data: IPlanWardObject[],
    dataProperty: string | null,
    units: string,
    color: string,
    textColor: string,
    icon: ComponentType,
  }
  let dataControllers: dataStructure[]
  $: dataControllers = [
    {
      name: "Building Footprints",
      data: filteredBuildings,
      dataProperty: "Area",
      units: "Square Feet",
      color: "var(--pw-blue)",
      textColor: "black",
      icon: BuildingIcon,
    },
    {
      name: "Parking Stalls",
      data: filteredParking,
      dataProperty: null,
      units: "Total Stalls",
      color: "var(--pw-yellow)",
      textColor: "black",
      icon: ParkingIcon
    }
  ]
  
  let showDetailView: boolean = false;
  let currentDataView: dataStructure | null = null;
  function handleInfoCardClick(e: any) {
    const cardName = e.detail;
    currentDataView = dataControllers.find(dc => dc.name == cardName);
    showDetailView = true;
  }
  function handleInfoCardClose() {
    showDetailView = false;
  }

  const TRANSITION_DURATION = 1000;
  function getInfoCardTransitionCss(t: number, u: number, topStart: number): string {
    return `transform: translateY(-${50 * u}%)`;
  }
  function getInfoCardTransitionTick(t: number, u: number, topStart: number, node: HTMLElement) {
    node.style.top = `calc(${50 * u}% + ${topStart * t}px)`;
  }
  let overallTopStart = 0;
  function infoCardTransition(node: HTMLElement, params: {
    cardTitle: string,
    topStart: number,
    inOut: "in" | "out",
  }) {
    if (!currentDataView || currentDataView.name !== params.cardTitle) {
      return params.inOut === "out" ? 
        fade(node, {duration: TRANSITION_DURATION}) : 
        fade(node, {duration: TRANSITION_DURATION, delay: 1000});
    } else {
      overallTopStart = params.topStart;
      return {
        delay: params.inOut === "in" ? TRANSITION_DURATION : 0,
        duration: TRANSITION_DURATION,
        easing: expoInOut,
        css: (t: number, u: number) => getInfoCardTransitionCss(t, u, params.topStart),
        tick: (t: number, u: number) => getInfoCardTransitionTick(t, u, params.topStart, node),
      }
    }
  }
  function getDetailCardTransitionCss(t: number, u: number, topEnd: number, inOut: "in" | "out"): string {
    const height = 100 * t;
    const heightString = `min-height: 96px; height: ${height}%;`;

    const oNone = 'opacity: 0;';
    const oFull = 'opacity: 100%;';
    let opacityString = inOut === "in" ? oNone : oFull;
    if (t > 0.00001) {
      opacityString = oFull;
    }

    return heightString + opacityString;
  }
  function detailCardTransition(node: HTMLElement, params: {inOut: "in" | "out"}) {
    return {
      delay: params.inOut === "out" ? 0 : TRANSITION_DURATION,
      duration: TRANSITION_DURATION,
      easing: expoInOut,
      css: (t: number, u: number) => getDetailCardTransitionCss(t, u, overallTopStart, params.inOut)
    }
  }
</script>

<article class="relative h-full w-full mx-auto flex flex-col space-y-6">
  <!-- Design Option Dropdown -->
  <section class="w-full relative">
    <ListBox 
      options={designOptionFilters} 
      selected={currentDesignOptionFilter} 
      on:selectionChanged="{handleFilterChanged}" />
  </section>
  <!-- Information Card Section -->
  <section class="relative w-full h-full flex flex-col">
    {#if showDetailView}
    <div 
      class="absolute w-full h-full max-h-full overflow-hidden top-1/2 left-0 -translate-y-1/2 z-10" 
      in:detailCardTransition="{{inOut: "in"}}" 
      out:detailCardTransition="{{inOut: "out"}}">
      <DetailInformationCard 
        title={currentDataView.name} 
        data={currentDataView.data} 
        color={currentDataView.color}
        on:close={handleInfoCardClose}/>
    </div>
    {:else}
    {#each dataControllers as dataController, i}
    <div 
      class="absolute flex w-full" 
      style={`top: ${i * 116}px; left: 0;`}
      in:infoCardTransition="{{cardTitle: dataController.name, topStart: i * 116, inOut: "in"}}"
      out:infoCardTransition="{{cardTitle: dataController.name, topStart: i * 116, inOut: "out"}}">
      <OverallInformationCard 
        title={dataController.name} 
        data={dataController.data} 
        dataProperty={dataController.dataProperty}
        units={dataController.units} 
        color={dataController.color} 
        textColor={dataController.textColor}
        on:click={handleInfoCardClick}>
        <div slot="icon" class="w-full h-full flex mx-auto justify-center">
          <svelte:component this={dataController.icon} strokeColor={dataController.color} fillColor={dataController.color} />
        </div>
      </OverallInformationCard>
    </div>
    {/each}
    {/if}
  </section>
</article>