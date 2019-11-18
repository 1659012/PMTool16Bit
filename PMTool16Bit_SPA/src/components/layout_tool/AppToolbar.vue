<template>
  <v-toolbar color="primary" fixed dark app>
    <v-toolbar-title class="ml-0 pl-3"></v-toolbar-title>
    <v-toolbar-side-icon @click.stop="handleDrawerToggle"></v-toolbar-side-icon>
    <!-- <v-text-field flat solo-inverted prepend-icon="search" label="Search" class="hidden-sm-and-down"></v-text-field> -->
    <v-spacer></v-spacer>
    <v-toolbar-items>
      <!-- <v-btn flat href="#">Hire Me</v-btn> -->
    </v-toolbar-items>

    <v-btn icon href="https://github.com/1659012/PMTool16Bit" target="_blank" title="My source on github">
      <v-icon class="fa-2x">fa-github</v-icon>
    </v-btn>

    <v-btn icon @click="handleFullScreen()" title="Full screen">
      <v-icon>fullscreen</v-icon>
    </v-btn>

    <v-btn flat icon color="purple darken-4" title="Logout" @click="logout">
      <v-icon>exit_to_app</v-icon>
    </v-btn>
    
    <!-- <v-menu offset-y origin="center center" class="elelvation-1" :nudge-bottom="14" transition="scale-transition">
      <v-btn icon flat slot="activator">
        <v-badge color="red" overlap>
          <span slot="badge">3</span>
          <v-icon medium>notifications</v-icon>
        </v-badge>
      </v-btn>
      <notification-list></notification-list>
    </v-menu> -->
    <v-menu offset-y origin="center center" :nudge-bottom="10" transition="scale-transition">
      <v-btn icon large flat slot="activator">
        <v-avatar size="30px">
          <img v-if="$store.state.profile.avatarUrl" :src="$store.state.profile.avatarUrl" alt="user_avatar">
          <img v-else src="../../../public/static/avatar/default-avatar.png" alt="user_avatar">
        </v-avatar>
      </v-btn>
      <v-list class="pa-0">
        <v-list-tile
          v-for="(item,index) in items"
          :to="item.href"          
          :key="index"
        >
          <v-list-tile-action v-if="item.icon">
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>{{ item.title }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-menu>
  </v-toolbar>
</template>
<script>
// import NotificationList from "../../components/widgets/list/NotificationList";
import Util from "@/util";
import {APP_MUTATIONS } from "../../store/store";
export default {
  name: "app-toolbar",
  components: {
    // NotificationList
  },
  data: () => ({
    items: [
      {
        icon: "account_circle",
        href: "/profile",
        title: "Profile"     
      },
      {
        icon: "settings",
        href: "#",
        title: "Settings",
        // click: e => {
        //   console.log(e);
        // }
      },
      // {
      //   icon: "fullscreen_exit",
      //   href: "#",
      //   title: "Logout",
      //   click: () => {
      //     window.getApp.$emit("APP_LOGOUT");
      //   }
      // }
    ]
  }),
  mounted(){
    // console.log(this.$store.state.profile);
  },
  computed: {
    toolbarColor() {
      return this.$vuetify.options.extra.mainNav;
    }
  },
  methods: {
    handleDrawerToggle() {
      window.getApp.$emit("APP_DRAWER_TOGGLED");
    },
    handleFullScreen() {
      Util.toggleFullScreen();
    },
    logout() {
      if (confirm("Are you sure to logout?")) {
        this.$store.commit(APP_MUTATIONS.NOT_AUTH);
        this.$store.commit(APP_MUTATIONS.SET_TOKEN, "");
      }
    }
  }
};
</script>
