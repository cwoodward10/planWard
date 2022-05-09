# planWard
Rhino Plugin that helps users plan our urban developments by giving them real-time updates to the information that they need. 


### Components
PlanWard has 2 components: 
1) The Rhino Plugin itself that is written in C# and interacts directly with Rhino.
2) The UI portion of the Plugin where the User can View and Set Information. It is written in Typescript with Svelte and interacts with Rhino using cefSharp's interprocess communication. 