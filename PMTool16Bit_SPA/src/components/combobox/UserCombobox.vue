
<template>
  <div>
    <v-combobox v-model="model" :items="items" label="Assign member(s)" item-text="fullName" item-value="id" multiple chips clearable>
      <template slot="selection" slot-scope="data">
        <v-chip :key="JSON.stringify(data.item)" :selected="data.selected" :disabled="data.disabled" class="v-chip--select-multi">
          <v-avatar class="accent white--text">{{ data.item.fullName.slice(0, 1).toUpperCase() }}</v-avatar>
          {{ data.item.fullName }}
          <v-icon class="ml-3" color="blue-grey" title="Remove item" small @click.prevent="removeItem(data.index)">close</v-icon>
        </v-chip>
      </template>
    </v-combobox>
  </div>
</template>
<script>
import _ from "lodash";
export default {
  components: {},
  props: ["defaultItems", "returnItems"],
  data() {
    return {
      items: [],
      model: []
    };
  },
  computed: {},
  watch: {
    model(value) {
      if (Array.isArray(value)) {
        let result = [];
        value.forEach(user => {
          let item = { memberId: null };
          item.memberId = user.id;
          result.push(item);
        });

        this.$emit("update:returnItems", result);
      }
    }
  },

  mounted() {
    this.loadData();
  },
  methods: {
    loadData() {
      let me = this;
      me.loading = true;
      this.axios
        .get("User/GetDropdown", {})
        .then(response => {
          if (response.data.success) {
            me.items = response.data.result;
          }
        })
        .then(() => {
          this.loadDefaultItems();
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    loadDefaultItems() {
      if (Array.isArray(this.defaultItems) && this.defaultItems.length > 0) {
        this.defaultItems.forEach(object => {
          let foundItem = this.items.find(item => item.id == object.memberId);
          if (foundItem) {
            this.model.push(foundItem);
          }
        });
      }
    },
    removeItem(index) {
      this.model.splice(index, 1);
    }
  }
};
</script>