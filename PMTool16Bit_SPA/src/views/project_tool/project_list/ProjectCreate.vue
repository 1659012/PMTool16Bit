
<template>
  <v-card>
     <v-card-title>
      <h3 class="headline">Create Project</h3>
    </v-card-title>
    <v-container grid-list-md px-3 py-2>
    <v-layout row wrap>
      <v-flex lg12>
         <v-text-field
            v-model="editedItem.projectName"
            name="Project name"
            v-validate="{ required: true, max:256 }"
            type="text"
            label="Project name"
            :error-messages="errors.collect('Project name')"
          ></v-text-field>
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
// import DatePicker from "../basiccomponents/DatePicker";
export default {
  // title: "",
   components: {
  
  },
   props: ["value"],
  data: () => ({
   editedItem:{}
  }),
 
  computed: {},
 
  watch: {},
  mounted() {
    this.editedItem=this.value;
  },
  methods: {
    close(item) {
      this.$emit("close", item);
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