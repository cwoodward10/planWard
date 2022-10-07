<script lang="ts">
import { CogIcon, XIcon } from '@rgossiaux/svelte-heroicons/outline';

import { AppStateEnum } from '../modules/application/ApplicationStateHelpers';
import { ApplicationState } from './../modules/store/MainStore';

function setAppState(state: AppStateEnum) {
    ApplicationState.set(state);
}

let showSettingsMenu = false;
</script>

<header class="mx-auto w-full grid grid-cols-12 align-middle">
    <h1 class="col-span-3 text-xl font-medium text-gray-800">PlanWard</h1>
    <div class="col-span-6 w-full relative mx-auto grid grid-cols-2 rounded-full bg-white">
        <button
            class="flex w-full py-1 text-center text-sm z-20"
            class:text-white="{$ApplicationState === AppStateEnum.MainInformation}"
            on:click={() => setAppState(AppStateEnum.MainInformation)}
            >
            <span class="mx-auto">Home</span>
        </button>
        <button
            class="flex w-full py-1 text-center text-sm z-20"
            class:text-white="{$ApplicationState === AppStateEnum.MainAttributeEditor}"
            on:click={() => setAppState(AppStateEnum.MainAttributeEditor)}

            >
            <span class="mx-auto">Editor</span>
        </button>
        <div 
            class="absolute h-full w-1/2 bg-pw-navy rounded-full move-transition"
            class:translate-x-0={$ApplicationState === AppStateEnum.MainInformation}
            class:translate-x-full={$ApplicationState === AppStateEnum.MainAttributeEditor}
        />
    </div>
    <div class="col-span-3 flex justify-self-end">
        <button
            class="h-full flex bg-white"
            >
            {#if !showSettingsMenu }
            <CogIcon class="flex m-auto h-5 text-gray-600 material-icons-outlined" on:click={() => showSettingsMenu = true} />
            {:else}
            <XIcon class="flex m-auto h-5 text-pw-red material-icons-outlined" on:click={() => showSettingsMenu = false} />
            {/if}
        </button>
    </div>
</header>

<style>
    .move-transition {
        transition: transform 200ms;
    }
</style>