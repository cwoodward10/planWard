import { GroupByProperty } from '$modules/utilities/FilterUtilities';

export type DataStructure = {
    data: number[],
    backgroundColor: string[],
    label: string,
    labels: string[],
    weight: number,
    borderColor: string[],
    borderWidth: number[],
    offset: number[],
}

/**
 * Converts an input array into a DataStructure type. It aggregates across either the given dataProperty 
 * (meaning the name of a property on the objects of the input array) or, if that is null, takes the length of the array.
 * The dataProperty must refer to a property on the object of type "number" so that it is summable. 
 * @param title
 * @param input 
 * @param groupByProperty
 * @param dataProperty 
 * @param backgroundColors
 * @param weight the weight of the data on a chart with multiple data sets
 * @returns 
 */
export function ConvertToDataStructure(
    title: string,
    input: any[],
    groupByProperty: string,
    dataProperty: string | null, 
    backgroundColors: string[],
    weight: number = 1,
): DataStructure {
    const groups = GroupByProperty(input, groupByProperty);
        
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
        backgroundColor: backgroundColors,
        label: title,
        labels: labels,
        weight: weight,
        offset: values.map(v => 5),
        borderColor: ["#000"],
        borderWidth: [0]
    }
}