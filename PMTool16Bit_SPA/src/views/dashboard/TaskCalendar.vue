<template>
  <div style="height:100%">
    <v-calendar ref="calendar" v-model="start" :type="type" color="primary">
      <template v-slot:day="{ date }">
        <div v-if="eventsMap[date] && eventsMap[date].length <= 3">
          <template v-for="event in eventsMap[date]">
            <TaskPopover :key="event.title" :event="event" />
          </template>
        </div>

        <div v-else-if="eventsMap[date] && eventsMap[date].length > 3">
          <v-menu offset-x :close-on-content-click="false">
            <template v-slot:activator="{ on }">
              <v-btn
                color="primary"
                flat
                dark
                v-on="on"
                class="text-capitalize ml-0"
              >
                {{ eventsMap[date].length }} tasks
                <v-icon>arrow_right</v-icon>
              </v-btn>
            </template>
            <div color="red" style="background-color:white">
              <template v-for="event in eventsMap[date]">
                <TaskPopover :key="event.title" :event="event" />
              </template>
            </div>
          </v-menu>
        </div>
      </template>
    </v-calendar>
    <v-layout row wrap>
      <v-flex lg4 class="text-sm-left text-xs-center">
        <v-btn @click="$refs.calendar.prev();setPreviousMonth()">
          <v-icon dark left>keyboard_arrow_left</v-icon>Prev
        </v-btn>
      </v-flex>
      <v-flex lg4 class="text-xs-center"
        ><p class="font-weight-medium mt-3">{{ startDate | date }}</p></v-flex
      >
      <v-flex lg4 class="text-sm-right text-xs-center">
        <v-btn @click="$refs.calendar.next();setNextMonth()">
          Next
          <v-icon right dark>keyboard_arrow_right</v-icon>
        </v-btn>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import moment from "moment";
import TaskPopover from "./TaskPopover";
export default {
  components: { TaskPopover },
  data: () => ({
    events: [],
    type: "month",
    startDate: "",
    start: ""
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
        (map[e.dueDate] = map[e.dueDate] || []).push(e);
      });
      return map;
    }
  },
  watch: {},
  mounted() {
    this.loadEventTaskCalendar();
    this.startDate = moment()
      .startOf("month")
      .format("YYYY-MM-DD");
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
    },
    setPreviousMonth() {
      this.startDate = moment(this.startDate)
        // .startOf('month') //if start is not first date of month
        .subtract(1, "days")
        .startOf("month")
        .format("YYYY-MM-DD");
    },
    setNextMonth() {
      this.startDate = moment(this.startDate)
        .endOf("month")
        .add(1, "days")
        // .startOf('month') //if start is not last date of month
        .format("YYYY-MM-DD");
    }
  }
};
</script>
<style scoped></style>
