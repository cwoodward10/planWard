export class TrackedRhinoObject {
    /** Persistent Id of the object in Rhino */
    rhinoId: string;

    /** Name of the Design Option that this Rhino Object belongs to. */
    designOptionName: string;

    /**
     * Name of the Region Name that this object belongs to within a Design Option.
     * ex: The "A" in building "A2"
     */
    regionName: string;

    /**
     * An Identifier to differentiate itself from other objects within the Region.
     * ex: The "2" in building "A2"
     */
    regionIdentifier: string;
}