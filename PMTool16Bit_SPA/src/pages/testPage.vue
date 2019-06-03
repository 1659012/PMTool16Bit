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
    <code>{{editedItem}}</code>
  </v-container>
</template>

<script>

export default {
  components: {  },
  props: [],
  data() {
    return {
      editedItem: {}
    };
  },
  mounted() {
    this.initialize();
  },
  computed: {},
  watch: {},
  methods: {
    initialize() {
      this.loadData();
    },
    loadData() {
      let me=this;
      this.axios
        .get("http://localhost:21021/api/services/app/ProjectService/GetById?", {
          params: {
            id: 2
          }
        })
        .then(response => {
          if (response.data.success) {
            me.editedItem = response.data.result;          
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    }
  }
};
</script>

<style>
</style>
