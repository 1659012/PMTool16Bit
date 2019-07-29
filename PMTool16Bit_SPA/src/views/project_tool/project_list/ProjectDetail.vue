
<template>
  <v-container fluid grid-list-lg>
    <div style="display:flex; align-items: center; justify-content: center;">
      <span class="headline">{{editedItem.projectName}}</span>
      <v-spacer></v-spacer>
      <v-btn color="deep-purple darken-1 mr-0" flat @click="goBack">
        <v-icon left dark class="mr-0">arrow_back_ios</v-icon>Go back
      </v-btn>
      <v-btn color="deep-purple darken-1 mr-0" flat @click="loadData">
        <v-icon left dark class="mr-0">refresh</v-icon>Refresh
      </v-btn>
    </div>
    <v-tabs color="cyan" dark slider-color="yellow">
      <v-tab :key="1" ripple>Board</v-tab>
      <v-tab :key="2" ripple>Members</v-tab>
      <v-tab :key="3" ripple>Timeline</v-tab>
      <v-tab :key="4" ripple>Settings</v-tab>
      <v-tab-item :key="1">
        <ProjectTaskList v-model="editedItem" :loadData="loadData" :isAdmin="isAdmin" />
      </v-tab-item>

      <v-tab-item :key="2">
        <ProjectMemberList v-model="editedItem" :loadData="loadData" :isAdmin="isAdmin" />
      </v-tab-item>

      <v-tab-item :key="3">
        <ProjectTimeLine v-if="reset" />
      </v-tab-item>

      <v-tab-item :key="4">
        <ProjectSetting v-model="editedItem" :loadData="loadData" :isProjectOwner="isProjectOwner" />
      </v-tab-item>
    </v-tabs>
  </v-container>
</template>
<script>
// import _ from "lodash";
import ProjectTaskList from "../project_task_list/ProjectTaskList";
import ProjectMemberList from "../project_member/ProjectMemberList";
import ProjectTimeLine from "../project_timeline/ProjectTimeLine";
import ProjectSetting from "../project_setting/ProjectSetting";
import projectMixin from "../../../mixin/projectMixin.js";
export default {
  title: "Project detail",
  mixins: [projectMixin],
  components: {
    ProjectTaskList,
    ProjectMemberList,
    ProjectTimeLine,
    ProjectSetting
  },
  props: [],
  data: () => ({
    editedItem: { projectMembers: null },
    taskGroups: [],
    taskGroupDialog: false,
    memberDialog: false,
    reset: true
  }),
  mounted() {
    this.initialize();
  },
  computed: {
    isAdmin() {
      return this.isAdminRole(this.editedItem.projectMembers);
    },
    isProjectOwner() {
      return this.isProjectOwnerRole(this.editedItem.projectMembers);
    }
  },
  watch: {},

  methods: {
    initialize() {
      this.loadData();
    },
    loadData() {
      this.loading = true;
      this.reset = false;
      var me = this;
      this.axios
        .get("ProjectService/Get", {
          params: {
            id: me.$route.params.id
          }
        })
        .then(response => {
          if (response.data.success) {
            this.editedItem = response.data.result;
            this.taskGroups = this.editedItem.taskGroups;
          }
        })
        .then(() => (me.reset = true))
        .catch(e => {
          this.errors.push(e);
        });
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