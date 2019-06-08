import axios from 'axios';
import Vue from 'vue';
import { store, APP_MUTATIONS } from "./store.js"

export default function setup() {
    axios.interceptors.request.use(function (config) {
        // Do something before request is sent
        config.headers.Authorization = `Bearer ${localStorage.getItem("token")}`
        return config;
    }, function (error) {
        // Do something with request error
        return Promise.reject(error);
    });
    axios.interceptors.response.use(function (response) {
        // if (response.config.method != "get")

        // Do something with response data
        return response;
    }, function (error) {
        // Do something with response error
        let data = error.response;
        if (data.status == 400) {
            Vue.notify({
                group: "message",
                type: "error",
                duration: 4000,
                title: data.data.error.message,
                text: data.data.error.details
            });
        }
        else if (data.status == 401) {
            store.commit(APP_MUTATIONS.NOT_AUTH);
            Vue.notify({
                group: "message",
                type: "error",
                duration: 4000,
                title: data.data.error.message,
                text: data.data.error.details
            });
        }
        else if (data.status == 500) {
            Vue.notify({
                group: "message",
                type: "error",
                duration: 4000,
                title: data.data.error.message,
                text: data.data.error.details
            });
        }
        else if (data.status == 502) {
            Vue.notify({
                group: "message",
                type: "error",
                duration: 4000,
                title: data.data.error.message,
                text: data.data.error.details
            });
        }
        else if (data.status == 403) {
            store.commit(APP_MUTATIONS.NOT_AUTH, "/");
            Vue.notify({
                group: "message",
                type: "error",
                duration: 4000,
                title: data.data.error.message,
                text: data.data.error.details
            });
        } else if (data.status == 404) {
            Vue.notify({
                group: "message",
                type: "error",
                duration: 4000,
                title: data.data.error.message,
                text: data.data.error.details
            });
        } else if (data.status == 415) {
            Vue.notify({
                group: "message",
                type: "error",
                duration: 4000,
                title: data.data.error.message,
                text: data.data.error.details
            });
        }
        return Promise.reject(error);
    });
}