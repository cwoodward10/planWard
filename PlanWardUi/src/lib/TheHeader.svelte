<script lang="ts">
	import { AppStateEnum } from '../modules/application/ApplicationStateHelpers';
	import { ApplicationState } from './../modules/store/MainStore';
    import Icon from 'svelte-awesome';
    import { thList, database, gear } from 'svelte-awesome/icons';

    function setAppState(state: AppStateEnum) {
        ApplicationState.set(state);
    }
</script>

<header class="mx-auto flex flex-col justify-center">
    <h1 class="text-center text-xl font-medium text-gray-800">PlanWard</h1>
    <div class="flex space-x-1 mx-auto">
        <button 
            class="w-6 h-6 flex border border-solid border-gray-500 rounded-md bg-white drop-shadow-sm" 
            on:click={() => setAppState(AppStateEnum.MainInformation)}
        >
            <Icon class="flex m-auto material-icons-outlined" data={thList}/>
        </button>
        <button 
            class="w-6 h-6 flex border border-solid border-gray-500 rounded-md bg-white drop-shadow-sm"
            on:click={() => setAppState(AppStateEnum.MainAttributeEditor)}

        >
            <Icon class="flex m-auto material-icons-outlined" data={database}/>
        </button>
        <button 
            class="w-6 h-6 flex border border-solid border-gray-500 rounded-md bg-white drop-shadow-sm"
            on:click={() => setAppState(AppStateEnum.MainSettings)}

        >
            <Icon class="flex m-auto material-icons-outlined" data={gear}/>
        </button>
    </div>
</header>