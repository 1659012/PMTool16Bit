<template>
  <v-container fluid>
    <h3>About Project</h3>
    <div class="text-xs-center my-3">
      <h2>Tools</h2>
    </div>
    <v-layout row wrap class="text-xs-center" justify-center>
      <v-flex lg2 v-for="(tool, index) in tools" :key="index" class="mx-3">
        <v-card flat width="250">
          <v-img :aspect-ratio="1" :src="tool.avatarUrl"></v-img>
        </v-card>
        <v-card-title primary-title align-center justify-center>
          <h4 class="display-1 font-weight-thin">{{ tool.name }}</h4>
        </v-card-title>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
export default {
  title: "About tools",
  components: {},
  data: () => ({
    tools: [
      {
        name: "Vuejs",
        avatarUrl: "https://upload.wikimedia.org/wikipedia/commons/f/f1/Vue.png"
      },
      {
        name: "Vuetify",
        avatarUrl: "https://cdn.vuetifyjs.com/images/logos/vuetify-logo-300.png"
      },
      {
        name: ".NET Core",
        avatarUrl:
          "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/150px-.NET_Core_Logo.svg.png"
      },
      {
        name: "ASP.NET Boilerplate",
        avatarUrl:
          "https://pbs.twimg.com/profile_images/874854473583263744/Ctj0smnK_400x400.jpg"
      }
    ]
  }),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>
