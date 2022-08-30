<script lang="ts">
import {
    CheckCircleIcon,
    ExclamationIcon,
    ExclamationCircleIcon,
    XCircleIcon
} from "@rgossiaux/svelte-heroicons/solid";

import {
    onDestroy
} from "svelte";

import {
    ApplicationAlertMessage,
    ApplicationAlertType,
    ApplicationAlertTimeout
} from "$modules/store/MainStore";

let showAlert = false;
const unSubMessage = ApplicationAlertMessage.subscribe(message => {
    if (message !== "") {
        showAlert = true;
        if ($ApplicationAlertTimeout != 0) {
            setTimeout(() => {
                showAlert = false;
            }, $ApplicationAlertTimeout);
        }
    }
})

onDestroy(() => {
    unSubMessage;
})

function handleClose() {
    showAlert = false;
}
</script>

<template>
    {#if showAlert === true}
    <div
        class="h-15 flex justify-between p-2 drop-shadow-lg z-50 rounded-md overflow-hidden"
        class:success={$ApplicationAlertType === "Success"}
        class:warning={$ApplicationAlertType === "Warning"}
        class:error={$ApplicationAlertType === "Error"}
        >
        <span class="flex">
            {#if $ApplicationAlertType === "Success"}
            <CheckCircleIcon class="text-white h-5 w-5" />
            {:else if  $ApplicationAlertType === "Warning"}
            <ExclamationIcon class="text-white h-5 w-5" />
            {:else}
            <ExclamationCircleIcon class="text-white h-5 w-5" />
            {/if}
        </span>
        <p class="flex flex-grow-0 text-center font-medium mx-auto truncate text-white">{ $ApplicationAlertMessage }</p>
        <span class="flex col-start-7 cursor-pointer" on:click="{handleClose}">
            <XCircleIcon class="ml-auto mr-0 text-white h-5 w-5" />
        </span>
    </div>
    {/if}
</template>

<style>
.success {
    @apply border-2 border-solid border-green-600;
    @apply bg-green-400;
}

.warning {
    @apply border-2 border-solid border-orange-600;
    @apply bg-orange-400;
}

.error {
    @apply border-2 border-solid border-red-600;
    @apply bg-red-400;
}
</style>
