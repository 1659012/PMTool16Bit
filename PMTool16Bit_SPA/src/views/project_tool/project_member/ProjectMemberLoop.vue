
<template>
  <div v-if="value">
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

                <v-list-tile-content>
                  <v-list-tile-title v-html="projectMember.member.fullName" @click="getProfile(projectMember.member.id)" style="cursor: pointer;"></v-list-tile-title>
                  <v-list-tile-sub-title v-html="projectMember.member.emailAddress"></v-list-tile-sub-title>
                </v-list-tile-content>

                <v-list-tile-action style="width:300px">
                  <v-list-tile-sub-title>{{projectMember.projectRole?projectMember.projectRole:"Unassign role"}}</v-list-tile-sub-title>

                  <v-list-tile-sub-title v-if="projectMember.member.lastLoginTime">Last active: {{momentFromNow(projectMember.member.lastLoginTime)}}</v-list-tile-sub-title>
                </v-list-tile-action>

                <v-list-tile-action style="width:300px">
                  <v-btn
                    color="success"
                    v-if="projectMember.memberId==$store.state.userId &&$store.state.userId!=projectOwnerId"
                    @click="leaveProject(projectMember)"
                  >Leave</v-btn>
                </v-list-tile-action>

                <v-list-tile-action>
                  <v-menu
                    bottom
                    left
                    lazy
                    transition="slide-x-transition"
                    v-if="!checkProjectOwner(projectMember.projectRole)"
                    :disabled="!isAdmin(projectMembers)"
                  >
                    <template #activator="{ on: menu }">
                      <v-tooltip top>
                        <template #activator="{ on: tooltip }">
                          <v-btn icon v-on="{ ...menu,...tooltip }" class="mr-0">
                            <v-icon color="blue-grey darken-1">more_vert</v-icon>
                          </v-btn>
                        </template>
                        <span>Member settings</span>
                      </v-tooltip>
                    </template>

                    <v-list class="pa-0">
                      <v-list-tile class="text--primary pa-0" @click="openMemberRoleDialog(projectMember)">
                        <v-list-tile-title class="py-0 my-0">Authorize member</v-list-tile-title>
                      </v-list-tile>
                      <v-divider></v-divider>
                      <v-list-tile class="text--primary pa-0" @click="deleteItem(projectMember)">
                        <v-list-tile-title class="py-0 my-0">Remove member</v-list-tile-title>
                      </v-list-tile>
                    </v-list>
                  </v-menu>

                  <v-icon v-else class="ml-3" color="green accent-4" title="Project owner">stars</v-icon>
                </v-list-tile-action>
              </v-list-tile>
              <v-divider :key="`'divider'`+index"></v-divider>
            </template>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
    <v-dialog lazy v-model="memberRoleDialog" max-width="600px" persistent>
      <ProjectMemberRoleUpdate
        v-if="memberRoleDialog"
        lazy
        v-model="memberItem"
        :projectOwnerId="projectOwnerId"
        @close="memberRoleDialog=false; memberItem={};loadData()"
        @cancel="memberRoleDialog=false; memberItem={};"
      />
    </v-dialog>
    <!-- <code>{{projectMembers}}</code> -->
    <!-- <code>{{projectOwnerId}}</code> -->
  </div>
</template>
<script>
import ProjectMemberRoleUpdate from "./ProjectMemberRoleUpdate";
import projectMixin from "../../../mixin/projectMixin.js";
export default {
  // title: "Project member",
  mixins: [projectMixin],
  components: { ProjectMemberRoleUpdate },
  props: ["value", "loadData"],
  data: () => ({
    editedItem: {},
    projectMembers: {},
    memberItem: {},
    projectOwnerId: null,
    memberRoleDialog: false
  }),
  computed: {},
  watch: {},
  mounted() {
    this.editedItem = this.value;
    this.projectMembers = this.editedItem.projectMembers;
    this.projectOwnerId = this.editedItem.projectOwnerId;
  },
  updated() {
    this.editedItem = this.value;
    this.projectMembers = this.editedItem.projectMembers;
    this.projectOwnerId = this.editedItem.projectOwnerId;
  },
  methods: {
    getProfile(userId) {
      let routeData = this.$router.resolve(`/Profile/${userId}`);
      window.open(routeData.href, "_blank");
    },
    openMemberRoleDialog(projectMember) {
      this.memberRoleDialog = true;
      this.memberItem = Object.assign({}, projectMember);
    },
    close(item) {
      this.$emit("close", item);
    },

    updateItem(item) {
      this.$root.updateItem(item, "ProjectMemberService/Update", this);
    },
    leaveProject(item) {
      this.deleteItem(item);
      this.$router.push("/projectlist");
    },
    deleteItem(item) {
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