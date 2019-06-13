import Vue from "vue";
import "./plugins/vuetify";
import App from "./App.vue";
import router from "./router/router";
import { store, APP_ACTIONS, APP_MUTATIONS } from "./store/store";
import "./registerServiceWorker";
import "roboto-fontface/css/roboto/roboto-fontface.css";
import "font-awesome/css/font-awesome.css";
import interceptor from "./store/interceptor";
import titleMixin from "./mixin/titleMixin";

import axios from "axios";
import VueAxios from "vue-axios";
import VueLocalStorage from "vue-localstorage";
import Notifications from "vue-notification";
import VeeValidate from "vee-validate";
import Vue2Filters from "vue2-filters";

// import paths from "./router/paths";
// import Breabcrumbs from 'vue-2-breadcrumbs';

Vue.config.productionTip = false;
Vue.use(VueAxios, axios);
Vue.use(VueLocalStorage);
Vue.use(Notifications);
Vue.use(VeeValidate);
Vue.use(Vue2Filters);
// Vue.use(Breabcrumbs);
Vue.use(require("vue-moment"));

//
Vue.mixin(titleMixin);

interceptor();

var ApiURL = (axios.defaults.baseURL =
  process.env.NODE_ENV !== "production"
    ? "http://localhost:21021/api/services/app/"
    : "http://api.pmtool16bit.vn/api/services/app/");

var BaseUrl =
  process.env.NODE_ENV !== "production"
    ? "http://localhost:21022/"
    : "http://api.pmtool16bit.vn/";

//Filter datetime
Vue.filter("date", function(value) {
  return value ? moment(String(value)).format("DD-MMM-YYYY") : "";
});
Vue.filter("month", function(value) {
  return value ? moment(String(value)).format("MMM/YYYY") : "";
});
Vue.filter("dateTime", function(value) {
  return value
    ? moment(String(value)).format("MMM-DD-YYYY") +
        " " +
        value.toLocaleTimeString("en-US")
    : "";
});

router.onReady(() => {
  // let defaultpath = listrouter.Dashboard.path;
  let defaultpath = "/";
  let loginPath = "/login";
  store.subscribe((mutation, state) => {
    switch (mutation.type) {
      case APP_MUTATIONS.LOGIN_SUCCESS: {
        let redirect = state.previousAuthUrl || router.currentRoute;
        if (redirect.meta.auth) {
          router.push(redirect.fullPath || redirect.path);
        } else {
          // console.log(state.arrValidUrl)
          if (redirect.query && redirect.query.redirect) {
            router.push(redirect.query.redirect);
          } else if (!state.arrValidUrl.includes(redirect.path)) {
            router.push(redirect.path);
          } else {
            router.push(defaultpath);
          }
        }
        break;
      }
      case APP_MUTATIONS.NOT_AUTH: {
        let redirect = state.previousAuthUrl || router.currentRoute;
        if (
          state.permissionFailed ||
          (redirect.meta.auth && redirect.path !== loginPath)
        ) {
          router.push({
            path: loginPath,
            query: {
              redirect: redirect.fullPath || redirect.path
            }
          });
        }
        break;
      }
    }
  });

  store.dispatch(APP_ACTIONS.CHECK_TOKEN).then(resp => {
    router.beforeEach((to, from, next) => {
      if (to.meta.auth && !store.state.auth) {
        store.commit(APP_MUTATIONS.NOT_AUTH, to);
        next({
          path: loginPath,
          query: {
            redirect: to.fullPath || to.path
          }
        });
        router.push(loginPath);
      }
      if (
        store.state.auth &&
        to.path.toLowerCase() === loginPath.toLowerCase()
      ) {
        router.push(defaultpath);
      }
      next();
    });
  });
});

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
