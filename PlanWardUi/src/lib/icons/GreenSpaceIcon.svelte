<script>
    export let strokeColor = '#000000';
    export let fillColor = '#000000';

    $: cssVars = `
        --stroke-color: ${strokeColor};
        --fill-color: ${fillColor};
        height: 100%;
		width: 100%;
    `
</script>

<svg style={cssVars} version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	 viewBox="0 0 100 100" enable-background="new 0 0 100 100" xml:space="preserve">
    <g id="Outline">
        <polygon stroke="var(--stroke-color)" fill="var(--fill-color)" fill-opacity="0.25" stroke-linecap="round" stroke-linejoin="round" points="0,55.3 50,84.2 100,55.3 
            50,26.4 	"/>
    </g>
    <g id="Trees">
        <g>
            <rect x="8" y="44.2" width="0.2" height="11.9"/>
            <circle fill="#FFFFFF" cx="8.1" cy="42.6" r="6"/>
            <circle fill="var(--fill-color)" opacity="0.5" cx="8.1" cy="42.6" r="6"/>
        </g>
        <g>
            <rect x="16.9" y="45.6" width="0.2" height="13.7"/>
            <circle fill="#FFFFFF" cx="17" cy="43.8" r="6.9"/>
            <circle fill="var(--fill-color)" opacity="0.6" cx="17" cy="43.8" r="6.9"/>
        </g>
        <g>
            <rect x="32.7" y="60.7" width="0.1" height="8.4"/>
            <circle fill="#FFFFFF" cx="32.7" cy="59.6" r="4.3"/>
            <circle fill="var(--fill-color)" opacity="0.4" cx="32.7" cy="59.6" r="4.3"/>
        </g>
        <g>
            <rect x="43" y="65.4" width="0.2" height="11.9"/>
            <circle fill="#FFFFFF" cx="43.1" cy="63.8" r="6"/>
            <circle fill="var(--fill-color)" opacity="0.5" cx="43.1" cy="63.8" r="6"/>
        </g>
        <g>
            <rect x="50.2" y="24.2" width="0.2" height="13.1"/>
            <circle fill="#FFFFFF" cx="50.3" cy="22.5" r="6.6"/>
            <circle fill="var(--fill-color)" opacity="0.5" cx="50.3" cy="22.5" r="6.6"/>
        </g>
        <g>
            <rect x="78.8" y="40.9" width="0.2" height="11.9"/>
            <circle fill="#FFFFFF" cx="78.9" cy="39.4" r="6"/>
            <circle fill="var(--fill-color)" opacity="0.5" cx="78.9" cy="39.4" r="6"/>
        </g>
        <g>
            <rect x="87.8" y="44.5" width="0.2" height="13.4"/>
            <circle fill="#FFFFFF" cx="87.9" cy="42.7" r="6.8"/>
            <circle fill="var(--fill-color)" opacity="0.75" cx="87.9" cy="42.7" r="6.8"/>
        </g>
        <g>
            <rect x="42.9" y="28.7" width="0.2" height="11.9"/>
            <circle fill="#FFFFFF" cx="43" cy="27.1" r="6"/>
            <circle fill="var(--fill-color)" opacity="0.6" cx="43" cy="27.1" r="6"/>
        </g>
        <g>
            <rect x="74" y="57.9" width="0.1" height="9.3"/>
            <circle fill="#FFFFFF" cx="74.1" cy="56.6" r="4.7"/>
            <circle fill="var(--fill-color)" opacity="0.4" cx="74.1" cy="56.6" r="4.7"/>
        </g>
    </g>
</svg>
