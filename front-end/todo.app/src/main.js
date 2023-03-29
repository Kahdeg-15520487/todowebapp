import { createApp } from "vue";
import App from "./App.vue";
import { createPinia } from 'pinia'
import "./assets/reset.css";
import '@dafcoe/vue-collapsible-panel/dist/vue-collapsible-panel.css'
const pinia = createPinia()
const app = createApp(App)

app.use(pinia)
app.mount('#app')
