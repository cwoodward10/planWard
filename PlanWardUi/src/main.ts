import App from './App.svelte';

import '$assets/styles/reset.css';
import '$assets/styles/styles.css';

const app = new App({
  target: document.getElementById('app')
})

export default app
