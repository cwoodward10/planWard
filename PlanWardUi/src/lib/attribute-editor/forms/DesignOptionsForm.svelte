<script lang="ts">
	import type { IPlanWardObject } from './../../../modules/data-models/IPlanWardObject';
  import type { PlanWardWindow } from '$modules/PlanWardWindow';
  import { createForm } from "svelte-forms-lib";
  import * as yup from 'yup';
  import { SelectedRhinoObjects } from '$modules/store/MainStore';
  import { FORM_MULTIPLE_VALUES_STRING } from '$modules/application/AttributeFormHelpers';
  import { onMount } from 'svelte';

  function SetFormText(objects: IPlanWardObject[]) {
    if (objects.length > 0 && objects.some(o => o.DesignOption != null)) {
      const firstOption = objects[0].DesignOption;
      const designOptionValuesSame = objects.every(o => o.DesignOption === firstOption);
      valueText = designOptionValuesSame ? firstOption : null;
      placeholderText = designOptionValuesSame ? null : FORM_MULTIPLE_VALUES_STRING;
    }
  }

  // set the initial value or content if exists
  let placeholderText: string | null = "Set a Design Option Name"
  let valueText: string | null = null;

  // update form values if 
  const unsubscribeRhinoSelection = SelectedRhinoObjects.subscribe((objects) => {
    SetFormText(objects);
    if ($form) {
      form.set({
        designOption: placeholderText
      })
    }
  })
  onMount(() => {
    unsubscribeRhinoSelection;
  })


  // create the form
  let pwWindow = window as unknown as PlanWardWindow;
  const {
    // observables state
    form,
    errors,
    state,
    touched,
    isValid,
    isSubmitting,
    isValidating,
    // handlers
    handleChange,
    handleSubmit
  } = createForm({
    initialValues: {
      designOption: placeholderText,
    },
    validationSchema: yup.object().shape({
      designOption: yup.string().not([FORM_MULTIPLE_VALUES_STRING]).required(),
    }),
    onSubmit: values => {
      const updatedObjects = $SelectedRhinoObjects.map(obj => {
        obj.DesignOption = values.designOption;
        return obj;
      });
      console.log("submitting", updatedObjects);
      const data = JSON.stringify(updatedObjects);
      return pwWindow.Interop.updateObjectUserDictionary(data);
    }
  });
</script>

<form class="flex flex-col space-y-3 w-full h-full" class:valid={$isValid} on:submit={handleSubmit}>
  <div class="flex flex-col space-y-1">
    <label class="flex" for="designOption">Design Option Name</label>
    <input 
      class="flex w-full" 
      name="designOption" 
      value={valueText} 
      placeholder={$form.designOption} 
      on:keyup={handleChange} 
    />
    {#if $errors.designOption && $touched.designOption}
      <small>{$errors.designOption}</small>
    {/if}
  </div>

  <button class="flex" type="submit" disabled={!$isValid}>
    {#if $isSubmitting}Set Rhino Attributes...{:else}submit{/if}
  </button>
</form>