
<template>
  <div>
    <v-autocomplete
      label="Task group"
      hide-no-data
      hint="Choose Task group"
      :loading="loading"
      :items="items"
      item-text="taskGroupName"
      item-value="id"
      return-object
      v-model="model"
    ></v-autocomplete>
  </div>
</template>
<script>
import _ from "lodash";
export default {
  data: () => ({
    items: [],
    loading: false,
    model: {}
  }),
  components: {},
  computed: {},
  watch: {
    model(val) {
      this.$emit("update:returnId", val ? val.id : "");
      this.$emit("update:returnObject", val);
    }
  },
  props: [
    "defaultId",
    "defaultObject",
    "returnId",
    "returnObject",
    "projectId"
  ],
  mounted() {
    this.loadData();
  },
  methods: {
    loadData() {
      this.loading = true;
      let me = this;

      this.axios
        .get("TaskGroupService/GetTaskGroupDropdown", {
          params: {
            projectId: me.projectId
          }
        })
        .then(response => {
          if (response.data.success) {
            me.items = response.data.result;
            me.loading = false;
          }
        })
        .then(() => {
          if (this.defaultId) {
            this.model = this.items.find(item => item.id === this.defaultId);
          } else if (this.defaultObject) {
            this.model = this.items.find(
              item => item.id === this.defaultObject.id
            );
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    }
  }
};
</script>