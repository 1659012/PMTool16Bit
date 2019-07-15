<template>
  <v-card flat class="card--flex-toolbar">
    <v-toolbar color="cyan" dark>
      <v-toolbar-title>
        <h3>Activities</h3>
      </v-toolbar-title>
    </v-toolbar>
    <v-card-text class="py-0">
      <v-timeline align-top dense id="dashboardActivities">
        <v-timeline-item :color="randomColor(activity.id)" small fill-dot v-for="activity in activities" :key="activity.id" class="py-0">
          <label>
            {{momentFromNow(activity.creationTime)}}
            <v-tooltip bottom>
              <template v-slot:activator="{ on }">
                <v-btn flat icon v-on="on" :to="`/ProjectDetail/${activity.projectId}`">
                  <v-icon>arrow_right</v-icon>
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
</template>

<script>
export default {
  components: {},
  data: () => ({
    activities: []
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

