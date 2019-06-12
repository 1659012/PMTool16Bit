
<template>
  <v-card>
    <v-container grid-list-md>
    
    </v-container>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="close()">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
    </v-card-actions>
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
   props: ["editedItem"],
  data: () => ({
   
  }),
 
  computed: {},
 
  watch: {},
  mounted() {},
  methods: {
    close(item) {
      this.$emit("close", item);
    },
    create() {
      this.$root.createItem(this.editedItem, "AbService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "AbService/Update", this);
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