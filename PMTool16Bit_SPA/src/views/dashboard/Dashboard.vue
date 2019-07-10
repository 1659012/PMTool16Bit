<template>
  <div id="pageDashboard">
    <v-container grid-list-xl fluid>
      <v-layout row wrap>
        <v-flex lg6>
          <v-calendar ref="calendar" v-model="start" :type="type" :end="end" color="primary"></v-calendar>
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
        </v-flex>
        <v-flex lg6>
          <v-card flat class="card--flex-toolbar">
            <v-toolbar color="cyan" dark>
              <v-toolbar-title>
                <h3>Activities</h3>
              </v-toolbar-title>
            </v-toolbar>
            <v-card-text class="py-0">
              <v-timeline align-top dense id="dashboardActivities">
                <v-timeline-item
                  :color="randomColor(activity.id)"
                  small
                  fill-dot
                  v-for="activity in activities"
                  :key="activity.id"
                >
                  <label>
                    {{momentFromNow(activity.creationTime)}}
                    <v-tooltip bottom>
                      <template v-slot:activator="{ on }">
                        <v-btn flat icon v-on="on" :to="`/ProjectDetail/${activity.projectId}`">
                          <v-icon >arrow_right</v-icon>
                        </v-btn>
                      </template>
                      <span>To project detail</span>
                    </v-tooltip>
                  </label>
                  <p class="body-1 font-weight-light">{{activity.description}}</p>
                </v-timeline-item>
              </v-timeline>
            </v-card-text>
          </v-card>
          <!-- <code>{{activities}}</code> -->
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import _ from "lodash";
export default {
  title: "Dashboard",
  components: {},
  data: () => ({
    activities: [],
    type: "month",
    start: "2019-07-01",
    end: "2019-07-31",
    typeOptions: [
      { text: "Day", value: "day" },
      { text: "4 Day", value: "4day" },
      { text: "Week", value: "week" },
      { text: "Month", value: "month" },
      { text: "Custom Daily", value: "custom-daily" },
      { text: "Custom Weekly", value: "custom-weekly" }
    ]
  }),
  computed: {},
  watch: {},
  mounted() {
    this.loadActivities();
  },
  methods: {
    randomColor(index = 0) {
      let colorList = [
        "purple lighten-2",
        "amber lighten-1",
        "cyan lighten-1",
        "red lighten-1",
        "green lighten-1"
      ];

      // let random= Math.floor(Math.random() * (colorList.length  + 1));
      // return colorList[random];
      let random = index % (colorList.length - 1);
      return colorList[random];
    },
    loadActivities() {
      let me = this;
      this.axios
        .get("ProjectActivityService/GetAllRecentActivities", {
          params: {
            maxResultCount: 5
          }
        })
        .then(response => {
          if (response.data.success) {
            me.activities = response.data.result;
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    }
  }
};
</script>
<style>
#dashboardActivities .v-timeline-item__inner-dot {
  height: 12px !important;
  width: 12px !important;
  margin: 6px !important;
}
#dashboardActivities .v-timeline-item__dot.v-timeline-item__dot--small {
  box-shadow: none !important;
  background-color: transparent !important;
}
</style>

