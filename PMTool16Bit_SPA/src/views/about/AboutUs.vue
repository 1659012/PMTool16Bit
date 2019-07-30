
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
        avatarUrl: "https://vignette.wikia.nocookie.net/leagueoflegends/images/e/eb/Darius_Poro_Icon.png/revision/latest/scale-to-width-down/480?cb=20150216223701"
      },
       {
        fullName: "Thịnh",
        id: 1659029,
        role: "Dev",
        avatarUrl: "https://vignette.wikia.nocookie.net/leagueoflegends/images/1/10/ProfileIcon0748_Icon_of_the_Poro_King.png/revision/latest?cb=20180202151337&path-prefix=it"
      },
       {
        fullName: "Sơn",
        id: 1659027,
        role: "Tester",
        avatarUrl: "https://media.lolusercontent.com/api/embedly/1/image/resize?url=http%3A%2F%2Fvignette2.wikia.nocookie.net%2Fleagueoflegends%2Fimages%2Fc%2Fc8%2FSona_Poro_Icon.png%2Frevision%2Flatest%3Fcb%3D20150214174347&key=f0abbd34f14549f3a15cd94dd9970851&width=425"
      },
       {
        fullName: "Trí",
        id: 1659031,
        role: "Dev",
        avatarUrl: "https://media.lolusercontent.com/api/embedly/1/image/resize?url=http%3A%2F%2Fvignette2.wikia.nocookie.net%2Fleagueoflegends%2Fimages%2F4%2F46%2FYasuo_Poro_Icon.png%2Frevision%2Flatest%3Fcb%3D20150214174528&key=f0abbd34f14549f3a15cd94dd9970851&width=425"
      },
       {
        fullName: "Ý",
        id: 1659034,
        role: "Dev",
        avatarUrl: "https://media.lolusercontent.com/api/embedly/1/image/resize?url=http%3A%2F%2Fvignette3.wikia.nocookie.net%2Fleagueoflegends%2Fimages%2F9%2F9b%2FShaco_Poro_Icon.png%2Frevision%2Flatest%3Fcb%3D20150220161436&key=f0abbd34f14549f3a15cd94dd9970851&width=425"
      }
    ]
  }),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>
