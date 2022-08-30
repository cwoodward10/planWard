/**
 * Add's an eventListener onClick that checks if the user
 * has clicked outside of the targeted node. 
 * If so, it returns a new 'outclick' event.
 * @param node node to perform action on
 * @returns a destroy function to call on svelte component destroy
 */
 export function handleClickOutsideElement(node: Node) {
	const handleClick = (event: Event) => {
		if (event.target instanceof Node &&
            !node.contains(event.target)) {
			node.dispatchEvent(new CustomEvent("outclick"));
		}
	};

	document.addEventListener("click", handleClick, true);

	return {
		destroy() {
			document.removeEventListener("click", handleClick, true);
		}
	};
}