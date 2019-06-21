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
import moment from "moment";

// import paths from "./router/paths";
// import Breabcrumbs from 'vue-2-breadcrumbs';

Vue.config.productionTip = false;
Vue.use(VueAxios, axios);
Vue.use(VueLocalStorage);
Vue.use(Notifications);
Vue.use(VeeValidate);
Vue.use(Vue2Filters);
// Vue.use(Breabcrumbs);

Vue.use(require('vue-moment'), {
  moment
})

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

//Filter datetime
Vue.filter('truncate', function (value, limit) {
  if(!value) return "";

  if (value.length > limit) {
      value = value.substring(0, (limit - 3)) + '...';
  }

  return value
});
Vue.mixin({
  methods: {
    toDateString(date) {
      if (date) return moment(date).format("YYYY-MM-DD");
      return "";
    },
    toMonthString(date) {
      if (date) return moment(date).format("YYYY-MM");
      return "";
    },
    momentFromNow(date) {      
      if (date) return moment(date).fromNow();
      return "";
    },
    createItem(item, url, me, isCloseModal = true) {
      Vue.axios.post(url, item).then(response => {
        if (response.data.success) {
          if (isCloseModal) me.close(response.data.result);
          else me.updateResult(response.data.result);
          Vue.notify({
            group: "message",
            type: "success",
            title: "Add",
            text: "Create successfully"
          });
          return response.data.result;
        } else {
          Vue.notify({
            group: "message",
            type: "error",
            title: "Important message",
            text: "Create unsuccessfully"
          });
        }
      });
      // .catch(e => {
      // this.errors.push(e);
      // });
    },
    updateItem(item, url, me, isCloseModal = true) {
      Vue.axios
        .put(url, item)
        .then(response => {
          if (response.data.success) {
            if (isCloseModal) me.close(response.data.result);
            else me.updateResult(response.data.result);
            Vue.notify({
              group: "message",
              type: "success",
              title: "Update",
              text: "Data has been updated!"
            });
            return response.data.result;
          } else {
            Vue.notify({
              group: "message",
              type: "error",
              title: "Important message",
              text: "Hello user! This is a notification!"
            });
          }
        })
        .catch(e => {
          // this.errors.push(e);
        });
    },
    deleteItem(item, url, me) {
      if (confirm("Are you sure you want to delete this item?")) {
        Vue.axios
          .delete(url, { params: { Id: item.id } })
          .then(response => {
            if (response.data.success) {
              me.loadData();
              Vue.notify({
                group: "message",
                type: "success",
                title: "Delete",
                text: "Data has been deleted!"
              });
              return response.data.success;
            } else {
              Vue.notify({
                group: "message",
                type: "error",
                title: "Important message",
                text: "Hello user! This is a notification!"
              });
            }
          })
          .catch(e => {
            // this.errors.push(e);
          });
      }
    },
    getFullPath(path) {
      path = path.replace(/\\/g, "/");
      return store.state.baseUrl + path;
    }
  }
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
