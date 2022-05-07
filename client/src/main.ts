import { createApp } from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import axios from "axios";
import VueAxios from "vue-axios";

const app = createApp(App);
app.config.globalProperties.hostname = "https://localhost:5001";
app.use(router);
app.use(VueAxios, axios);
app.mount("#app");
