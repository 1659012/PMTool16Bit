
<template>
  <div>
    <v-divider></v-divider>
    <v-list class="py-0">
      <div v-for="(eventTask, eventTaskIndex) in eventTasks" :key="eventTaskIndex" @click="openEventTaskDetail(eventTask)" class="customeList pt-1">
        <v-list-tile avatar ripple>
          <v-list-tile-content>
            <v-list-tile-sub-title class="text--primary" style="display: flex;
              align-items: center;">
              <v-tooltip left>
                <template v-slot:activator="{ on }">
                  <v-icon :color="eventTask.isCompleted? 'teal accent-3': 'grey lighten-2'" v-on="on">check_circle</v-icon>
                </template>
                <span>{{eventTask.isCompleted?'Comleted':'Incomplete'}}</span>
              </v-tooltip>
              <span class="ml-1">{{ eventTask.taskName }}</span>
            </v-list-tile-sub-title>

            <v-list-tile-sub-title class="text--primary" style="display: flex;
              align-items: center;">
              <v-layout row wrap>
                <v-flex xs6 style="display: flex;
              align-items: center;">
                  <v-tooltip left>
                    <template v-slot:activator="{ on }">
                      <v-icon :color="eventTask.dueDate?'light-blue accent-1':'grey lighten-2'" v-on="on">event</v-icon>
                    </template>
                    <span>{{eventTask.dueDate? 'Due date': 'Unassign due date'}}</span>
                  </v-tooltip>

                  <span class>{{eventTask.dueDate?eventTask.dueDate:""|date}}</span>
                </v-flex>

                <v-flex xs6 style="display: flex;
              align-items: center;">
                  <v-tooltip left v-if="eventTask.eventTaskMembers.length==0">
                    <template v-slot:activator="{ on }">
                      <v-icon color="grey lighten-2" v-on="on">assignment_ind</v-icon>
                    </template>
                    <span>Unassign member</span>
                  </v-tooltip>

                  <span
                    class="ml-2"
                    :title="eventTask.eventTaskMembers.length > 0?getMemberNames(eventTask.eventTaskMembers):''"
                  >{{getMemberNames(eventTask.eventTaskMembers)|truncate(15)}}</span>
                </v-flex>
              </v-layout>
            </v-list-tile-sub-title>
          </v-list-tile-content>

          <v-list-tile-action>
            <v-list-tile-action-text>{{momentFromNow(eventTask.lastModificationTime)}}</v-list-tile-action-text>

            <div>
              <v-tooltip left>
                <template v-slot:activator="{ on }">
                  <v-icon :color="priorityLevels[eventTask.priorityLevel].color" v-on="on">{{priorityLevels[eventTask.priorityLevel].icon}}</v-icon>
                </template>
                <span>Priority level: {{priorityLevels[eventTask.priorityLevel].text}}</span>
              </v-tooltip>

              <v-tooltip right>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" :color="eventTask.isMarked? 'orange': 'grey lighten-1'">star</v-icon>
                </template>
                <span>{{eventTask.isMarked?'Bookmarked':'Bookmarke this task'}}</span>
              </v-tooltip>
            </div>
          </v-list-tile-action>
        </v-list-tile>

        <v-divider v-if="eventTaskIndex + 1 < eventTasks.length" :key="`divider-${eventTaskIndex}`"></v-divider>
        <!-- <code>{{eventTask.eventTaskMembers}}</code> -->
      </div>
    </v-list>

    <v-dialog lazy v-model="eventTaskDialog" max-width="800px" persistent>
      <EventTaskDetail
        v-if="eventTaskDialog"
        lazy
        v-model="editedItem"
        :projectId="projectId"
        :loadData="loadData"
        @close="closeEventTaskDetail"
        @cancel="cancelEventTaskDetail"
      />
    </v-dialog>
  </div>
</template>
<script>
import EventTaskDetail from "./EventTaskDetail";
import { PriorityLevels } from "../../../enum/enums";
export default {
  components: { EventTaskDetail },
  props: ["eventTasks", "loadData", "projectId"],
  data: () => ({
    eventTaskDialog: false,
    editedItem: {},
    render: false,
    priorityLevels: PriorityLevels
  }),

  computed: {},

  watch: {},
  mounted() {},
  methods: {
    openEventTaskDetail(item) {      
      this.editedItem = _.cloneDeep(item);
      this.eventTaskDialog = true;
    },
    closeEventTaskDetail() {
      this.editedItem = {};
      this.eventTaskDialog = false;
      this.loadData();
    },
    cancelEventTaskDetail() {
      this.editedItem = {};
      this.eventTaskDialog = false;
    },
    getMemberNames(eventTaskMembers) {
      if (Array.isArray(eventTaskMembers) && eventTaskMembers.length > 0) {
        var names = "";
        eventTaskMembers.forEach(item => {
          names += item.member.name + ", ";
        });
        names = names.substring(0, names.length - 2);
        return names;
      }
      return " ";
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

<style lang="css">
.v-toolbar__content {
  max-height: 50px !important;
  padding: 0 16px;
}

.customeList:hover {
  cursor: pointer;
  background: grey;
  transition: 0.2s cubic-bezier(0.4, 0, 0.2, 1);
  background-color: #f5f5f5;
}

.customAvatar {
  height: 20px !important;
  width: 20px !important;
}
</style>

<style lang="css" scope>
.customAvatar {
  height: 20px !important;
  width: 20px !important;
}
</style>