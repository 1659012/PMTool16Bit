
<template>
  <v-container fluid grid-list-lg>
    <v-toolbar dense flat color="transparent">
      <v-dialog lazy v-model="taskGroupDialog" max-width="600px" persistent>
        <TaskGroupCreate v-if="taskGroupDialog" lazy :projectId="editedItem.id" @close="taskGroupDialog=false;loadData()"/>
      </v-dialog>

      <v-dialog lazy v-model="memberDialog" max-width="600px" persistent>
        <ProjectMemberDialog v-if="memberDialog" lazy v-model="editedItem" @close="memberDialog=false;loadData()"/>
      </v-dialog>
    </v-toolbar>

    <div style="display:flex; align-items: center; justify-content: center;">
      <span class="headline">{{editedItem.projectName}}</span>
      <v-spacer></v-spacer>
      <v-btn color="deep-purple darken-1" flat @click="goBack">
        <v-icon left dark class="mr-0">arrow_back_ios</v-icon>Go back
      </v-btn>
    </div>
    <v-tabs color="cyan" dark slider-color="yellow">
      <v-tab :key="1" ripple>Board</v-tab>
      <v-tab :key="2" ripple>Members</v-tab>
      <v-tab :key="3" ripple>Timeline</v-tab>
      <v-tab :key="4" ripple>Settings</v-tab>

      <v-tab-item :key="1">
        <div>
          <v-btn color="deep-purple darken-1" flat class="pl-0" @click="memberDialog=true;">
            <v-icon left dark class="ml-2">add_circle_outline</v-icon>Add member
          </v-btn>

          <v-btn color="deep-purple darken-1" flat class="pl-0 ml-0" @click="taskGroupDialog=true;">
            <v-icon left dark class="ml-2">add_circle_outline</v-icon>Add Task group
          </v-btn>

          <TaskGroupLoops :taskGroups.sync="editedItem.taskGroups" :loadData="loadData"/>
        </div>
      </v-tab-item>

      <v-tab-item :key="2">
        <ProjectMemberList v-model="editedItem" :loadData="loadData"/>
      </v-tab-item>

      <v-tab-item :key="3">
        <v-card flat>
          <v-card-text>time line</v-card-text>
        </v-card>
      </v-tab-item>

      <v-tab-item :key="4">
        <v-card flat>
          <v-card-text>settings</v-card-text>
        </v-card>
      </v-tab-item>
    </v-tabs>

    <!-- <code>{{editedItem}}</code> -->
  </v-container>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
import TaskGroupCreate from "../task_group/TaskGroupCreate";
import TaskGroupLoops from "../task_group/TaskGroupLoops";
import ProjectMemberList from "./project_member/ProjectMemberList";
import ProjectMemberDialog from "./project_member/ProjectMemberDialog";
export default {
  title: "Project detail",
  components: {
    TaskGroupCreate,
    TaskGroupLoops,
    ProjectMemberList,
    ProjectMemberDialog
  },
  props: [],
  data: () => ({
    editedItem: {},
    taskGroups: [],
    projectId: null,
    taskGroupDialog: false,
    memberDialog: false
  }),

  computed: {},

  watch: {},
  mounted() {
    this.initialize();
  },
  methods: {
    initialize() {
      this.projectId = this.$route.params.id;
      this.loadData();
    },
    loadData() {
      var me = this;
      me.loading = true;
      this.axios
        .get("ProjectService/GetProjectDetailById", {
          params: {
            id: this.$route.params.id
          }
        })
        .then(response => {
          if (response.data.success) {
            this.editedItem = response.data.result;
            this.taskGroups = this.editedItem.taskGroups;
          } else {
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    goBack() {
      this.$router.go(-1);
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