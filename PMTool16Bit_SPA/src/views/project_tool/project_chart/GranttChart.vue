
<template>
  <div v-if="data">
    <HighchartsGantt :options="chartOptions" />
    <!-- <code>{{data}}</code> -->
    <!-- <code>{{chartOptions.series[0].data}}</code> -->
   
  </div>
</template>
<script>
// import _ from "lodash";
// import { Chart } from "highcharts-vue";

export default {
  // title: "",
  components: {},
  props: ["data"],
  data: () => ({
    chartOptions: {
      title: {
        text: "Project Progress"
      },
      subtitle: {
        // text: "Purely CSS-driven design"
      },

      series: [
        {
          name: "",
          data: [
            // {
            //   id: "Parent36",
            //   name: "sdsdsd",
            //   // start: "",
            //   // end: "",
            //   // assignee: "",
            //   // dependency: "",
            //   // parent: "",
            //   // completed: "",
            //   pointWidth: "3"
            // },
            // {
            //   id: "30",
            //   name: "sdsd",
            //   start: Date.UTC(2019, 10, 27),
            //   end: Date.UTC(2019, 10, 29),
            //   // assignee: "",
            //   // dependency: "",
            //   parent: "Parent36"
            //   // completed: "",
            // }
            {
              id: "Parent2",
              name: "taskgroup02",
              start: null,
              end: null,
              assignee: "",
              dependency: "",
              parent: "",
              completed: "",
              pointWidth: "3"
            },
            {
              id: "4",
              name: "2323saww",
              start: 1560556800000,
              end: 1561852800000,
              assignee: "admin, HoanLeader",
              dependency: "",
              parent: "Parent2",
              completed: ""
            },
            {
              id: "8",
              name: "jihknmk",
              start: 1560556800000,
              end: 1561852800000,
              assignee: "",
              dependency: "",
              parent: "Parent2",
              completed: ""
            },
            {
              id: "Parent36",
              name: "sdsdsd",
              start: null,
              end: null,
              assignee: "",
              dependency: "",
              parent: "",
              completed: "",
              pointWidth: "3"
            },
            {
              id: "30",
              name: "sdsd",
              start: 1560556800000,
              end: 1561852800000,
              assignee: "",
              dependency: "4",
              parent: "Parent36",
              completed: ""
            }
          ]
        }
      ]
    },
    // today: new Date(),
    // day: 1000 * 60 * 60 * 24,
    // testDay: Date.UTC(2019, 10, 26),
  }),
  computed: {},
  watch: {},
  mounted() {
    this.chartOptions.series[0].data = this.data;
  },
  methods: {}
};
</script>
<style>
.highcharts-credits{
  display: none!important;
}
</style>

