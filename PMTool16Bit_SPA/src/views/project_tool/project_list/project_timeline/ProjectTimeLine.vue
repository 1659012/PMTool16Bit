
<template>
  <div>
    <GranttChart v-if="reset" :data="chartData" />
  </div>
</template>
<script>
// import _ from "lodash";
import projectMixin from "../../../../mixin/projectMixin.js";
import GranttChart from "../../project_chart/GranttChart";
export default {
  // title: "Project detail",
  mixins: [projectMixin],
  components: { GranttChart },
  props: [],
  data: () => ({
    chartData: [],
    reset: false
  }),
  computed: {},
  watch: {},
  mounted() {
    this.loadGanttChartData();
  },
  methods: {
    loadGanttChartData() {
      this.reset = false;
      var me = this;
      this.axios
        .get("ProjectService/GetGanttChartData", {
          params: {
            projectId: this.$route.params.id
          }
        })
        .then(response => {
          if (response.data.success) {
            this.chartData = response.data.result;
          }
        })
        .then(() => {
          this.chartData.forEach(item => {
            if (item.parent) {
              delete item.pointWidth;
            }
          });
        })
        .then(() => {
          this.reset = true;
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    create() {
      this.$root.createItem(this.editedItem, "ProjectService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "ProjectService/Update", this);
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