
<template>
  <div v-if="reset">
    <v-layout row wrap>
      <v-flex lg6>
        <h3>Project info</h3>
        <v-text-field
          v-model="editedItem.projectName"
          name="Project name"
          v-validate="{ required: true, max:256 }"
          type="text"
          label="Project name"
          :error-messages="errors.collect('Project name')"
          readonly
        ></v-text-field>
        <v-textarea
          name="Short description"
          label="Short description"
          v-model="editedItem.shortDescription"
          v-validate="{ max:500 }"
          :error-messages="errors.collect('Short description')"
          disabled
        ></v-textarea>
        <v-layout row wrap>
          <v-flex xs4>
            <label>ProjectOwner:</label>
          </v-flex>
          <v-flex xs8>
            <span>
              {{editedItem.projectOwner?
              editedItem.projectOwner.fullName:""}}
            </span>
          </v-flex>
          <v-flex xs4>
            <label>Total of members:</label>
          </v-flex>
          <v-flex xs8>
            <span>{{totalMembers}}</span>
          </v-flex>
          <v-flex xs4>
            <label>Total of tasks:</label>
          </v-flex>
          <v-flex xs8>
            <span>{{totalTasks.all}}</span>
          </v-flex>
          <v-flex xs4>
            <label>Total of tasks done:</label>
          </v-flex>
          <v-flex xs8>
            <span>{{totalTasks.completed}}</span>
          </v-flex>
        </v-layout>

        <div class="text-xs-right">
          <v-btn color="blue darken-1" dark @click="projectDialog=true;">Update</v-btn>
        </div>
      </v-flex>
    </v-layout>
    <v-dialog lazy v-model="projectDialog" max-width="600px" persistent>
      <ProjectCreate v-if="projectDialog" v-model="editedItem" lazy @close="closeProjectDialog" @cancel="cancelProjectDialog" />
    </v-dialog>
  </div>
</template>
<script>
import _ from "lodash";
import ProjectCreate from "../project_list/ProjectCreate";
export default {
  props: ["value", "loadData"],
  components: { ProjectCreate },
  data: () => ({
    editedItem: { projectName: "", shortDescription: "", id: null },
    projectDialog: false,
    reset: false
  }),
  updated() {
    this.editedItem = this.value;
  },
  mounted() {
    this.editedItem = this.value;
    if (this.value) {
      let me = this;
      _.delay(() => (me.reset = true), 500);
    }
  },
  computed: {
    totalMembers() {
      if (Array.isArray(this.editedItem.projectMembers)) {
        return this.editedItem.projectMembers.length;
      }
      return 0;
    },
    totalTasks() {
      var total = { all: 0, incompleted: 0, completed: 0 };

      if (Array.isArray(this.editedItem.taskGroups)) {
        this.editedItem.taskGroups.forEach(taskGroup => {
          if (Array.isArray(taskGroup.eventTasks)) {
            total.all += parseInt(taskGroup.eventTasks.length);

            let completedTasks = taskGroup.eventTasks.filter(
              eventTask => eventTask.isCompleted == true
            );

            total.completed += parseInt(completedTasks.length);
          }
        });
      }
      total.incompleted = total.all - total.completed;
      return total;
    }
  },
  watch: {},
  methods: {
    closeProjectDialog() {
      this.projectDialog = false;
      this.loadData();
    },
    cancelProjectDialog() {
      this.projectDialog = false;
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
            this.loadData();
          } else {
            this.create();
          }
        }
      });
    }
  }
};
</script>


