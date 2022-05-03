<script lang="ts">
	import MainInformationView from './lib/information-viewer/MainInformationView.svelte';
	import type { PlanWardWindow } from './modules/PlanWardWindow';
	import { EventBus } from './modules/EventBus';
  import { onDestroy, onMount } from 'svelte';

  import TheHeader from './lib/TheHeader.svelte';
  import { ApplicationState } from './modules/store/MainStore';
  import { GetComponentFromAppState } from './modules/application/ApplicationStateHelpers';
  import TheAlert from '$lib/TheAlert.svelte';

  onMount(() => {
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

<main class="relative placeholder:mx-auto max-w-98 w-screen h-screen p-5 flex flex-col space-y-2 overflow-hidden object-contain">
  <div class="absolute w-full px-10 top-3 left-1/2 -translate-x-1/2 z-50">
    <TheAlert />
  </div>
  <TheHeader />
  <svelte:component this={appStateComponent}/>
</main>

<style>
  .max-w-98 {
    max-width: 98%;
  }
</style>