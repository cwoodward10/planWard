<script lang="ts">
	import ListBox from '$lib/component-library/ListBox.svelte';
  import OverallInformationCard from './helpers/OverallInformationCard.svelte';
  import DetailInformationCard from './helpers/DetailInformationCard.svelte';
  import BuildingIcon from '$lib/icons/BuildingIcon.svelte';
  import ParkingIcon from '$lib/icons/ParkingIcon.svelte';

	import { PlanWardBuildings, PlanWardParking, AllDesignOptions } from '$modules/store/MainStore';
  import { CurrentDesignOptionFilter, FilterOption, DESIGNOPTION_ALL_FILTER } from '$modules/store/FilterHandler';
  
  import { drawer } from '$modules/application/CustomTransitions';
  import type { IAccountingObject } from '$modules/data-models/IAccountingObject';

  import { ComponentType, onMount } from 'svelte';

  let designOptionFilters: FilterOption[];
  $: designOptionFilters = [DESIGNOPTION_ALL_FILTER]
      .concat($AllDesignOptions)
      .sort((a: string, b: string) => a.localeCompare(b))
      .map((optionName: string, index: number) => { return {id: index, value: optionName}});

  function handleFilterChanged(event: any) {
    const newFilter = designOptionFilters.find(dof => dof.id === event.detail);
    if (newFilter) {
      CurrentDesignOptionFilter.set(newFilter);
    }
  }

  $: filteredBuildings = $PlanWardBuildings.filter(b => {
    return $CurrentDesignOptionFilter.id === 0 || $CurrentDesignOptionFilter.value === b.DesignOption;
  })
  $: filteredParking = $PlanWardParking.filter(p => {
    return $CurrentDesignOptionFilter.id === 0 || $CurrentDesignOptionFilter.value === p.DesignOption;
  })

  type  dataViewController = {
    name: string,
    data: IAccountingObject[],
    dataProperty: string | null,
    units: string,
    color: string,
    textColor: string,
    icon: ComponentType,
  }
  let dataViewControllers: dataViewController[]
  $: dataViewControllers = [
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
  let currentDataViewIndex: number | null = null;
  function handleInfoCardClick(e: any) {
    const cardName = e.detail;
    currentDataViewIndex = dataViewControllers.findIndex(dc => dc.name == cardName);
    showDetailView = true;
  }
  function handleInfoCardClose() {
    showDetailView = false;
  }

  let currentDataView: dataViewController | null;
  $: currentDataView = currentDataViewIndex == null ? null : dataViewControllers[currentDataViewIndex];

  let animationDuration = 0;
  let animationDelay = 0;
  onMount(() => {
    // use set timeout so that mounts with 0 delay to animation
    // but then has delay set for when clicks are registered later
    setTimeout(() => {
      animationDuration = 300;
      animationDelay = animationDuration * 1.5;
    }, 10);
  })
</script>

<article class="relative h-full w-full mx-auto flex flex-col space-y-6 overflow-hidden">
  <!-- Design Option Dropdown -->
  <section class="w-full relative z-20">
    <ListBox 
      options={designOptionFilters} 
      selected={$CurrentDesignOptionFilter} 
      on:selectionChanged="{handleFilterChanged}" />
  </section>
  <!-- Information Card Section -->
  <section class="relative w-full h-full flex">
    {#if showDetailView}
    <div 
      class="absolute top-0 left-0 w-full h-full overflow-hidden z-10"
      in:drawer={{duration: animationDuration, delay: animationDelay, orientation: "Vertical"}}
      out:drawer={{duration: animationDuration, delay: 0, orientation: "Vertical"}}>
      <DetailInformationCard 
        title={currentDataView.name} 
        data={currentDataView.data} 
        dataProperty={currentDataView.dataProperty}
        color={currentDataView.color}
        on:close={handleInfoCardClose}/>
    </div>
    {:else}
    <div 
      class="absolute top-0 left-0 w-full h-full overflow-hidden flex flex-col space-y-6" 
      in:drawer={{duration: animationDuration, delay: animationDelay, orientation: "Vertical"}}
      out:drawer={{duration: animationDuration, delay: 0, orientation: "Vertical"}}>
      {#each dataViewControllers as dataController, i}
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
      {/each}
    </div>
    {/if}
  </section>
</article>