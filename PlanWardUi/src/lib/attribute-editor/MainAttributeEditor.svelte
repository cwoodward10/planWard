
<script lang="ts">
  import type { PlanWardWindow } from '$modules/PlanWardWindow';
  import { createForm } from "svelte-forms-lib";
  import * as yup from 'yup';
  import { SelectedRhinoObjects } from '../../modules/store/MainStore';

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
      name: "",
      email: ""
    },
    validationSchema: yup.object().shape({
      name: yup.string().required(),
      email: yup
        .string()
        .email()
        .required()
    }),
    onSubmit: values => {
      return pwWindow.Interop.updateObjectUserDictionary.then(() => {
        alert(JSON.stringify(values, null, 2));
      });
    }
  });
</script>

{#if $SelectedRhinoObjects && $SelectedRhinoObjects.length > 0}
<form class:valid={$isValid} on:submit={handleSubmit}>
  <label for="name">name</label>
  <input name="name" on:keyup={handleChange} />
  {#if $errors.name && $touched.name}
    <small>{$errors.name}</small>
  {/if}

  <label for="email">email</label>
  <input name="email" on:keyup={handleChange} />
  {#if $errors.email && $touched.email}
    <small>{$errors.email}</small>
  {/if}

  <button type="submit" disabled={!$isValid}>
    {#if $isSubmitting}loading...{:else}submit{/if}
  </button>
</form>
{:else}
<span class="flex mx-auto">There are not Objects Selected in Rhino</span>
{/if}
