
<template>
  <div>
    <v-autocomplete
      :label="newLabel?newLabel:'Event task'"
      hide-no-data
      hint="Choose Event task"
      :loading="loading"
      :items="items"
      item-text="taskName"
      item-value="id"
      return-object
      v-model="model"
      clearable
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
      this.$emit("update:returnId", val ? val.id : null);
      this.$emit("update:returnObject", val);
    }
  },
  props: [
    "defaultId",
    "defaultObject",
    "returnId",
    "returnObject",
    "taskGroupId",
    "newLabel"
  ],
  mounted() {
    this.loadData();
  },
  methods: {
    loadData() {
      this.loading = true;
      let me = this;

      this.axios
        .get("EventTaskService/GetEventTaskDropdown", {
          params: {
            taskGroupId: me.taskGroupId
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