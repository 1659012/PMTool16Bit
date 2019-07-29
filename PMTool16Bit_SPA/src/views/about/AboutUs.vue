
<template>
  <v-container fluid>
    <h3>About us</h3>
    <v-layout row wrap>
      <v-flex lg3 v-for="(member, index) in members" :key="index">
        <v-img contain :aspect-ratio="16/9" :src="member.avatarUrl">
          <v-layout pa-2 column fill-height class="lightbox white--text">
            <v-spacer></v-spacer>
            <v-flex shrink>
              <div class="subheading">{{member.fullName}}</div>
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
        avatarUrl: "https://cdn.vuetifyjs.com/images/parallax/material.jpg"
      }
    ]
  }),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>
