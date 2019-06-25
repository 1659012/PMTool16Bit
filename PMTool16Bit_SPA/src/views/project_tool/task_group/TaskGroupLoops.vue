
<template>
  <div>
    <v-layout row wrap>
      <v-flex lg3 v-for="(taskGroup, taskGroupIndex) in taskGroups" :key="taskGroupIndex">
        <v-hover>
          <v-card
            class="rounded-card"
            slot-scope="{ hover }"
            :class="`elevation-${hover ? 12 : 2}`"
          >
            <v-toolbar flat px-1>
              <v-toolbar-title
                class="subheading"
                style="cursor: pointer;"
                @click="openTaskGroupDetail(taskGroup)"
              >{{taskGroup.taskGroupName}}</v-toolbar-title>
              <v-spacer></v-spacer>

              <v-menu bottom left lazy transition="slide-x-transition">
                <template v-slot:activator="{ on }">
                  <v-btn icon v-on="on" class="mr-0">
                    <v-icon color="blue-grey darken-1">arrow_right</v-icon>
                  </v-btn>
                </template>

                <v-list>
                  <v-list-tile class="text--primary pa-0" @click="taskGroup.eventTaskDialog=true;">
                    <v-list-tile-title class="caption py-0 my-0">Task group detail</v-list-tile-title>
                  </v-list-tile>
                  <v-divider></v-divider>
                  <v-list-tile class="text--primary pa-0" @click="deleteTaskGroup(taskGroup)">
                    <v-list-tile-title class="caption py-0 my-0">Delete task group</v-list-tile-title>
                  </v-list-tile>
                </v-list>
              </v-menu>
            </v-toolbar>
            <EventTaskLoops :eventTasks="taskGroup.eventTasks" :projectId="taskGroup.projectId" :loadData="loadData"/>
            <v-divider></v-divider>
            <v-card-actions class="py-0">
              <v-tooltip right>
                <template v-slot:activator="{ on }">
                  <v-btn
                    fab
                    flat
                    small
                    color="primary"
                    v-on="on"
                    @click="taskGroupId=taskGroup.id;eventTaskDialog=true;"
                  >
                    <v-icon dark>add</v-icon>
                  </v-btn>
                </template>
                <span>Add task</span>
              </v-tooltip>
            </v-card-actions>
          </v-card>
        </v-hover>
      </v-flex>
    </v-layout>
    <v-dialog lazy v-model="eventTaskDialog" max-width="600px" persistent>
      <EventTaskCreate
        v-if="eventTaskDialog"
        lazy
        :taskGroupId="taskGroupId"
        @close="eventTaskDialog=false;taskGroupId=null;loadData()"
      />
    </v-dialog>
    <v-dialog lazy v-model="taskGroupDetailDialog" max-width="600px" persistent>
      <TaskGroupDetail
        v-if="taskGroupDetailDialog"
        lazy
        v-model="taskGroup"
        @close="taskGroupDetailDialog=false;loadData()"
      />
    </v-dialog>
  </div>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
import EventTaskCreate from "../event_task/EventTaskCreate";
import EventTaskLoops from "../event_task/EventTaskLoops";
import TaskGroupDetail from "./TaskGroupDetail";
export default {
  // title: "",
  components: { EventTaskCreate, EventTaskLoops, TaskGroupDetail },
  props: ["taskGroups", "loadData"],
  data: () => ({
    eventTaskDialog: false,
    taskGroupId: null,
    taskGroupDetailDialog: false,
    taskGroup:{}
  }),

  computed: {},

  watch: {},
  mounted() {
    this.taskGroups = this.value;
  },
  methods: {
    openTaskGroupDetail(taskGroup){
      this.taskGroupDetailDialog=true;
      this.taskGroup=taskGroup;
    },
    deleteTaskGroup(item) {
      this.$root.deleteItem(item, "TaskGroupService/Delete", this);
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

