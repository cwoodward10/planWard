<script lang="ts">
import type {
    IPlanWardObject
} from './../../../modules/data-models/IPlanWardObject';
import type {
    PlanWardWindow
} from '$modules/PlanWardWindow';
import {
    createForm
} from "svelte-forms-lib";
import * as yup from 'yup';
import {
    SelectedRhinoObjects
} from '$modules/store/MainStore';
import {
    FormInputValueHelper,
    FORM_MULTIPLE_VALUES_STRING
} from '$modules/application/AttributeFormHelpers';
import {
    onMount
} from 'svelte';

const formInputValueHelper = new FormInputValueHelper();

// update form values if 
const unsubscribeRhinoSelection = SelectedRhinoObjects.subscribe((objects) => {
    formInputValueHelper.SetFormText(objects);
    if ($form) {
        form.set({
            designOption: formInputValueHelper.placeholderDesignOption
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
        designOption: formInputValueHelper.actualDesignOption,
    },
    validationSchema: yup.object().shape({
        designOption: yup.string().not([FORM_MULTIPLE_VALUES_STRING]).required(),
    }),
    onSubmit: values => {
        const updatedObjects = $SelectedRhinoObjects.map(obj => {
            obj.DesignOption = values.designOption;
            return obj;
        });
        const data = JSON.stringify(updatedObjects);
        pwWindow.Interop.updateObjectUserDictionary(data);
    }
});
</script>

<form class="flex flex-col space-y-3 w-full h-full" class:valid={$isValid} on:submit={handleSubmit}>
    <div class="flex flex-col space-y-1">
        <label class="flex form-label" for="designOption">Design Option Name</label>
        <input
            class="flex w-full form-input-text"
            name="designOption"
            value={formInputValueHelper.actualDesignOption}
            placeholder={$form.designOption}
            on:keyup={handleChange}
            />
        {#if $errors.designOption && $touched.designOption}
        <small class="flex form-input-error">{$errors.designOption}</small>
        {/if}
    </div>

    <div class="flex flex-col w-full justify-center">
        <button
            class="flex mx-auto btn-standard"
            type="submit"
            disabled={!$isValid}
            >
            Submit
        </button>
        {#if $isSubmitting}
        <p class="flex mx-auto text-gray-500 text-center font-thin">Setting Rhino Attributes</p>
        {/if}
    </div>
</form>
