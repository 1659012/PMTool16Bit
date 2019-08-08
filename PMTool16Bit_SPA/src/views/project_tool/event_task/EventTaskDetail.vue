
<template>
  <div>
    <v-card v-if="editedItem">
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
      <v-container grid-list-md px-3 py-2 id="eventTaskBody">
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
            <p class="subheading text--lighten-3 mt-3">
              <span class="font-weight-light">Priority level:</span>
              {{priorityLevels[editedItem.priorityLevel].text}}
            </p>
          </v-flex>
          <v-flex lg3 class="pr-3">
            <v-slider v-model="editedItem.priorityLevel" :max="3" :min="0" :color="priorityLevels[editedItem.priorityLevel].color" thumb-label></v-slider>
          </v-flex>
          <v-flex lg12>
            <v-textarea name="Description" label="Description" v-model="editedItem.description" v-validate="{ max:500 }" rows="2"></v-textarea>
          </v-flex>
          <v-flex lg12>
            <TaskMemberCombobox :projectId="projectId" :defaultItems="editedItem.eventTaskMembers" :returnItems.sync="editedItem.eventTaskMembers" />
          </v-flex>
          <v-flex lg6>
            <EventTaskDropdown
              :defaultId="editedItem.taskDependencyId"
              :returnId.sync="editedItem.taskDependencyId"
              :returnObject.sync="dependencyEventTask"
              :projectId="projectId"
              newLabel="Task dependency"
            />
          </v-flex>
          <v-flex lg6>
            <div v-if="editedItem.taskDependencyId">
              <label class="caption">Task group</label>
              <p>{{dependencyEventTask.taskGroupName}}</p>
            </div>
          </v-flex>
          <v-flex lg12>
            <h5>Documents</h5>
            <MultiUploader v-model="editedItem.fileIds" />
          </v-flex>
          <v-flex lg12>
            <TodoLoops v-model="editedItem.todos" />
          </v-flex>
        </v-layout>
      </v-container>
      <v-card-actions style="background-color: #f5f5f5 !important;">
        <v-spacer></v-spacer>
        <v-btn color="red darken-1" flat @click.native="cancel">Cancel</v-btn>
        <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
      </v-card-actions>
    </v-card>
    <v-dialog lazy v-model="changeGroupdialog" max-width="600px" persistent>
      <ChangeTaskGroupDialog
        v-if="changeGroupdialog"
        lazy
        v-model="editedItem"
        :projectId="projectId"
        :loadData="loadData"
        :duplicate="isDuplicated"
        @close="changeGroupdialog=false;isDuplicated=false;close();"
        @cancel="changeGroupdialog=false;"
      />
    </v-dialog>
  </div>
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
import MultiUploader from "../../../components/upload_file/MultiUploader";
import { PriorityLevels } from "../../../enum/enums";
export default {
  // title: "",
  components: {
    DateTimePicker,
    TaskMemberCombobox,
    ChangeTaskGroupDialog,
    TodoLoops,
    TaskGroupDropdown,
    EventTaskDropdown,
    MultiUploader
  },
  props: [
    "value",
    "eventTaskId",
    "projectId",
    "loadData",
    "setReadonly",
    "isAdmin"
  ],
  data: () => ({
    priorityLevels: PriorityLevels,
    changeGroupdialog: false,
    editedItem: null,
    duplicatedItem: {},
    dependencyEventTask: {},
    isDuplicated: false
  }),

  computed: {},

  watch: {},
  mounted() {
    if (this.value) {
      this.editedItem = _.cloneDeep(this.value);
    } else if (this.eventTaskId) {
      this.getDetailEventTask(this.eventTaskId);
    }
  },

  methods: {
    getDetailEventTask(eventTaskId) {
      this.loading = true;
      var me = this;
      this.axios
        .get("EventTaskService/Get", {
          params: {
            id: eventTaskId
          }
        })
        .then(response => {
          if (response.data.success) {
            this.editedItem = response.data.result;
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
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
#eventTaskBody {
  max-height: 75vh;
  overflow-y: auto;
}
#eventTaskBody::-webkit-scrollbar-track {
  box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
  -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
  background-color: #f5f5f5;
}

#eventTaskBody::-webkit-scrollbar {
  width: 12px;
  background-color: #f5f5f5;
}

#eventTaskBody::-webkit-scrollbar-thumb {
  background-color: #bdbdbd;
}
</style>
