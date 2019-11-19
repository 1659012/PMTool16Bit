<template>
  <v-container fluid>
    <h3>About me</h3>
    <v-layout row wrap dense>
      <v-flex lg4 class="text-xs-center">
        <v-avatar size="300">
          <img
            src="../../../public/static/avatar/me.jpg"
            alt="About me"
            style="object-fit: cover;"
          />
        </v-avatar>
      </v-flex>
      <v-flex lg8>
        <v-card>
          <v-card-title class="headline">Personal Info</v-card-title>
          <v-card-text>
            <p>
              <v-icon color="blue-grey darken-2">account_box</v-icon>
              Le Nguyen Han Hoan
            </p>
            <p>
              <v-icon color="blue-grey darken-2">phone</v-icon>
              098 535 8948
            </p>
            <p>
              <v-icon color="blue-grey darken-2">email</v-icon>
              lenguyenhanhoan@gmail.com
            </p>
            <p>
              <v-icon color="blue-grey darken-2">home</v-icon>
              Binh Tan Dist, HCM City
            </p>
            <p>
              <v-icon color="blue-grey darken-2">account_balance</v-icon>
              University of Science Ho Chi Minh City
            </p>
            <p>
              <v-icon color="blue-grey darken-2">library_books</v-icon>
              Major: Computer science -IT Service Science
            </p>
            <p>
              <v-icon color="blue-grey darken-2">school</v-icon>
              Other: Bachelor of Foreign Trade university- Banking and Finance
            </p>
          </v-card-text>
        </v-card>
      </v-flex>
      <v-flex lg4> </v-flex>
      <v-flex lg8 class="mt-3">
        <v-card>
          <v-card-title class="headline">Skills</v-card-title>
          <v-card-text>
            <p>
              <v-icon color="blue-grey darken-2">language</v-icon>
              Code languages: C#, PHP, Javascript
            </p>
            <p>
              <v-icon color="blue-grey darken-2">favorite</v-icon>
              Interest in: .Net, Angular, Vuejs, Full-stack
            </p>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
export default {
  title: "About me",
  components: {},
  data: () => ({}),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>
