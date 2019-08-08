
<template>
  <v-container fluid>
    <h3>About us</h3>    
    <v-layout row wrap>
      <v-flex lg12>
        <div class="text-xs-center">
          <h3>16bit - Group 4</h3>
        </div>
      </v-flex>
      <v-flex lg3 v-for="(member, index) in members" :key="index">
        <v-img :aspect-ratio="1" :src="member.avatarUrl" class="ma-3">
          <v-layout pa-2 column fill-height class="lightbox white--text">
            <v-spacer></v-spacer>
            <v-flex shrink>
              <div class="font-weight-medium">{{member.fullName}}</div>
              <div class="body-1">{{member.id}}</div>
              <div class="body-1">{{member.role}}</div>
            </v-flex>
          </v-layout>
        </v-img>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
export default {
  title: "About us",
  components: {},
  data: () => ({
    members: [
      {
        fullName: "Hoan",
        id: 1659012,
        role: "Team leader",
        avatarUrl: "/static/members/lol.jpg"
      },
       {
        fullName: "Thịnh",
        id: 1659029,
        role: "Dev",
        avatarUrl: "/static/members/lol.jpg"
      },
       {
        fullName: "Sơn",
        id: 1659027,
        role: "Tester",
        avatarUrl: "/static/members/lol.jpg"
      },
       {
        fullName: "Trí",
        id: 1659031,
        role: "Dev",
        avatarUrl: "/static/members/lol.jpg"
      },
       {
        fullName: "Ý",
        id: 1659034,
        role: "Dev",
        avatarUrl: "/static/members/lol.jpg"
      }
    ]
  }),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>
