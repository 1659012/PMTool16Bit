
<template>
  <v-card>
    <v-card-title>
      <h4 class="headline">Update Event Task</h4>
    </v-card-title>
    <v-container grid-list-md px-3 py-2>
      <v-layout row wrap>
        <v-flex lg12>
          <v-text-field
            v-model="editedItem.taskName"
            name="Task name"
            v-validate="{ required: true, max:256 }"
            type="text"
            label="Task name"
            :error-messages="errors.collect('Task name')"
          ></v-text-field>

          <v-textarea name="Description" label="Description" v-model="editedItem.description" v-validate="{ max:256 }"></v-textarea>

          <DateTimePicker :input="editedItem.dueDate" :output.sync="editedItem.dueDate" :label="`Due date`"/>
          
          <v-switch v-model="editedItem.isCompleted" label="isCompleted"></v-switch>

           <v-switch v-model="editedItem.isMarked" label="BookMark"></v-switch>

          <UserCombobox :defaultItems="editedItem.eventTaskMembers" :returnItems.sync="editedItem.eventTaskMembers"/>

        </v-flex>
      </v-layout>
    </v-container>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="close()">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
    </v-card-actions>
    <!-- <code>{{editedItem}}</code> -->
  </v-card>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
import DateTimePicker from "../../../components/basic/DateTimePicker";
import UserCombobox from "../../../components/combobox/UserCombobox";
export default {
  // title: "",
  components: {DateTimePicker ,UserCombobox},
  props: ["editedItem"],
  data: () => ({}),

  computed: {},

  watch: {},
  mounted() {},
  methods: {
    close(item) {
      this.$emit("close", item);
    },
    create() {
      this.$root.createItem(this.editedItem, "EventTaskService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "EventTaskService/Update", this);
    },
    save() {
      this.$validator.validateAll().then(result => {
        if (result) {

          this.editedItem.lastModificationTime=new Date();
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