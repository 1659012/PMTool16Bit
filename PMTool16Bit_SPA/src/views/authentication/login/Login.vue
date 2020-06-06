<template>
  <v-app id="login" class="primary">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4 lg4>
            <v-card class="elevation-1 pa-3">
              <v-card-text>
                <div class="layout column align-center">
                  <img src="/static/m.png" alt="Vue Material Admin" width="120" height="120" />
                  <h1 class="flex my-4 primary--text">PMTool16Bit</h1>
                </div>
                <v-form>
                  <v-text-field
                    append-icon="person"
                    name="login"
                    label="Login"
                    type="text"
                    v-model="model.userNameOrEmailAddress"
                  ></v-text-field>
                  <v-text-field
                    append-icon="lock"
                    name="password"
                    label="Password"
                    id="password"
                    type="password"
                    v-model="model.password"
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <!-- <v-btn icon>
                  <v-icon color="blue">fa fa-facebook-square fa-lg</v-icon>
                </v-btn>
                <v-btn icon>
                  <v-icon color="red" @click="loginGoogle">fa fa-google fa-lg</v-icon>
                </v-btn>
                <v-btn icon>
                  <v-icon color="light-blue">fa fa-twitter fa-lg</v-icon>
                </v-btn> -->
                <v-card-text>
                  <span>Don’t have an account?</span>
                  <span
                    class="primary--text ml-3"
                    style="cursor: pointer;"
                    @click="$router.push('/register');"
                  >Create Account</span>
                </v-card-text>
                <v-spacer></v-spacer>
                <v-btn block color="primary" @click="login" :loading="loading">Login</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
import { APP_MUTATIONS, APP_ACTIONS } from "../../../store/store";
export default {
  data: () => ({
    loading: false,
    model: {
      userNameOrEmailAddress: "",
      password: ""
    },
    baseUrl: ""
  }),
  mounted() {
    // console.log(process.env.NODE_ENV);
    this.baseUrl =
      process.env.NODE_ENV !== "production"
        // ? "http://localhost:21021/api/services/app/"
        ? "http://hoanpmtoolapi.azurewebsites.net/api/"
        : "http://hoanpmtoolapi.azurewebsites.net/api/";
  },
  methods: {
    login() {
      // this.loading = true;
      let me = this;
      this.axios
        .post(me.baseUrl + "TokenAuth/Authenticate", this.model)
        .then(response => {
          // me.loading = false;
          if (response.data.success) {
            // console.log(response.data.result);
            //  me.$notify({
            //   group: "message",
            //   duration: 3000,
            //   type: "success",
            //   title: "Login success",
            //   text: "Hi "+ response.data.result.profile.username
            // });
            me.$store.commit(
              APP_MUTATIONS.SET_TOKEN,
              response.data.result.accessToken
            );
            me.$localStorage.set(
              "profile",
              JSON.stringify(response.data.result.profile)
            );
            me.$localStorage.set("token", response.data.result.accessToken);
            me.$localStorage.set("roles", response.data.result.roles);
            window.location.reload();
            setTimeout(() => {
              me.$store.commit(APP_MUTATIONS.LOGIN_SUCCESS);

              me.$store.dispatch(APP_ACTIONS.LOGIN, {
                token: response.data.result.accessToken,
                profile: response.data.result.profile,
                // roles: response.data.result.roles
                userId: response.data.result.userId,
                userName: response.data.result.userName,
                roles: null
              });
            }, 3000);
            // console.log(response.data.result.accessToken);
          } else {
            // me.loading = false;
            me.$notify({
              group: "message",
              duration: 3000,
              type: "error",
              title: response.data.error.message,
              text: response.data.error.details
            });
          }
        });
    },
    loginGoogle() {
      // this.$gAuth
      //   .getAuthCode()
      //   .then(authCode => {
      //     //on success
      //     console.log(authCode);
      //     // return this.$http.post("http://your-backend-server.com/auth/google", {
      //     //   code: authCode,
      //     //   redirect_uri: "postmessage"
      //     // });
      //   })
      //   .then(response => {
      //     //after ajax
      //   })
      //   .catch(error => {
      //     //on fail do something
      //   });
    }
  }
};
</script>
<style scoped lang="css">
#login {
  height: 50%;
  width: 100%;
  position: absolute;
  top: 0;
  left: 0;
  content: "";
  z-index: 0;
}
</style>
