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
    <g id="Fill">
        <path fill="var(--fill-color)" opacity="0.25" d="M100,42.4c-2.6,1.5-5.3,3-7.9,4.6c2.6,1.5,5.3,3,7.9,4.6c-15.8,9.1-31.6,18.2-47.4,27.4
            c-2.6-1.5-5.3-3-7.9-4.6c-2.6,1.5-5.3,3-7.9,4.6h0c-0.1-0.1-0.2-0.1-0.2-0.1c-0.4-0.2-0.8-0.5-1.2-0.7c-0.5-0.3-1-0.6-1.5-0.9
            c-1-0.6-2-1.2-3-1.7c-2-1.2-4-2.3-6-3.4c1-0.5,1.9-1.1,2.9-1.7c0.5-0.3,1-0.6,1.4-0.8c0.5-0.3,1-0.5,1.4-0.8
            c0.4-0.3,0.9-0.7,0.9-1.3c0-0.6-0.6-1-1-1.3c-1-0.6-1.9-1.1-2.9-1.7c-0.5-0.3-1-0.6-1.5-0.9c-0.5-0.3-0.8-0.5-1.4-0.6
            c-1-0.2-2.3-0.1-3.2,0.4c-0.5,0.3-1,0.5-1.4,0.8s-1,0.6-1.4,0.8c-1,0.6-1.9,1.1-2.9,1.7c-5.3-3-10.5-6.1-15.8-9.1
            c2.6-1.5,5.3-3,7.9-4.6c-2.6-1.5-5.3-3-7.9-4.6c13.2-7.6,26.3-15.2,39.5-22.8c1,0.6,1.9,1.1,2.9,1.7c0.5,0.3,1,0.6,1.5,0.8
            c0.2,0.1,0.5,0.3,0.7,0.4c0.2,0.1,0.5,0.3,0.7,0.4c1,0.5,2.2,0.6,3.3,0.3c0.3-0.1,0.6-0.2,0.8-0.3c0.3-0.1,0.6-0.3,0.8-0.5
            c0.4-0.2,0.9-0.5,1.3-0.7c3.9-2.2,7.7-4.5,11.6-6.7l10.5,6.1c-0.9,0.5-1.9,1.1-2.8,1.6c-0.5,0.3-1,0.5-1.4,0.9
            c-0.4,0.3-0.8,0.8-1,1.3c-0.4,1.2,0.2,2.2,1.1,2.9c0.4,0.4,0.9,0.6,1.4,0.9c0.5,0.3,1,0.5,1.4,0.8c2.6-1.5,5.3-3,7.9-4.6
            C86.8,34.8,93.4,38.6,100,42.4z M70.6,47.2c0.3-0.1,0.3-0.4,0-0.5c-5-2.9-9.9-5.7-14.9-8.6c-0.2-0.1-0.6-0.1-0.9,0
            c-0.1,0.1-0.3,0.2-0.4,0.3c-0.3,0.1-0.3,0.4,0,0.5c5,2.9,9.9,5.7,14.9,8.6c0.2,0.1,0.6,0.1,0.9,0C70.3,47.4,70.5,47.3,70.6,47.2z
            M45.6,61.6c0.3-0.1,0.3-0.4,0-0.5c-5-2.9-9.9-5.7-14.9-8.6c-0.2-0.1-0.6-0.1-0.9,0c-0.1,0.1-0.3,0.2-0.4,0.3
            c-0.3,0.1-0.3,0.4,0,0.5c5,2.9,9.9,5.7,14.9,8.6c0.2,0.1,0.6,0.1,0.9,0C45.3,61.8,45.5,61.7,45.6,61.6z"/>
        <path d="M36.8,78.9C36.9,78.9,36.9,78.9,36.8,78.9C36.9,78.9,36.9,78.9,36.8,78.9z"/>
    </g>
    <g id="Parking">
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="72.4,35.6 
            80.3,31 84.2,33.3 76.3,37.8 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="84.2,42.4 
            92.1,37.8 96.1,40.1 88.2,44.7 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="80.3,40.1 
            88.2,35.6 92.1,37.8 84.2,42.4 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="32.9,67.5 
            25,72 28.9,74.3 36.8,69.7 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="40.8,72 
            32.9,76.6 36.8,78.9 44.7,74.3 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="36.8,69.7 
            28.9,74.3 32.9,76.6 40.8,72 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="76.3,37.8 
            84.2,33.3 88.2,35.6 80.3,40.1 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="69.7,47.7 
            61.8,43.2 57.9,45.4 65.8,50 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="88.2,44.7 
            96.1,40.1 100,42.4 92.1,47 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="64.5,62.9 
            72.4,67.5 68.4,69.7 60.5,65.2 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="68.4,60.6 
            76.3,65.2 72.4,67.5 64.5,62.9 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="92.1,47 
            100,51.5 96.1,53.8 88.2,49.2 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="56.6,67.5 
            64.5,72 60.5,74.3 52.6,69.7 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="60.5,65.2 
            68.4,69.7 64.5,72 56.6,67.5 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="48.7,72 
            56.6,76.6 52.6,78.9 44.7,74.3 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="52.6,69.7 
            60.5,74.3 56.6,76.6 48.7,72 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="72.4,58.3 
            80.3,62.9 76.3,65.2 68.4,60.6 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="76.3,56.1 
            84.2,60.6 80.3,62.9 72.4,58.3 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="80.3,53.8 
            88.2,58.3 84.2,60.6 76.3,56.1 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="84.2,51.5 
            92.1,56.1 88.2,58.3 80.3,53.8 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="88.2,49.2 
            96.1,53.8 92.1,56.1 84.2,51.5 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="53.9,38.6 
            61.8,43.2 57.9,45.4 50,40.9 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="50,59.1 
            42.1,54.5 38.2,56.8 46.1,61.4 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="34.2,50 
            42.1,54.5 38.2,56.8 30.3,52.3 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="53.9,56.8 
            46.1,52.3 42.1,54.5 50,59.1 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="38.2,47.7 
            46.1,52.3 42.1,54.5 34.2,50 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="65.8,50 
            57.9,45.4 53.9,47.7 61.8,52.3 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="61.8,52.3 
            53.9,47.7 50,50 57.9,54.5 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="57.9,54.5 
            50,50 46.1,52.3 53.9,56.8 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="50,40.9 
            57.9,45.4 53.9,47.7 46.1,43.2 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="46.1,43.2 
            53.9,47.7 50,50 42.1,45.4 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="42.1,45.4 
            50,50 46.1,52.3 38.2,47.7 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="27.6,41.6 
            19.7,37.1 15.8,39.4 23.7,43.9 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="31.6,39.4 
            23.7,34.8 19.7,37.1 27.6,41.6 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="19.7,46.2 
            11.8,41.6 7.9,43.9 15.8,48.5 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="23.7,43.9 
            15.8,39.4 11.8,41.6 19.7,46.2 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="11.8,50.7 
            3.9,46.2 0,48.5 7.9,53 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="15.8,48.5 
            7.9,43.9 3.9,46.2 11.8,50.7 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="35.5,37.1 
            27.6,32.5 23.7,34.8 31.6,39.4 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="39.5,34.8 
            31.6,30.2 27.6,32.5 35.5,37.1 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="43.4,32.5 
            35.5,28 31.6,30.2 39.5,34.8 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="47.4,30.2 
            39.5,25.7 35.5,28 43.4,32.5 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="19.7,59.9 
            11.8,64.4 15.8,66.7 23.7,62.1 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="15.8,57.6 
            7.9,62.1 11.8,64.4 19.7,59.9 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="11.8,55.3 
            3.9,59.9 7.9,62.1 15.8,57.6 	"/>
        <polyline fill="none" stroke="var(--stroke-color)" stroke-width="0.25" stroke-linecap="round" stroke-linejoin="round" points="7.9,53 
            0,57.6 3.9,59.9 11.8,55.3 	"/>
    </g>
    <g id="Curbs">
        <path fill="none" stroke="var(--stroke-color)" stroke-width="0.5" stroke-linecap="round" stroke-linejoin="round" d="M63.2,21.1
            c-3.9,2.2-7.7,4.5-11.6,6.7c-0.4,0.2-0.9,0.5-1.3,0.7c-0.3,0.2-0.6,0.3-0.9,0.5c-0.3,0.1-0.5,0.2-0.8,0.3c-1,0.2-2.3,0.2-3.3-0.3
            c-0.2-0.1-0.5-0.3-0.7-0.4c-0.2-0.1-0.5-0.3-0.7-0.4c-0.5-0.3-1-0.6-1.5-0.8c-1-0.6-1.9-1.1-2.9-1.7C26.3,33.3,13.2,40.9,0,48.5
            c2.6,1.5,5.3,3,7.9,4.6c-2.6,1.5-5.3,3-7.9,4.6c5.3,3,10.5,6.1,15.8,9.1c1-0.6,1.9-1.1,2.9-1.7c0.5-0.3,1-0.6,1.4-0.8
            c0.5-0.3,1-0.6,1.4-0.8c0.9-0.5,2.2-0.6,3.2-0.4c0.6,0.1,0.9,0.3,1.4,0.6c0.5,0.3,1,0.6,1.5,0.9c1,0.6,1.9,1.1,2.9,1.7
            c0.5,0.3,1,0.7,1,1.3c0,0.6-0.4,1-0.9,1.3c-0.5,0.3-1,0.6-1.4,0.8c-0.5,0.3-1,0.6-1.4,0.8c-1,0.6-1.9,1.1-2.9,1.7
            c2,1.1,4,2.3,6,3.4c1,0.6,2,1.1,3,1.7c0.5,0.3,1,0.6,1.5,0.9c0.4,0.2,0.8,0.5,1.2,0.7c0.1,0,0.2,0.1,0.2,0.1c0,0,0,0,0,0
            c0,0,0,0,0,0c2.6-1.5,5.3-3,7.9-4.6c2.6,1.5,5.3,3,7.9,4.6c15.8-9.1,31.6-18.2,47.4-27.3c-2.6-1.5-5.3-3-7.9-4.6
            c2.6-1.5,5.3-3,7.9-4.6c-6.6-3.8-13.2-7.6-19.7-11.4c-2.6,1.5-5.3,3-7.9,4.6c-0.5-0.3-0.9-0.5-1.4-0.8c-0.5-0.3-1-0.5-1.4-0.9
            c-0.9-0.7-1.5-1.7-1.1-2.9c0.2-0.5,0.6-1,1-1.3c0.4-0.4,0.9-0.6,1.4-0.9c0.9-0.5,1.9-1.1,2.8-1.6"/>
        <path fill="none" stroke="var(--stroke-color)" stroke-width="0.5" stroke-linecap="round" stroke-linejoin="round" d="M45.6,61.6
            c-0.1,0.1-0.3,0.2-0.4,0.3c-0.2,0.1-0.6,0.1-0.9,0c-5-2.9-9.9-5.7-14.9-8.6c-0.2-0.1-0.2-0.4,0-0.5c0.1-0.1,0.3-0.2,0.4-0.3
            c0.2-0.1,0.6-0.1,0.9,0c5,2.9,9.9,5.7,14.9,8.6C45.9,61.3,45.9,61.5,45.6,61.6z"/>
        <path fill="none" stroke="var(--stroke-color)" stroke-width="0.5" stroke-linecap="round" stroke-linejoin="round" d="M70.6,47.2
            c-0.1,0.1-0.3,0.2-0.4,0.3c-0.2,0.1-0.6,0.1-0.9,0c-5-2.9-9.9-5.7-14.9-8.6c-0.2-0.1-0.2-0.4,0-0.5c0.1-0.1,0.3-0.2,0.4-0.3
            c0.2-0.1,0.6-0.1,0.9,0c5,2.9,9.9,5.7,14.9,8.6C70.9,46.8,70.9,47.1,70.6,47.2z"/>
        <polygon stroke="var(--stroke-color)" fill="var(--fill-color)" opacity="0.5" points="68.2,26.2 66.1,26.5 66.5,26.7 64.7,27.8 65.2,28 65.6,28.3 67.4,27.2 67.8,27.5 	"/>
        <polygon stroke="var(--stroke-color)" fill="var(--fill-color)" opacity="0.5" points="61.8,26 63.9,25.8 63.5,25.5 65.3,24.5 64.8,24.3 64.4,24 62.6,25 62.2,24.8 	"/>
    </g>
</svg>
