import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);
export const APP_ACTIONS = {
  CHECK_TOKEN: "CHECK_TOKEN",
  LOGIN: "LOGIN",
  SUBSCRIBE_NOTIFICATION: "SUBSCRIBE_NOTIFICATION"
};

export const APP_MUTATIONS = {
  SET_TOKEN: "SET_TOKEN",
  SET_USER: "SET_USER",
  SET_ROLES: "SET_ROLES",
  LOGIN_SUCCESS: "LOGIN_SUCCESS",
  NOT_AUTH: "NOT_AUTH",
  LOGIN: "LOGIN",
  LOGOUT: "LOGOUT"
  // UPDATE_NOTIFICATION_COUNT: 'UPDATE_NOTIFICATION_COUNT',
  // UPDATE_NOTIFICATION_LIST: 'UPDATE_NOTIFICATION_LIST',
  // UPDATE_NOTIFICATION_STATUS: 'UPDATE_NOTIFICATION_STATUS',
  // ADD_NOTIFICATION: 'ADD_NOTIFICATION',
  // MARK_ALL_NOTIFICATION_AS_READ: 'MARK_ALL_NOTIFICATION_AS_READ',
};

export const store = new Vuex.Store({
  state: {
    token: "",
    userId: {},
    userName: "",
    profile: {},
    previousAuthUrl: "",
    arrValidUrl: ["/login"],
    auth: false,
    permissionFailed: false,
    baseUrl:
      process.env.NODE_ENV !== "production"
        ? "https://hoanpmtoolapi.azurewebsites.net/"
        : "https://hoanpmtoolapi.azurewebsites.net/",
    notifications: [],
    notificationCount: 0
    // roles: ""
  },
  mutations: {
    [APP_MUTATIONS.SET_TOKEN]: (state, token) => {
      state.token = token;
      
      if (!token) {
        localStorage.clear();
      }
    },
    [APP_MUTATIONS.SET_USER]: (state, profile) => {
      state.userId = profile.id;
      state.userName = profile.userName;
      state.profile = profile;
    },
    // [APP_MUTATIONS.SET_ROLES]: (state, roles) => {
    //   state.roles = roles;
    // },
    [APP_MUTATIONS.LOGIN_SUCCESS](state) {
      state.auth = true;
      state.permissionFailed = true;// to do
    },
    [APP_MUTATIONS.NOT_AUTH](state, path) {
      if (path) {
        state.previousAuthUrl = path;
        state.permissionFailed = true;
      }
      state.auth = false;
      state.token = "";
      state.userId = null;
      state.userName = "";
      state.profile = {};
    }
  },
  actions: {
    [APP_ACTIONS.CHECK_TOKEN]: context => {
      let token = localStorage.getItem("token");
      // let roles = localStorage.getItem("roles");
      let profile = JSON.parse(localStorage.getItem("profile"));
      if (token) {
       
        context.commit(APP_MUTATIONS.LOGIN_SUCCESS);
        context.commit(APP_MUTATIONS.SET_USER, profile);
        // context.commit(APP_MUTATIONS.SET_ROLES, roles);
      } else {
        context.commit(APP_MUTATIONS.NOT_AUTH);
      }
     
    },
    [APP_ACTIONS.LOGIN]: (context, { token, profile, roles }) => {
      context.commit(APP_MUTATIONS.SET_TOKEN, token);
      context.commit(APP_MUTATIONS.SET_USER, profile);
      // context.commit(APP_MUTATIONS.SET_ROLES, roles)
    },
    [APP_ACTIONS.LOGOUT]: (context) => {
      context.commit(APP_MUTATIONS.NOT_AUTH);     
    }
  }
});
