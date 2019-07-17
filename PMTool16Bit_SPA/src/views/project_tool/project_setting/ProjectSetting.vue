
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
        ></v-text-field>
        <v-textarea
          name="Short description"
          label="Short description"
          v-model="editedItem.shortDescription"
          v-validate="{ max:500 }"
          :error-messages="errors.collect('Short description')"
        ></v-textarea>
        <v-btn color="blue darken-1" block dark @click.native="save">Update</v-btn>
      </v-flex>
    </v-layout>
    <!-- <code>{{value}}</code> -->
  </div>
</template>
<script>
import _ from "lodash";
export default {
  props: ["value", "loadData"],
  data: () => ({
    editedItem: {}
  }),
  computed: {},
  watch: {},
  // updated() {
  //   this.editedItem = _.cloneDeep(this.value);
  // },
  mounted() {
    this.editedItem = _.cloneDeep(this.value);
  },
  methods: {
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


