<template>
  <div id="pageDashboard">
    <v-container grid-list-xl fluid>
      <v-layout row wrap>
        <v-flex lg6>
          <TaskCalendar />
        </v-flex>
        <v-flex lg6>
          <ActivityTable />
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import _ from "lodash";
import TaskCalendar from "./TaskCalendar";
import ActivityTable from "./ActivityTable";
export default {
  title: "Dashboard",
  components: { ActivityTable, TaskCalendar },
  data: () => ({}),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>


