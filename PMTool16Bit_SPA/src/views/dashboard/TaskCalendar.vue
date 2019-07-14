<template>
  <div style="height:100%">
    <v-calendar ref="calendar" v-model="startDate" :type="type" :end="endDate" color="primary">
      <template v-slot:day="{ date }">
        <div v-if="eventsMap[date]&&eventsMap[date].length<=3">
          <template v-for="event in eventsMap[date]">
            <TaskPopover :key="event.title" :event="event" />
          </template>
        </div>

        <div v-else-if="eventsMap[date] && eventsMap[date].length>3">
          <v-menu offset-x :close-on-content-click="false">
            <template v-slot:activator="{ on }">
              <v-btn color="primary" flat dark v-on="on" class="text-capitalize ml-0">{{eventsMap[date].length}} tasks
                 <v-icon>arrow_right</v-icon>
              </v-btn>
            </template>
            <template v-for="event in eventsMap[date]">
              <TaskPopover :key="event.title" :event="event" />
            </template>
          </v-menu>
        </div>
      </template>
    </v-calendar>
    <v-layout row wrap>
      <v-flex lg4 class="text-sm-left text-xs-center">
        <v-btn @click="$refs.calendar.prev()">
          <v-icon dark left>keyboard_arrow_left</v-icon>Prev
        </v-btn>
      </v-flex>
      <v-flex lg4 class="text-xs-center">
        <!-- <v-select v-model="type" :items="typeOptions" label="Type"></v-select> -->
      </v-flex>
      <v-flex lg4 class="text-sm-right text-xs-center">
        <v-btn @click="$refs.calendar.next()">
          Next
          <v-icon right dark>keyboard_arrow_right</v-icon>
        </v-btn>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import TaskPopover from "./TaskPopover";
export default {
  components: { TaskPopover },
  data: () => ({
    events: [],
    type: "month",
    startDate: "2019-07-01",
    endDate: "2019-07-31",
    typeOptions: [
      { text: "Day", value: "day" },
      { text: "4 Day", value: "4day" },
      { text: "Week", value: "week" },
      { text: "Month", value: "month" },
      { text: "Custom Daily", value: "custom-daily" },
      { text: "Custom Weekly", value: "custom-weekly" }
    ]
  }),
  computed: {
    getToday() {
      return this.toDateString(new Date());
    },
    eventsMap() {
      const map = {};
      this.events.forEach(e => {
        e.startDate = e.startDate.slice(0, 10);
        e.dueDate = e.dueDate.slice(0, 10);
        // e.open = false;
        (map[e.dueDate] = map[e.dueDate] || []).push(e);
      });
      return map;
    }
  },
  watch: {},
  mounted() {
    this.loadEventTaskCalendar();
    this.startDate = this.getToday;
  },
  methods: {
    loadEventTaskCalendar() {
      let me = this;
      this.axios
        .get("EventTaskService/GetEventTaskCalendar")
        .then(response => {
          if (response.data.success) {
            me.events = response.data.result;
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    }
  }
};
</script>
<style  scoped>
</style>



