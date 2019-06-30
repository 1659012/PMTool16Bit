
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
      <ProjectMemberDialog v-if="memberDialog" lazy v-model="editedItem" @close="memberDialog=false;loadData()" @cancel="memberDialog=false;" />
    </v-dialog>

    <v-btn color="deep-purple darken-1" flat class="pl-0" @click="memberDialog=true;">
      <v-icon left dark class="ml-2">add_circle_outline</v-icon>Add member
    </v-btn>

    <v-btn color="deep-purple darken-1" flat class="pl-0 ml-0" @click="taskGroupDialog=true;">
      <v-icon left dark class="ml-2">add_circle_outline</v-icon>Add Task group
    </v-btn>

    <v-btn color="deep-purple darken-1" flat class="pl-0 ml-0" @click="downloadTaskListExcel(editedItem.id)">
      <v-icon left dark class="ml-2">cloud_download</v-icon>Down Task List
    </v-btn>

    <TaskGroupLoops v-model="editedItem" :loadData="loadData"/>
    
  </div>
</template>
<script>
// import _ from "lodash";
import TaskGroupLoops from "../../task_group/TaskGroupLoops";
import TaskGroupCreate from "../../task_group/TaskGroupCreate";
import ProjectMemberDialog from "../project_member/ProjectMemberDialog";
import projectMixin from "../../../../mixin/projectMixin.js";
export default {
  // title: "Project detail",
  mixins: [projectMixin],
  components: {
    TaskGroupCreate,
    TaskGroupLoops,
    ProjectMemberDialog
  },
  props: [
    "value",
    "loadData",
  
  ],
  data: () => ({
    editedItem:{},
    taskGroups: [],
    projectId: null,
    taskGroupDialog: false,
    memberDialog: false
  }),

  computed: {},

  watch: {},
  mounted() {
    this.editedItem = this.value;
  },
  updated() {
    this.editedItem = this.value;
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