
<template>
  <div v-if="value">
    <v-dialog lazy v-model="taskGroupDialog" max-width="600px" persistent>
      <TaskGroupCreate
        v-if="taskGroupDialog"
        lazy
        :projectId="editedItem.id"
        @close="taskGroupDialog=false; loadData()"
        @cancel="taskGroupDialog=false;"
      />
    </v-dialog>

    <v-dialog lazy v-model="memberDialog" max-width="600px" persistent>
      <ProjectMemberDialog
        v-if="memberDialog"
        lazy
        v-model="editedItem"
        @close="memberDialog=false;loadData()"
        @cancel="memberDialog=false;"       
      />
    </v-dialog>

    <div style="display:flex;">
      <v-btn color="deep-purple darken-1" flat class="pl-0" @click="memberDialog=true;" :disabled="!isAdmin" :title="isAdmin?'':'Only Project owner can add members'">
        <v-icon left dark class="ml-2">add_circle_outline</v-icon>Add member
      </v-btn>

      <v-btn color="deep-purple darken-1" flat class="pl-0 ml-0" @click="taskGroupDialog=true;">
        <v-icon left dark class="ml-2">add_circle_outline</v-icon>Add Task group
      </v-btn>

      <v-btn color="deep-purple darken-1" flat class="pl-0 ml-0" @click="downloadTaskListExcel(editedItem.id)">
        <v-icon left dark class="ml-2">cloud_download</v-icon>Down Task List
      </v-btn>
      <v-spacer></v-spacer>

      <!-- <v-select
        :items="taskOptions"
        v-model="filter.taskType"
        label="Chose tasks"
        item-text="text"
        item-value="value"
        clearable
        style="width:250px!important;
        max-width:250px !important;"
      ></v-select>-->

      <v-divider vertical></v-divider>

      <v-tooltip bottom>
        <template v-slot:activator="{ on }">
          <v-btn :color="gridView ?'primary':'grey'" dark v-on="on" icon flat class="mx-1" @click="gridView=true">
            <v-icon>apps</v-icon>
          </v-btn>
        </template>
        <span>Grid View</span>
      </v-tooltip>

      <v-tooltip bottom>
        <template v-slot:activator="{ on }">
          <v-btn :color="!gridView ?'primary':'grey'" dark v-on="on" icon flat class="mr-1" @click="gridView=false">
            <v-icon>format_list_bulleted</v-icon>
          </v-btn>
        </template>
        <span>List View</span>
      </v-tooltip>
    </div>
    <TaskGroupLoops v-model="editedItem" :loadData="loadData" :isAdmin="isAdmin" :changeView="gridView" />
    <!-- <code>{{editedItem.taskGroups}}</code> -->
  </div>
</template>
<script>
import _ from "lodash";
import TaskGroupLoops from "../task_group/TaskGroupLoops";
import TaskGroupCreate from "../task_group/TaskGroupCreate";
import ProjectMemberDialog from "../project_member/ProjectMemberDialog";
export default {
  components: {
    TaskGroupCreate,
    TaskGroupLoops,
    ProjectMemberDialog
  },
  props: ["value", "loadData", "isAdmin"],
  data: () => ({
    editedItem: {},
    taskGroups: [],
    eventTasks: [],
    projectId: null,
    taskGroupDialog: false,
    memberDialog: false,
    gridView: true,
    filter: { taskType: "", taskPriorityLevel: "" },
    taskOptions: [
      { text: "All tasks", value: "all" },
      { text: "Incomplete tasks", value: "incomplete" },
      { text: "Completed tasks", value: "completed" }
      // { text: "Just my task", value: "only" },
      // { text: "Not Assigned", value: "noAssigned" },
      // { text: "Already assigned", value: "assigned" }
    ]
  }),

  computed: {},

  watch: {},
  mounted() {
    this.editedItem = this.value;
    this.taskGroups = this.value.taskGroups;
  },
  updated() {
    this.editedItem = this.value;
    this.taskGroups = this.value.taskGroups;
  },
  methods: {
    downloadTaskListExcel(projectId) {
      window.location =
        this.$store.state.baseUrl +
        "api/services/app/FileService/ExportTasksInProject?projectId=" +
        projectId;
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
<style lang="css" scoped>
.v-text-field__details {
  display: none !important;
}
.v-messages.theme--light {
  display: none !important;
}
.v-messages__wrapper {
  display: none !important;
}
</style>
