<script lang="ts">
  import ListBox from '$lib/component-library/ListBox.svelte';
  import OverallInformationCard from './helpers/OverallInformationCard.svelte';
  import BuildingIcon from '$lib/icons/BuildingIcon.svelte';
  import ParkingIcon from '$lib/icons/ParkingIcon.svelte';

	import { PlanWardBuildings, PlanWardParking } from '../../modules/store/MainStore';
	import { AllDesignOptions } from './../../modules/store/MainStore';

	import type { IBuilding } from './../../modules/data-models/IBuilding';
  import type { IPlanWardObject } from '$modules/data-models/IPlanWardObject';
  import type { ComponentType } from 'svelte';

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
</script>

<article class="h-full w-full mx-auto flex flex-col space-y-6">
  <!-- Design Option Dropdown -->
  <section class="w-full relative">
    <ListBox 
      options={designOptionFilters} 
      selected={currentDesignOptionFilter} 
      on:selectionChanged="{handleFilterChanged}" />
  </section>
  <!-- Information Card Section -->
  <section class="relative w-full h-full flex flex-col space-y-6">
    {#each dataControllers as dataController}
    <OverallInformationCard 
      title={dataController.name} 
      data={dataController.data} 
      dataProperty={dataController.dataProperty}
      units={dataController.units} 
      color={dataController.color} 
      textColor={dataController.textColor}>
      <div slot="icon" class="w-full h-full flex mx-auto justify-center">
        <svelte:component this={dataController.icon} strokeColor={dataController.color} fillColor={dataController.color} />
      </div>
    </OverallInformationCard>
    {/each}
  </section>
</article>