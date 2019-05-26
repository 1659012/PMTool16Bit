<template>
  <v-container grid-list-xl fluid>
    <!-- {{name}} -->
    {{fullName}}
    <v-layout row wrap>
      <v-flex lg6>
        <v-text-field v-model="name" label="Name" placeholder="Placeholder"></v-text-field>
      </v-flex>
      <v-flex lg6>
        <v-text-field v-model="firstName" label="First name" placeholder="Placeholder"></v-text-field>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Carousel from "../components/Carousel";
export default {
  components: { Carousel },
  props: [],
  data() {
    return {
      name: "han hoan",
      firstName: "Le",
      items: [1, 2, 3, 4]
    };
  },
  mounted() {
    this.initialize();
  },
  computed: {
    fullName() {
      return this.name + this.firstName;
    }
  },
  watch: {
    name() {
      console.log("name changed!");
    }
  },
  methods: {
    initialize() {
      this.loadData();
    },
    loadData() {}
  }
};
</script>

<style>
</style>
