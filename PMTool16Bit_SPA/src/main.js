import Vue from "vue";
import "./plugins/vuetify";
import App from "./App.vue";
import router from "./router/";
import store from "./store/store";
import "./registerServiceWorker";
import "roboto-fontface/css/roboto/roboto-fontface.css";
import "font-awesome/css/font-awesome.css";
import axios from "axios";
import VueAxios from "vue-axios";
import VueLocalStorage from "vue-localstorage";
import Notifications from "vue-notification";
import VeeValidate from "vee-validate";
import Breabcrumbs from 'vue-2-breadcrumbs';

Vue.config.productionTip = false;
Vue.use(VueAxios, axios);
Vue.use(VueLocalStorage);
Vue.use(Notifications);
Vue.use(VeeValidate);
Vue.use(Breabcrumbs);



new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
