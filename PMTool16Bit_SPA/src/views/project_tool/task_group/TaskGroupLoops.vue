
<template>
  <div>
    <v-layout row wrap>
      <v-flex lg3 v-for="(taskGroup, taskGroupIndex) in taskGroups" :key="taskGroupIndex">
        <v-hover>
          <v-card slot-scope="{ hover }" :class="`elevation-${hover ? 12 : 2}`">
            <v-card-title primary-title>
              <div>
                <h5 class="mb-0">{{taskGroup.taskGroupName}}</h5>
              </div>
            </v-card-title>
            <v-card-text>
              <div v-for="(eventTask, eventTaskIndex) in taskGroup.eventTasks" :key="eventTaskIndex">
                <code>{{taskGroup.eventTasks}}</code>
              </div>
            </v-card-text>
            <v-card-actions>
              <v-dialog lazy v-model="taskGroup.eventTaskDialog" max-width="600px" persistent>
                <EventTaskCreate
                  v-if="taskGroup.eventTaskDialog"
                  lazy
                  :taskGroupId="taskGroup.id"
                  @close="taskGroup.eventTaskDialog=false;loadData()"
                />
              </v-dialog>
              <v-btn flat color="blue" @click="taskGroup.eventTaskDialog=true;">Add task</v-btn>
              <v-btn flat color="green">Edit</v-btn>
              <v-btn flat color="orange" @click="deleteTaskGroup(taskGroup)">Delete</v-btn>
            </v-card-actions>
          </v-card>
        </v-hover>
      </v-flex>
    </v-layout>
  </div>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
import EventTaskCreate from "../event_task/EventTaskCreate";
export default {
  // title: "",
  components: { EventTaskCreate },
  props: ["taskGroups"],
  data: () => ({}),

  computed: {},

  watch: {},
  mounted() {
    this.taskGroups = this.value;
  },
  methods: {
    close(item) {
      this.$emit("close", item);
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