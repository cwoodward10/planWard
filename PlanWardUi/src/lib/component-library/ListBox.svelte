<script lang="ts">
    import { createEventDispatcher } from 'svelte';

    import { SelectorIcon, CheckIcon } from '@rgossiaux/svelte-heroicons/outline';
    
    import { handleClickOutsideElement } from '$modules/SvelteActions';

    export let options: { id: string | number, value: string | number }[];
    export let selected: { id: string | number, value: string | number };

    let showMenu = false;
    /** prevents the click event of showing the the menu to come through immediately following this one.*/
    let preventShowMenu = false;

    function handleShowMenu(event: Event) {
        if (!preventShowMenu) {
            showMenu = true;
        }
    } 

    function handleClickOut(event: Event) {
        showMenu = false;
        preventShowMenu = true;
        // set a timout to return preventShowMenu to false.
        setTimeout(() => preventShowMenu = false, 100);
    }  

    const dispatch = createEventDispatcher();
    function handleClick(itemId: string | number) {
        dispatch('selectionChanged', itemId);
        showMenu = false;
    }
</script>

<menu id="list-box-container" 
    class="relative w-full">
    <div class="flex justify-between rounded-lg bg-white shadow px-4 py-2" 
        on:click="{handleShowMenu}">
        <p class="select-none">{ selected.value }</p>
        <SelectorIcon class="h-4 my-auto" />
    </div>
    {#if showMenu}
        <ol id="list-box-menu" 
            class="w-full absolute overflow-hidden left-0 top-full translate-y-3 bg-white rounded-lg drop-shadow-lg flex flex-col align-baseline"  
            use:handleClickOutsideElement
            on:outclick="{handleClickOut}">
            {#each options as option }
                <li 
                    class="relative w-full flex gap-2 hover:bg-pw-navy hover:text-white px-4 py-2 cursor-pointer" 
                    on:click={() => handleClick(option.id)}>
                    {#if selected.id === option.id}
                    <CheckIcon class="absolute top-1/2 left-3 -translate-y-1/2 my-auto h-4"/>
                    {/if}
                    <p class="ml-6 select-none">{ option.value }</p>
                </li>
            {/each}
        </ol>
    {/if}
</menu>