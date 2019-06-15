
<template>
  <v-container fluid grid-list-lg>
    <v-toolbar dense flat color="transparent">
      <v-dialog lazy v-model="taskGroupDialog" max-width="600px" persistent>
        <TaskGroupCreate v-if="taskGroupDialog" lazy :projectId="editedItem.id" @close="close"/>
      </v-dialog>
    </v-toolbar>
    <div>
      <h3 class="headline">{{editedItem.projectName}}</h3>
       <v-btn color="deep-purple darken-1" flat class="pl-0" @click="taskGroupDialog=true;">
          <v-icon left dark class>add_circle_outline</v-icon>Add Task group
        </v-btn>
    </div>

    <TaskGroupLoops :taskGroups.sync="editedItem.taskGroups"/>
   
    <!-- <code>{{editedItem}}</code> -->
  </v-container>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
import TaskGroupCreate from "./TaskGroupCreate";
import EventTaskCreate from "./EventTaskCreate";
import TaskGroupLoops from "../task_group/TaskGroupLoops";
export default {
  title: "Project detail",
  components: { TaskGroupCreate, EventTaskCreate, TaskGroupLoops },
  props: [],
  data: () => ({
    editedItem: {},
    taskGroups: [],
    projectId: null,
    taskGroupDialog: false,
    eventTaskDialog: false
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
    close(item) {
      this.loadData();
      this.taskGroupDialog = false;
    },
    deleteTaskGroup(item) {
      this.$root.deleteItem(item, "TaskGroupService/Delete", this);
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