<script lang="ts">
  import TheHeader from '$lib/TheHeader.svelte';
	import MainInformationView from '$lib/information-viewer/MainInformationView.svelte';
  import TheAlert from '$lib/TheAlert.svelte';

  import { onDestroy, onMount } from 'svelte';

  import { AllPlanWardObjects, ApplicationState } from '$modules/store/MainStore';
  import { AppStateEnum, GetComponentFromAppState } from '$modules/application/ApplicationStateHelpers';

	import type { PlanWardWindow } from '$modules/PlanWardWindow';
  
	import { EventBus } from '$modules/EventBus';
  import { DevConstants } from '$modules/dev-constants/DevConstants';


  onMount(() => {
    ApplicationState.set(AppStateEnum.MainInformation);

    const pwWindow = (window as unknown as PlanWardWindow);
    pwWindow.EventBus = new EventBus();
    
    if (pwWindow && "Interop" in pwWindow) {
      pwWindow.Interop.refreshInformation();
    } else {
      AllPlanWardObjects.set(DevConstants);
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

<main class="app-main m-auto p-5 flex flex-col gap-y-4 overflow-hidden object-contain">
  <div class="app-container h-full w-full m-auto relative flex flex-col gap-y-4 overflow-hidden object-contain">
    <div class="absolute w-full px-10 top-3 left-1/2 -translate-x-1/2 z-50">
      <TheAlert />
    </div>
    <TheHeader />
    <div class="w-full h-full">
      <svelte:component this={appStateComponent}/>
    </div>
  </div>
</main>

<style>
  .app-main {
    background-color: white;
    max-width: 650px;
    width: 100vw;
    height: 100vh;
  }
  .app-container {
    max-width: 525px;
  }

  @media (min-width: 650px) {
    .app-main {
      border: var(--gray) solid 1px;
    }
  }
</style>