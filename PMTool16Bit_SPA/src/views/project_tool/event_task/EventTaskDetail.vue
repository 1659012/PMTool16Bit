
<template>
  <v-card>
    <v-card-title class="grey lighten-4 py-2" style="display:flex;">
      <span class="headline">Update Event Task</span>
      <v-spacer></v-spacer>
      <v-menu bottom left lazy transition="slide-x-transition">
        <template #activator="{ on: menu }">
          <v-tooltip right>
            <template #activator="{ on: tooltip }">
              <v-btn icon v-on="{ ...menu,...tooltip }" class="ma-0">
                <v-icon color="blue-grey darken-1" medium>more_vert</v-icon>
              </v-btn>
            </template>
            <span>Task settings</span>
          </v-tooltip>
        </template>

        <v-list>
          <v-list-tile class="text--primary pa-0" @click="changeGroupdialog=true">
            <v-list-tile-title class="caption py-0 my-0">Move to other Task group</v-list-tile-title>
          </v-list-tile>
          <v-divider></v-divider>
          <v-list-tile class="text--primary pa-0" @click="deleteItem(editedItem)">
            <v-list-tile-title class="caption py-0 my-0">Delete task</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>
    </v-card-title>
    <v-container grid-list-md px-3 py-2>
      <v-layout row wrap>
        <v-flex lg12>
          <v-text-field
            v-model="editedItem.taskName"
            name="Task name"
            v-validate="{ required: true, max:256 }"
            type="text"
            label="Task name"
            :error-messages="errors.collect('Task name')"
          ></v-text-field>

          <v-textarea name="Description" label="Description" v-model="editedItem.description" v-validate="{ max:500 }"></v-textarea>

          <DateTimePicker :input="editedItem.dueDate" :output.sync="editedItem.dueDate" clearable :label="`Due date`" />

          <v-switch v-model="editedItem.isCompleted" label="Check Complete"></v-switch>

          <v-switch v-model="editedItem.isMarked" label="BookMark"></v-switch>
          <v-layout row wrap>
            <v-flex lg4>
              <v-subheader>Priority level</v-subheader>
            </v-flex>
            <v-flex lg4>
              <!-- :class="priorityLevels[editedItem.priorityLevel].color"
              class="white--text"-->
              <v-subheader>{{priorityLevels[editedItem.priorityLevel].text}}</v-subheader>
            </v-flex>
            <v-flex lg4>
              <v-slider v-model="editedItem.priorityLevel" :max="3" :min="0" :color="priorityLevels[editedItem.priorityLevel].color" thumb-label></v-slider>
              <!-- <v-rating v-model="editedItem.priorityLevel" background-color="orange lighten-3" color="orange" medium></v-rating> -->
            </v-flex>
          </v-layout>

          <TaskMemberCombobox :projectId="projectId" :defaultItems="editedItem.eventTaskMembers" :returnItems.sync="editedItem.eventTaskMembers" />
        </v-flex>
      </v-layout>
    </v-container>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="cancel">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
    </v-card-actions>

    <v-dialog lazy v-model="changeGroupdialog" max-width="600px" persistent>
      <ChangeTaskGroupDialog
        v-if="changeGroupdialog"
        lazy
        v-model="editedItem"
        :projectId="projectId"
        :loadData="loadData"
        @close="changeGroupdialog=false;loadData()"
        @cancel="changeGroupdialog=false;"
      />
    </v-dialog>
  </v-card>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
import DateTimePicker from "../../../components/basic/DateTimePicker";
import TaskMemberCombobox from "./TaskMemberCombobox";
import ChangeTaskGroupDialog from "../event_task/ChangeTaskGroupDialog";
import { PriorityLevels } from "../../../enum/enums";
export default {
  // title: "",
  components: { DateTimePicker, TaskMemberCombobox, ChangeTaskGroupDialog },
  props: ["editedItem", "projectId", "loadData"],
  data: () => ({
    priorityLevels: PriorityLevels,
    changeGroupdialog: false
  }),

  computed: {},

  watch: {},
  mounted() {},
  methods: {
    close() {
      this.$emit("close");
    },
    cancel() {
      this.$emit("cancel");
    },
    moveEventTask(eventTask) {},
    deleteItem(item) {
      this.$root.deleteItem(item, "EventTaskService/Delete", this);
      this.$notify({
        group: "message",
        type: "success",
        title: "Delete",
        text: "Data has been deleted!"
      }); //Fix dont show note in main
      this.close();
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
          this.editedItem.lastModificationTime = new Date();
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