
<template>
  <v-container fluid>
    <h3>About Project</h3>
    <v-layout row wrap>
      <v-flex lg12>
        <div class="text-xs-center">
          <h2>Tools</h2>
        </div>
      </v-flex>
      <v-flex lg2 v-for="(tool, index) in tools" :key="index">
        <v-img :aspect-ratio="1" :src="tool.avatarUrl" class="ma-3"></v-img>
        <v-layout align-center column justify-center>
          <h4 class="display-1 font-weight-thin mb-4">{{tool.name}}</h4>
          <!-- <h4 class="subheading">Build your application today!</h4> -->
        </v-layout>
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
        avatarUrl: "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/150px-.NET_Core_Logo.svg.png"
      },
        {
        name: "ASP.NET Boilerplate",
        avatarUrl: "https://pbs.twimg.com/profile_images/874854473583263744/Ctj0smnK_400x400.jpg"
      }
    ]
  }),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>
