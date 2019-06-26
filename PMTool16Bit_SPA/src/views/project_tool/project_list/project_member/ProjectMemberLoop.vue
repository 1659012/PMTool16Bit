
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
                  <v-menu bottom left lazy transition="slide-x-transition">
                    <template #activator="{ on: menu }">
                      <v-tooltip left>
                        <template #activator="{ on: tooltip }">
                          <v-btn icon v-on="{ ...menu,...tooltip }" class="mr-0">
                            <v-icon color="blue-grey darken-1">more_vert</v-icon>
                          </v-btn>
                        </template>
                        <span>Member settings</span>
                      </v-tooltip>
                    </template>

                    <v-list>
                      <v-list-tile class="text--primary pa-0" @click="memberRoleDialog=true;">
                        <v-list-tile-title class="py-0 my-0">
                          <!-- Authorize member -->
                          <v-menu offset-y>
                            <template v-slot:activator="{ on }">
                              <v-btn color="primary" dark v-on="on">Dropdown</v-btn>
                            </template>
                            <v-list>
                              <v-list-tile
                                v-for="(role, index) in roles"
                                :key="index"
                                @click="1221"
                              >
                                <v-list-tile-title>{{ role }}</v-list-tile-title>
                              </v-list-tile>
                            </v-list>
                          </v-menu>
                        </v-list-tile-title>
                      </v-list-tile>
                      <v-divider></v-divider>
                      <v-list-tile class="text--primary pa-0" @click="deleteItem(projectMember)">
                        <v-list-tile-title class="py-0 my-0">Remove member</v-list-tile-title>
                      </v-list-tile>
                    </v-list>
                  </v-menu>
                </v-list-tile-action>
              </v-list-tile>
              <v-divider :key="`'divider'`+index"></v-divider>
            </template>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
    <!-- <v-dialog lazy v-model="taskGroupDialog" max-width="600px" persistent>
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
    </v-dialog>-->
    <!-- <code>{{projectMembers}}</code> -->
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
    memberItem: {},
    memberRoleDialog: false
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

    updateItem(item) {
      this.$root.updateItem(item, "ProjectMemberService/Update", this);
    },

    deleteItem(item) {
      console.log(item);
      let me = this;
      this.axios
        .delete("ProjectMemberService/Delete", {
          params: { projectId: item.projectId, memberId: item.memberId }
        })
        .then(response => {
          if (response.data.success) {
            me.loadData();
            me.$notify({
              group: "message",
              type: "success",
              title: "Delete",
              text: "Data has been deleted!"
            });
          }
        });
    }
  }
};
</script>