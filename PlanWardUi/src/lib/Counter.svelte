<script lang="ts">
	import { ParkingCount, TotalSquareFootage } from './../modules/store/MainStore';

  let parkingCount = 0;
  ParkingCount.subscribe((value: number) => {
    parkingCount = value;
  })

  let totalSF = "0";
  TotalSquareFootage.subscribe((value: number) => {
    totalSF = value.toFixed(2);
  })
</script>

<article class="max-w-md mx-auto flex flex-col p-3 border border-gray-700 border-solid">
  <div class="grid grid-cols-5">
    <h1 class="col-span-2 text-center font-bold text-gray-800">{ parkingCount }</h1>
    <h1 class="col-span-3 font-light text-gray-800">Parking Stalls</h1>
  </div>
  <div class="grid grid-cols-5">
    <h1 class="col-span-2 text-center font-bold text-gray-800">{ totalSF }</h1>
    <h1 class="col-span-3 font-light text-gray-800">Total Square Feet</h1>
  </div>
</article>