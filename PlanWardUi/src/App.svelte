<script lang="ts">
	import type { PlanWardWindow } from './modules/PlanWardWindow';
	import { EventBus } from './modules/EventBus';
  import { onMount } from 'svelte';

  import logo from './assets/svelte.png'
  import Counter from './lib/Counter.svelte'

  onMount(() => {
    (window as unknown as PlanWardWindow).EventBus = new EventBus();
  })
</script>

<main class="w-screen h-screen p-3 flex flex-col space-y-2">
  <header class="mx-auto flex">
    <h1 class="text-center text-xl font-medium text-gray-800">PlanWard</h1>
  </header>
  <Counter />
</main>