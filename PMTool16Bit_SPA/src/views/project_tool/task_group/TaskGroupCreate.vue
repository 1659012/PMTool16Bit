
<template>
  <v-card>
     <v-card-title>
      <h4 class="headline">Create Task Group</h4>
    </v-card-title>
    <v-container grid-list-md px-3 py-2>
    <v-layout row wrap>
      <v-flex lg12>
         <v-text-field
            v-model="editedItem.taskGroupName"
            name="Task group name"
            v-validate="{ required: true, max:256 }"
            type="text"
            label="Task group name"
            :error-messages="errors.collect('Task group name')"
          ></v-text-field>
           <code>{{projectId}}</code>
      </v-flex>
    </v-layout>
    </v-container>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="close()">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
    </v-card-actions>    <!-- <code>{{editedItem}}</code> -->
   
  </v-card>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
export default {
  // title: "",
   components: {
  
  },
   props: ["projectId"],
  data: () => ({
   editedItem:{ projectId:null}
  }),
 
  computed: {},
 
  watch: {},
  mounted() {
    this.editedItem.projectId=this.projectId;
  },
  methods: {
    close(item) {
      this.$emit("close", item);
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