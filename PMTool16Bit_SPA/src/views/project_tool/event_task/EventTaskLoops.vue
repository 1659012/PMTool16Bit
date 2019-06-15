
<template>
  <div>
    <v-divider></v-divider>
    <v-list class="py-0">
      <div v-for="(eventTask, eventTaskIndex) in eventTasks" :key="eventTaskIndex" @click="openEventTaskDetail(eventTask)" class="customeList">
        <v-list-tile avatar ripple>
          <v-list-tile-content>            
            <v-list-tile-sub-title class="text--primary">
               <v-icon :color="eventTask.isMarked? 'orange': 'grey lighten-1'" background-color="orange lighten-3" title="comleted">star_border</v-icon>
              
               <span>{{ eventTask.taskName }}</span></v-list-tile-sub-title>
            <!-- <v-list-tile-title v-text="eventTask.taskName"></v-list-tile-title> -->
            <v-list-tile-sub-title>
              <!-- <img class="customAvatar mr-2" src="../../../assets/logo.png">
              <img class="customAvatar mr-2" src="../../../assets/logo.png">-->
            </v-list-tile-sub-title>
          </v-list-tile-content>

          <v-list-tile-action>
            <v-list-tile-action-text>{{momentFromNow(eventTask.lastModificationTime)}}</v-list-tile-action-text>
            <v-icon :color="eventTask.isMarked? 'orange': 'grey lighten-1'" background-color="orange lighten-3">star_border</v-icon>
          </v-list-tile-action>
        </v-list-tile>

        <v-divider v-if="eventTaskIndex + 1 < eventTasks.length" :key="`divider-${eventTaskIndex}`"></v-divider>
        <!-- <code>{{eventTask}}</code> -->
      </div>
    </v-list>

    <v-dialog lazy v-model="eventTaskDialog" max-width="600px" persistent>
      <EventTaskDetail v-if="eventTaskDialog" lazy :editedItem="editedItem" @close="closeEventTaskDetail"/>
    </v-dialog>
  </div>
</template>
<script>
import EventTaskDetail from "./EventTaskDetail";
export default {
  components: { EventTaskDetail },
  props: ["eventTasks", "loadData"],
  data: () => ({
    eventTaskDialog: false,
    editedItem: {}
  }),

  computed: {},

  watch: {},
  mounted() {},
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