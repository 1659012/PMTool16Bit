
<template>
  <div v-if="value">
    <v-layout row wrap>
      <v-flex lg8>
        <v-text-field
          v-model="editedItem.projectName"
          name="Project name"
          v-validate="{ required: true, max:256 }"
          type="text"
          label="Project name"
          :error-messages="errors.collect('Project name')"
          readonly
        ></v-text-field>
        <v-textarea
          name="Short description"
          label="Short description"
          v-model="editedItem.shortDescription"
          v-validate="{ max:500 }"
          :error-messages="errors.collect('Short description')"
          readonly
        ></v-textarea>
        <v-btn color="blue darken-1" block dark @click="projectDialog=true;">Update</v-btn>
      </v-flex>
    </v-layout>
    <!-- <code>{{value}}</code> -->
    <v-dialog lazy v-model="projectDialog" max-width="600px" persistent>
      <ProjectCreate v-if="projectDialog" v-model="editedItem" lazy @close="closeProjectDialog" @cancel="cancelProjectDialog" />
    </v-dialog>
  </div>
</template>
<script>
import _ from "lodash";
import ProjectCreate from "../project_list/ProjectCreate";
export default {
  props: ["value", "loadData"],
  components: { ProjectCreate },
  data: () => ({
    editedItem: { projectName: "", shortDescription: "", id: null },
    projectDialog: false
  }),
  computed: {},
  watch: {},
  updated() {
    this.editedItem = this.value;
  },
  mounted() {
    this.editedItem = this.value;
  },
  methods: {
    closeProjectDialog() {
      this.projectDialog = false;
      this.loadData();
    },
    cancelProjectDialog() {
      this.projectDialog = false;
    },
    create() {
      this.$root.createItem(this.editedItem, "ProjectService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "ProjectService/Update", this);
    },
    save() {
      this.$validator.validateAll().then(result => {
        if (result) {
          if (this.editedItem.id) {
            this.update();
            this.loadData();
          } else {
            this.create();
          }
        }
      });
    }
  }
};
</script>


