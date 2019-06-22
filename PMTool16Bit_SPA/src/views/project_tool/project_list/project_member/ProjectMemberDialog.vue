
<template>
  <v-card v-if="value">
    <v-card-title>
      <h4 class="headline">Edit project member</h4>
    </v-card-title>
    <v-container grid-list-md px-3 py-2>
      <v-layout row wrap>
        <v-flex lg12>
           <ProjectMemberCombobox v-if="editedItem.projectMembers" :defaultItems="editedItem.projectMembers" :returnItems.sync="editedItem.projectMembers"/>
        </v-flex>
      </v-layout>
    </v-container>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="close()">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
    </v-card-actions>
    <!-- <code>{{editedItem.projectMembers}}</code> -->
  </v-card>
</template>
<script>
import _ from "lodash";
import ProjectMemberCombobox from "./ProjectMemberCombobox";
export default {
  // title: "",
  components: { ProjectMemberCombobox },
  props: ["value"],
  data: () => ({
    editedItem:{}
  }),

  computed: {},

  watch: {},

  mounted() {
    this.editedItem =_.cloneDeep(this.value);
  },

  methods: {
    close(item) {
      this.$emit("close", item);
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
          } else {
            this.create();
          }
        }
      });
    }
  }
};
</script>