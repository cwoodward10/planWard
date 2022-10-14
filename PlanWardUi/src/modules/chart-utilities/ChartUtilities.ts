import { GroupByProperty } from '$modules/utilities/FilterUtilities';

export type DataStructure = {
    data: number[],
    backgroundColor: string[],
    label: string,
    labels: string[]
}

export function ConvertToDataStructure(input: any[], dataProperty: string | null): DataStructure {
    const groups = GroupByProperty(input, "DesignOption");
        
    let values: number[] = [];
    let labels: string[] = [];
    for (let groupName in groups) {
        labels.push(groupName);
        const value = dataProperty === null ?
            groups[groupName].length :
            groups[groupName].reduce((total: number, obj: any) => total + obj[dataProperty], 0);
        values.push(value);
    }

    return {
        data: values,
        backgroundColor: ["#45ff6b"],
        label: "Design Options",
        labels: labels
    }
}

export function CreateChartConfig(
    data: DataStructure[], 
    chartType: string,
    legendPosition: "left" | "center" | "right" = "left",
    onChartClick: (e: Event) => void = (e: Event) => {},
    onLegendClick: (e: Event, legendItem: any) => void = (e: Event, legendItem) => {},
    rotation: number = 0,
) {
    // combine all labels of datasets for use in legend
    const allLabels = data.reduce((labels: string[], nextData: DataStructure) => {
        return labels.concat(nextData.labels);
    }, [])

    return {
        type: chartType,
        data: {
            datasets: data,
            labels: allLabels
        },
        options: {
            rotation: rotation,
            responsive: true,
            legend: {
                display: true,
                position:legendPosition,
                onClick: onLegendClick
            },
            onClick: onChartClick
        },
    }
}