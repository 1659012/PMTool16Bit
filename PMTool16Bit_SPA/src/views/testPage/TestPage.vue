<template>
  <v-container grid-list-xl fluid>
    {{fullName}}
    <v-layout row wrap>
      <v-flex lg6>
        <v-text-field v-model="firstName" label="Name" placeholder="Placeholder"></v-text-field>
      </v-flex>
      <v-flex lg6>
        <v-text-field v-model="surName" label="Family name" placeholder="Placeholder"></v-text-field>
      </v-flex>
    </v-layout>
    <code>{{editedItem}}</code>
  </v-container>
</template>

<script>
export default {
  components: {},
  props: [],
  data() {
    return {
      firstName: "",
      surName: "",
      editedItem: {}
    };
  },
  mounted() {
    this.initialize();
  },
  computed: {
    fullName() {
      return this.firstName + " " + this.surName;
    }
  },
  watch: {
    firstName(val) {
      console.log(val);
      this.$notify({
        group: "message",
        duration: 5000,
        type: "success",
        title: "Note",
        text: val
      });
    }
  },
  methods: {
    initialize() {
      // this.loadData();
       this.$notify({
        group: "message",
        duration: 3000,
        type: "success",
        title: "Note",
        text: "initialize"
      });
    },
    loadData() {
      let me = this;
      this.axios
        .get(
          "http://localhost:21021/api/services/app/ProjectService/GetById?",
          {
            params: {
              id: 2
            }
          }
        )
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
