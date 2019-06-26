
<template>
  <div>
    <v-layout row wrap>
      <v-flex lg12>
        <v-card>
          <v-list two-line class="py-0">
            <template v-for="(projectMember, index) in projectMembers">
              <v-list-tile :key="index" avatar>
                <v-list-tile-avatar>
                  <!-- <img v-if="projectMember.member.avatarUrl" :src="projectMember.member.avatarUrl"> -->

                  <v-avatar class="grey lighten-2 teal--text pa-3">
                    {{ projectMember.member.name.slice(0, 1).toUpperCase()+
                    projectMember.member.surname.slice(0, 1).toUpperCase()}}
                  </v-avatar>
                </v-list-tile-avatar>

                <v-list-tile-content style="width:600px!important;">
                  <v-list-tile-title
                    v-html="projectMember.member.fullName"
                    @click="getProfile(projectMember.member.id)"
                    style="cursor: pointer;"
                  ></v-list-tile-title>
                  <v-list-tile-sub-title v-html="projectMember.member.emailAddress"></v-list-tile-sub-title>
                </v-list-tile-content>

                <v-list-tile-action>                 
                  <v-layout row wrap pa-0 ma-0  style="width:600px!important;">
                    <v-flex lg4></v-flex>
                    <v-flex lg4></v-flex>
                    <v-flex lg4 pa-0 ma-0>
                      <v-select
                        :items="roles"
                        v-model="projectMember.projectRole"
                        :label="projectMember.projectRole?'':'Unassign role'"
                        flat
                       
                      ></v-select>
                    </v-flex>
                  </v-layout>
                </v-list-tile-action>
              </v-list-tile>
              <v-divider :key="`'divider'`+index"></v-divider>
            </template>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
    <v-dialog lazy v-model="taskGroupDialog" max-width="600px" persistent>
      <TaskGroupCreate
        v-if="taskGroupDialog"
        lazy
        :projectId="editedItem.id"
        @close="taskGroupDialog=false;loadData()"
      />
      <v-card>
        <v-card-actions>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="red darken-1" flat @click.native="close()">Cancel</v-btn>
            <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
          </v-card-actions>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <code>{{projectMembers}}</code>
  </div>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import ProjectMemberDialog from "./ProjectMemberDialog";
import Enums from "../../../../enum/enums";
export default {
  // title: "Project member",
  components: {},
  props: ["projectMembers", "loadData"],
  data: () => ({
    roles: Enums.Roles,
    memberItem: {}
  }),

  computed: {},

  watch: {},

  updated() {},

  mounted() {
    console.log(this.roles);
  },
  methods: {
    getProfile(userId) {
      let routeData = this.$router.resolve(`/Profile/${userId}`);
      window.open(routeData.href, "_blank");
    },
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