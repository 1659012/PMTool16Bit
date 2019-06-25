
<template>
  <div v-if="value">
    <v-btn color="deep-purple darken-1" flat class="pl-0" @click="dialog=true;">
      <v-icon left dark class="ml-2">add_circle_outline</v-icon>Add member
    </v-btn>

    <v-dialog lazy v-model="dialog" max-width="600px" persistent>
      <ProjectMemberDialog v-if="dialog" lazy v-model="editedItem" @close="dialog=false;loadData()"/>
    </v-dialog>

    <ProjectMemberLoop :projectMembers="editedItem.projectMembers" :loadData="loadData"/>
    <!-- <code>{{editedItem}}</code> -->
  </div>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
import ProjectMemberLoop from "./ProjectMemberLoop";
import ProjectMemberDialog from "./ProjectMemberDialog";
export default {
  title: "Project member",
  components: { ProjectMemberDialog, ProjectMemberLoop },
  props: ["value", "loadData"],
  data: () => ({
    editedItem: {},
    dialog: false
  }),

  computed: {},

  watch: {},

  updated() {
    this.editedItem = this.value;
  },

  mounted() {
    this.editedItem = this.value;
    console.log( this.$store.state.userId);
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