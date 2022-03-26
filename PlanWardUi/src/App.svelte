<script lang="ts">
	import MainInformationView from './lib/information-viewer/MainInformationView.svelte';
	import type { PlanWardWindow } from './modules/PlanWardWindow';
	import { EventBus } from './modules/EventBus';
  import { onDestroy, onMount } from 'svelte';

  import TheHeader from './lib/TheHeader.svelte';
  import { ApplicationState } from './modules/store/MainStore';
  import { GetComponentFromAppState } from './modules/application/ApplicationStateHelpers';

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

<main class="mx-auto max-w-98 w-screen h-screen p-5 flex flex-col space-y-2 overflow-hidden object-contain">
  <TheHeader />
  <svelte:component this={appStateComponent}/>
</main>

<style>
  .max-w-98 {
    max-width: 98%;
  }
</style>