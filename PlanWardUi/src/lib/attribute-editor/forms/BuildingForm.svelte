<script lang="ts">
import type {
    IPlanWardObject
} from '$modules/data-models/IPlanWardObject';
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
    FORM_MULTIPLE_VALUES_STRING,
    FormInputValueHelper
} from '$modules/application/AttributeFormHelpers';
import {
    onMount
} from 'svelte';
import type {
    IAccountingObject
} from '$modules/data-models/IAccountingObject';

type buildingObject =
    IAccountingObject &
    IPlanWardObject & {
        ProgramType: string
    };

const formInputValueHelper = new FormInputValueHelper();

// update form values if 
const unsubscribeRhinoSelection = SelectedRhinoObjects.subscribe((objects) => {
    formInputValueHelper.SetFormText(objects);
    if ($form) {
        form.set({
            designOption: formInputValueHelper.placeholderDesignOption,
            regionName: formInputValueHelper.placeholderRegionName,
            regionIdentifier: formInputValueHelper.placeholderRegionIdentifier,
            programType: formInputValueHelper.placeholderBuildingProgram,
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
        regionName: formInputValueHelper.actualRegionName,
        regionIdentifier: formInputValueHelper.actualRegionIdentifier,
        programType: formInputValueHelper.actualBuildingProgram,
    },
    validationSchema: yup.object().shape({
        designOption: yup.string().not([FORM_MULTIPLE_VALUES_STRING]).required(),
        regionName: yup.string().not([FORM_MULTIPLE_VALUES_STRING]).required(),
        regionIdentifier: yup.string().not([FORM_MULTIPLE_VALUES_STRING]).required(),
        programType: yup.string().not([FORM_MULTIPLE_VALUES_STRING]).required(),
    }),
    onSubmit: values => {
        const updatedObjects = $SelectedRhinoObjects.map((obj: buildingObject) => {
            obj.DesignOption = values.designOption;
            obj.RegionName = values.regionName;
            obj.RegionIdentifier = values.regionIdentifier;
            obj.ProgramType = values.programType;
            return obj;
        });
        const data = JSON.stringify(updatedObjects);
        return pwWindow.Interop.updateObjectUserDictionary(data);
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
            placeholder={formInputValueHelper.placeholderDesignOption}
            on:keyup={handleChange}
            />
        {#if $errors.designOption && $touched.designOption}
        <small class="flex form-input-error">{$errors.designOption}</small>
        {/if}
    </div>

    <div class="flex flex-col space-y-1">
        <label class="flex form-label" for="regionName">Region Name</label>
        <input
            class="flex w-full form-input-text"
            name="regionName"
            value={formInputValueHelper.actualRegionName}
            placeholder={formInputValueHelper.placeholderRegionName}
            on:keyup={handleChange}
            />
        {#if $errors.regionName && $touched.regionName}
        <small class="flex form-input-error">{$errors.regionName}</small>
        {/if}
    </div>

    <div class="flex flex-col space-y-1">
        <label class="flex form-label" for="regionIdentifier">Region Identifier</label>
        <input
            class="flex w-full form-input-text"
            name="regionIdentifier"
            value={formInputValueHelper.actualRegionIdentifier}
            placeholder={formInputValueHelper.placeholderRegionIdentifier}
            on:keyup={handleChange}
            />
        {#if $errors.regionIdentifier && $touched.regionIdentifier}
        <small class="flex form-input-error">{$errors.regionIdentifier}</small>
        {/if}
    </div>

    <div class="flex flex-col space-y-1">
        <label class="flex form-label" for="programType">Program Type</label>
        <input
            class="flex w-full form-input-text"
            name="programType"
            value={formInputValueHelper.actualBuildingProgram}
            placeholder={formInputValueHelper.placeholderBuildingProgram}
            on:keyup={handleChange}
            />
        {#if $errors.programType && $touched.programType}
        <small class="flex form-input-error">{$errors.programType}</small>
        {/if}
    </div>

    <button class="flex btn-standard" type="submit" disabled={!$isValid}>
        {#if $isSubmitting}Set Rhino Attributes...{:else}submit{/if}
    </button>
</form>
