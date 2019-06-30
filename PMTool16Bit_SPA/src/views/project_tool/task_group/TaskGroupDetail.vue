
<template>
  <v-card>
    <v-card-title>
      <h4 class="headline">Update task group</h4>
    </v-card-title>
    <v-card-text>
      <v-text-field
        v-model="editedItem.taskGroupName"
        name="Task group name"
        v-validate="{ required: true, max:256 }"
        type="text"
        label="Task group name"
        :error-messages="errors.collect('Task name')"
      ></v-text-field>

      <v-textarea
        name="Short description"
        label="Short description"
        v-model="editedItem.shortDescription"
        v-validate="{ max:256 }"
        :error-messages="errors.collect('Short description')"
      ></v-textarea>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="cancel()">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
    </v-card-actions>
    <!-- <code>{{editedItem}}</code> -->
  </v-card>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
export default {
  // title: "",
  components: {},
  props: ["value"],
  data: () => ({
    editedItem: {
      taskGroupName: "",
      taskGroupId: null,
      eventTaskMembers: [],
      shortDescription: ""
    }
  }),

  computed: {},

  watch: {},
  mounted() {
    this.editedItem = this.value;
  },
  methods: {
    close() {
      this.$emit("close");
    },
    cancel() {
      this.$emit("cancel");
    },
    create() {
      this.$root.createItem(this.editedItem, "TaskGroupService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "TaskGroupService/Update", this);
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