
<script lang="ts">
  import { SelectedRhinoObjects } from '../../modules/store/MainStore';
  import DesignOptionsForm from "$lib/attribute-editor/forms/DesignOptionsForm.svelte";
  import { GetCorrectAttributeEditorForm } from '$modules/application/AttributeFormHelpers';
  import  { onDestroy } from 'svelte';


  let formStateComponent = DesignOptionsForm; // start w/ the most basic form by default
  const unsubscribeSelectedRhinoObjects = SelectedRhinoObjects.subscribe((objects) => {
    formStateComponent = GetCorrectAttributeEditorForm(objects);
  })
  onDestroy(() => {
    unsubscribeSelectedRhinoObjects;
  })

</script>

<article class="w-full h-full overflow-hidden">
  {#if $SelectedRhinoObjects && $SelectedRhinoObjects.length > 0}
  <section class="w-full h-full p-3">
    <svelte:component this={formStateComponent} />
  </section>
  {:else}
  <section class="flex mx-auto p-3">There are not Objects Selected in Rhino</section>
  {/if}
</article>
