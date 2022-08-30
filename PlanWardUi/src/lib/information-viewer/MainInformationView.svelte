<script lang="ts">
  import ListBox from '$lib/component-library/ListBox.svelte';
  import OverallInformationCard from './helpers/OverallInformationCard.svelte';
  import BuildingIcon from '$lib/icons/BuildingIcon.svelte';
  import ParkingIcon from '$lib/icons/ParkingIcon.svelte';

	import { PlanWardBuildings, PlanWardParking } from '../../modules/store/MainStore';
	import { AllDesignOptions } from './../../modules/store/MainStore';

	import type { IBuilding } from './../../modules/data-models/IBuilding';

  $: parkingCount = $PlanWardParking.length;
  $: totalSF = Math.round($PlanWardBuildings.filter(b => b.Area != null && b.Area > 0).reduce((total: number, nextBld: IBuilding) => total + nextBld.Area, 0));

  const DESIGNOPTION_ALL_FILTER = 'All';
  let currentDesignOptionFilter = { id:0, value: DESIGNOPTION_ALL_FILTER};
  $: designOptionFilters = [DESIGNOPTION_ALL_FILTER, "Testing", "Testing Another"]
    .concat($AllDesignOptions)
    .map((optionName: string, index: number) => { return {id: index, value: optionName}} );

  function handleFilterChanged(event: any) {
    const newFilter = designOptionFilters.find(dof => dof.id === event.detail);
    if (newFilter) {
      currentDesignOptionFilter = newFilter;
    }
  }

</script>

<article class="w-full mx-auto flex flex-col space-y-6">
  <section class="w-full relative">
    <ListBox 
      options={designOptionFilters} 
      selected={currentDesignOptionFilter} 
      on:selectionChanged="{handleFilterChanged}" />
  </section>
  <OverallInformationCard 
    title={'Building Footprints'} 
    value={totalSF} 
    units={'Square Feet'} 
    color={'var(--pw-blue)'} 
    textColor={'black'}>
    <div slot="icon" class="w-full h-full flex mx-auto justify-center">
      <BuildingIcon strokeColor={'var(--pw-blue)'} fillColor={'var(--pw-blue)'} />
    </div>
  </OverallInformationCard>
  <OverallInformationCard 
    title={'Parking Stalls'} 
    value={parkingCount} 
    units={'Total Stalls'} 
    color={'var(--pw-yellow)'}
    textColor={'black'} >
    <div slot="icon" class="w-full h-full flex mx-auto justify-center">
      <ParkingIcon strokeColor={'var(--pw-yellow)'} fillColor={'var(--pw-yellow)'} />
    </div>
  </OverallInformationCard>
</article>