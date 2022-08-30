
<script lang="ts">
  import DesignOptionsForm from "$lib/attribute-editor/forms/DesignOptionsForm.svelte";

  import  { onDestroy } from 'svelte';
  
  import { SelectedRhinoObjects } from '../../modules/store/MainStore';

  import { GetCorrectAttributeEditorForm } from '$modules/application/AttributeFormHelpers';


  let formStateComponent = DesignOptionsForm; // start w/ the most basic form by default
  const unsubscribeSelectedRhinoObjects = SelectedRhinoObjects.subscribe((objects) => {
    formStateComponent = GetCorrectAttributeEditorForm(objects);
  })
  onDestroy(() => {
    unsubscribeSelectedRhinoObjects;
  })

</script>

<article class="w-full h-full overflow-hidden flex">
  {#if $SelectedRhinoObjects && $SelectedRhinoObjects.length > 0}
  <section class="w-full h-full p-3">
    <svelte:component this={formStateComponent} />
  </section>
  {:else}
  <section class="flex mx-auto p-3">No Objects Selected in Rhino</section>
  {/if}
</article>
