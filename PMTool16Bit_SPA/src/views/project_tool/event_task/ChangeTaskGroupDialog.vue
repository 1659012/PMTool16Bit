
<template>
  <v-card v-if="value">
    <v-card-title>
      <h4 class="headline">Change Task group</h4>
    </v-card-title>
    <v-container grid-list-md px-3 py-2>
      <v-layout row wrap>
        <v-flex lg12>
          <TaskGroupDropdown v-if="value" :defaultId="editedItem.taskGroupId" :returnId.sync="editedItem.taskGroupId" :projectId="projectId" />
        </v-flex>
      </v-layout>
    </v-container>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="cancel">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
    </v-card-actions>
    <code>{{editedItem}}</code>
  </v-card>
</template>
<script>
import _ from "lodash";
import TaskGroupDropdown from "../task_group/TaskGroupDropdown";
export default {
  // title: "",
  components: { TaskGroupDropdown },
  props: ["value", "loadData", "projectId", "duplicate"],
  data: () => ({
    editedItem: {}
  }),

  computed: {},

  watch: {},

  mounted() {
    this.editedItem = _.cloneDeep(this.value);
    if (this.duplicate) {
      delete this.editedItem.id;
    }
  },

  methods: {
    close() {
      this.$emit("close");
    },
    cancel() {
      this.$emit("cancel");
    },
    create() {
      this.$root.createItem(this.editedItem, "EventTaskService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "EventTaskService/Update", this);
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