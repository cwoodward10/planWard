export function GroupByProperty<Input>(
    data: Input[], 
    property: string): {[key: string]: Input[]}
{
    return data.reduce((grouping: {}, next: Input) => {
        const key = next[property] == null ? "undefined" : next[property];
        
        if (grouping[key] == null) {
            grouping[key] = [next];
        } else {
            grouping[key] = grouping[key].concat(next);
        }
        
        return grouping;
    }, {})
}