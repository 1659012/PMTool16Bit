
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
              <v-layout row wrap style="display: flex;
              align-items: center;">
                <v-flex xs1>
                  <v-tooltip left>
                    <template v-slot:activator="{ on }">
                      <v-icon color="light-blue accent-1" v-on="on">event</v-icon>
                    </template>
                    <span>Due date</span>
                  </v-tooltip>
                </v-flex>

                <v-flex xs5>
                  <span class="pl-2">{{eventTask.dueDate?eventTask.dueDate:""|date}}</span>
                </v-flex>

                <v-flex xs1>
                  <v-tooltip left>
                    <template v-slot:activator="{ on }">
                      <v-icon :color="eventTask.eventTaskMembers.length>0?'light-blue accent-1':'grey lighten-2'" v-on="on">assignment_ind</v-icon>
                    </template>
                    <span>{{eventTask.eventTaskMembers.length>0?'Assigned members':'Unassign'}}</span>
                  </v-tooltip>
                </v-flex>
              
                <v-flex xs5>
                  <span class="ml-2">Hoan</span>
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
                <span>Bookmark</span>
              </v-tooltip>
             
            </div>
          </v-list-tile-action>
        </v-list-tile>

        <v-divider v-if="eventTaskIndex + 1 < eventTasks.length" :key="`divider-${eventTaskIndex}`"></v-divider>
        <!-- <code>{{eventTask.eventTaskMembers}}</code> -->
      </div>
    </v-list>

    <v-dialog lazy v-model="eventTaskDialog" max-width="600px" persistent>
      <EventTaskDetail v-if="eventTaskDialog" lazy :editedItem="editedItem" @close="closeEventTaskDetail"/>
    </v-dialog>
  </div>
</template>
<script>
import EventTaskDetail from "./EventTaskDetail";
import Enums from "../../../enum/enums";
export default {
  components: { EventTaskDetail },
  props: ["eventTasks", "loadData"],
  data: () => ({
    eventTaskDialog: false,
    editedItem: {},
    render: false,
    priorityLevels: Enums.PriorityLevels
  }),

  computed: {},

  watch: {},
  mounted() {
    // this.eventTasks.forEach(eventTask => {
    //   let foundItem = this.priorityLevels.find(
    //     priorityLevel => priorityLevel.value == eventTask.priorityLevel
    //   );
    //   if (foundItem) eventTask.priorityLevelItem = foundItem;
    //   console.log(foundItem);
    // });
    // let me= this;
    //  _.delay(() => (me.render = true), 10);
  },
  methods: {
    openEventTaskDetail(item) {
      this.editedItem = Object.assign({}, item);
      this.eventTaskDialog = true;
    },
    closeEventTaskDetail() {
      this.editedItem = {};
      this.eventTaskDialog = false;
      this.loadData();
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