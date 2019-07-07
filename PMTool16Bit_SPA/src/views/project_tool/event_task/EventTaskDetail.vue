
<template>
  <v-card v-if="render">
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
          <v-list-tile class="text--primary pa-0" @click="changeGroupdialog=true; isDuplicated=true">
            <v-list-tile-title class="caption py-0 my-0">Duplicate and move to other Task group</v-list-tile-title>
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
        </v-flex>
        <v-flex lg6>
          <DateTimePicker :input="editedItem.startDate" :output.sync="editedItem.startDate" clearable label="Start date" />
        </v-flex>
        <v-flex lg6>
          <DateTimePicker :input="editedItem.dueDate" :output.sync="editedItem.dueDate" clearable label="Due date" />
        </v-flex>
        <v-flex lg3>
          <v-checkbox v-model="editedItem.isCompleted" label="Check Complete" color="success"></v-checkbox>
        </v-flex>
        <v-flex lg3>
          <v-checkbox v-model="editedItem.isMarked" label="BookMark" color="orange"></v-checkbox>
        </v-flex>
        <v-flex lg3>
          <p class="subheading text--lighten-3 mt-3">Priority level: {{priorityLevels[editedItem.priorityLevel].text}}</p>
        </v-flex>
        <v-flex lg3 class="pr-3">
          <v-slider v-model="editedItem.priorityLevel" :max="3" :min="0" :color="priorityLevels[editedItem.priorityLevel].color" thumb-label></v-slider>
        </v-flex>
        <v-flex lg12>
          <v-textarea name="Description" label="Description" v-model="editedItem.description" v-validate="{ max:500 }"></v-textarea>
        </v-flex>
        <v-flex lg12>
          <TaskMemberCombobox :projectId="projectId" :defaultItems="editedItem.eventTaskMembers" :returnItems.sync="editedItem.eventTaskMembers" />
        </v-flex>
        <v-flex lg6>
          <EventTaskDropdown
            :defaultId="editedItem.taskDependencyId"
            :returnId.sync="editedItem.taskDependencyId"
            :returnObject.sync="eventTask"
            newLabel="Task defendency"
          />
        </v-flex>
        <v-flex lg6>
          <label v-if="eventTask.taskGroupName">Task group:</label>
          <p v-if="eventTask.taskGroupName">{{eventTask.taskGroupName}}</p>
        </v-flex>
        <v-flex lg12>
          <TodoLoops v-model="editedItem.todos" />
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
        :duplicate="isDuplicated"
        @close="changeGroupdialog=false;isDuplicated=false;loadData()"
        @cancel="changeGroupdialog=false;"
      />
    </v-dialog>
    <!-- <code>{{editedItem}}</code> -->
  </v-card>
</template>
<script>
import _ from "lodash";
// import moment from "moment";
import DateTimePicker from "../../../components/basic/DateTimePicker";
import TaskMemberCombobox from "./TaskMemberCombobox";
import ChangeTaskGroupDialog from "../event_task/ChangeTaskGroupDialog";
import TodoLoops from "../todo_list/TodoLoops";
import TaskGroupDropdown from "../task_group/TaskGroupDropdown";
import EventTaskDropdown from "./EventTaskDropdown";
import { PriorityLevels } from "../../../enum/enums";
export default {
  // title: "",
  components: {
    DateTimePicker,
    TaskMemberCombobox,
    ChangeTaskGroupDialog,
    TodoLoops,
    TaskGroupDropdown,
    EventTaskDropdown
  },
  props: ["value", "projectId", "loadData"],
  data: () => ({
    priorityLevels: PriorityLevels,
    changeGroupdialog: false,
    editedItem: {},
    eventTask: {},
    render: false,
    isDuplicated: false
  }),

  computed: {},

  watch: {},
  mounted() {
    new Promise(resolve => {
      this.editedItem = _.cloneDeep(this.value);
      resolve();
    }).then(() => {
      this.render = true;
    });
  },

  methods: {
    close() {
      this.$emit("close");
    },
    cancel() {
      this.$emit("cancel");
    },
    deleteItem(item) {
      this.$root.deleteItem(item, "EventTaskService/Delete", this);
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
<style scoped>
.v-input--slider {
  margin-top: 8px;
}
</style>
