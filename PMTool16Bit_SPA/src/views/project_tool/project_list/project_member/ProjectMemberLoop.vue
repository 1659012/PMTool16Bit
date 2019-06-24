
<template>
  <div>
    <v-layout row wrap>
      <v-flex lg12>
        <v-card>
          <!-- <v-toolbar color="cyan" dark>
            <v-toolbar-side-icon></v-toolbar-side-icon>
  
            <v-toolbar-title>Inbox</v-toolbar-title>
  
            <v-spacer></v-spacer>
  
            <v-btn icon>
              <v-icon>search</v-icon>
            </v-btn>
          </v-toolbar>-->

          <v-list two-line class="py-0">
            <template v-for="(projectMember, index) in projectMembers">
              <v-list-tile :key="index" avatar>
                <v-list-tile-avatar>
                  <img v-if="projectMember.member.avatarUrl" :src="projectMember.member.avatarUrl">

                 <v-avatar v-else class="grey lighten-2 teal--text pa-3">
                      {{ projectMember.member.name.slice(0, 1).toUpperCase()+
                      projectMember.member.surname.slice(0, 1).toUpperCase()}}
                    </v-avatar>

                  <!-- <v-chip>
                    <v-avatar class="teal">A</v-avatar>ANZ Bank
                  </v-chip> -->
                </v-list-tile-avatar>

                <v-list-tile-content>
                  <v-list-tile-title v-html="projectMember.member.fullName" @click="getProfile(projectMember.member.id)" style="cursor: pointer;"></v-list-tile-title>
                  <v-list-tile-sub-title v-html="projectMember.member.emailAddress"></v-list-tile-sub-title>
                </v-list-tile-content>
              </v-list-tile>
                <v-divider :key="`'divider'`+index"></v-divider>
            </template>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
    <!-- <code>{{projectMembers}}</code> -->
  </div>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import ProjectMemberDialog from "./ProjectMemberDialog";
export default {
  // title: "Project member",
  components: {},
  props: ["projectMembers"],
  data: () => ({
    dialog: false
  }),

  computed: {},

  watch: {},

  updated() {},

  mounted() {},
  methods: {
    getProfile(userId){
      console.log(userId);
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