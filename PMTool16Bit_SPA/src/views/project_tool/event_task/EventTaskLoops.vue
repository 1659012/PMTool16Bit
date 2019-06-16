
<template>
  <div>
    <v-divider></v-divider>
    <v-list class="py-0">
      <div v-for="(eventTask, eventTaskIndex) in eventTasks" :key="eventTaskIndex" @click="openEventTaskDetail(eventTask)" class="customeList">
        <v-list-tile avatar ripple>
          <v-list-tile-content>
            <v-list-tile-sub-title class="text--primary" style="display: flex;
              align-items: center;">
              <v-tooltip left>
                <template v-slot:activator="{ on }">
                  <v-icon :color="eventTask.isCompleted? 'teal accent-3': 'grey lighten-4'" v-on="on">check_circle</v-icon>
                </template>
                <span>{{eventTask.isCompleted?'Comleted':'Incomplete'}}</span>
              </v-tooltip>
              <span class="ml-2">{{ eventTask.taskName }}</span>
            </v-list-tile-sub-title>

             <v-list-tile-sub-title class="text--primary" style="display: flex;
              align-items: center;">
              <v-tooltip left>
                <template v-slot:activator="{ on }">
                  <v-icon :color="priorityLevels[eventTask.priorityLevel].color" v-on="on">{{priorityLevels[eventTask.priorityLevel].icon}}</v-icon>
                </template>
                <span>Priority level: {{priorityLevels[eventTask.priorityLevel].text}}</span>
              </v-tooltip>
             
              <span class="mx-2">
                {{eventTask.dueDate?eventTask.dueDate:""|date}}
              </span>


              <v-tooltip left>
                <template v-slot:activator="{ on }">
                  <v-icon :color="priorityLevels[eventTask.priorityLevel].color" v-on="on">{{priorityLevels[eventTask.priorityLevel].icon}}</v-icon>
                </template>
                <span>Priority level: {{priorityLevels[eventTask.priorityLevel].text}}</span>              
              </v-tooltip>

                <span class="ml-2">Hoan</span>
            </v-list-tile-sub-title>
            
            <v-list-tile-sub-title>
              <!-- <img class="customAvatar mr-2" src="../../../assets/logo.png">
              <img class="customAvatar mr-2" src="../../../assets/logo.png">-->
            </v-list-tile-sub-title>
          </v-list-tile-content>

          <v-list-tile-action>
            <v-list-tile-action-text>{{momentFromNow(eventTask.lastModificationTime)}}</v-list-tile-action-text>
            <v-icon :color="eventTask.isMarked? 'orange': 'grey lighten-1'">star</v-icon>
          </v-list-tile-action>
        </v-list-tile>

        <v-divider v-if="eventTaskIndex + 1 < eventTasks.length" :key="`divider-${eventTaskIndex}`"></v-divider>
        <!-- <code>{{eventTask.priorityLevelItem}}</code> -->
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
    render:false,
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