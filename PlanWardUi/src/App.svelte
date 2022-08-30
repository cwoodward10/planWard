<script lang="ts">
  import TheHeader from './lib/TheHeader.svelte';
	import MainInformationView from './lib/information-viewer/MainInformationView.svelte';
  import TheAlert from '$lib/TheAlert.svelte';

  import { onDestroy, onMount } from 'svelte';

  import { ApplicationState } from './modules/store/MainStore';
  import { AppStateEnum, GetComponentFromAppState } from './modules/application/ApplicationStateHelpers';

	import type { PlanWardWindow } from './modules/PlanWardWindow';
  
	import { EventBus } from './modules/EventBus';


  onMount(() => {
    ApplicationState.set(AppStateEnum.MainInformation);

    const pwWindow = (window as unknown as PlanWardWindow);
    pwWindow.EventBus = new EventBus();
    
    if (pwWindow.Interop) {
      pwWindow.Interop.refreshInformation();
    }
  })
  
  let appStateComponent = MainInformationView;
  const unsubscribeStateTracking = ApplicationState.subscribe((state) => {
    appStateComponent = GetComponentFromAppState(state);
  })
  onDestroy(() => {
    unsubscribeStateTracking;
  })
</script>

<main class="relative mx-auto max-w-98 w-screen h-screen p-5 flex flex-col gap-y-4 overflow-hidden object-contain">
  <div class="absolute w-full px-10 top-3 left-1/2 -translate-x-1/2 z-50">
    <TheAlert />
  </div>
  <TheHeader />
  <div class="w-full h-full">
    <svelte:component this={appStateComponent}/>
  </div>
</main>

<style>
  .max-w-98 {
    max-width: 98%;
  }
</style>