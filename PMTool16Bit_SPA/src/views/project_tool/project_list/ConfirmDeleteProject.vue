
<template>
  <v-card v-if="value">
    <v-card-title>
      <h4 class="headline">Delete project</h4>
    </v-card-title>
    <v-card-text>
      <p>This action cannot be undone. This will permanently delete the project</p>
      <v-text-field solo label="Please type in the name of the project to confirm" hint="Capital letter is not required" v-model="projectName" ></v-text-field>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="blue darken-1" flat @click.native="cancel">Cancel</v-btn>

      <v-btn color="red darken-1" flat :disabled="projectName.toLowerCase()!==editedItem.projectName.toLowerCase()" @click.native="deleteItem(editedItem)">Delete</v-btn>
    </v-card-actions>
  </v-card>
</template>
<script>
// import _ from "lodash";
export default {
  components: {},
  props: ["value", "loadData"],
  data: () => ({
    editedItem: {projectName:" "},
    projectName: ""
  }),

  computed: {},

  watch: {},

  mounted() {
    this.editedItem = this.value;
  },

  methods: {
    close() {
      this.$emit("close");
    },
    cancel() {
      this.$emit("cancel");
    },
    deleteItem(item) {
      this.$root.deleteItem(item, "ProjectService/Delete", this);
      this.close(); 
    }
  }
};
</script>