<template>
  <v-app id="register" class="primary">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4 lg4>
            <v-card class="elevation-1 pa-3">
              <v-card-text class="py-0">
                <div class="layout column align-center">
                  <!-- <img src="/static/m.png" alt="Vue Material Admin" width="120" height="120"> -->
                  <h1 class="flex my-4 primary--text">PMTool16Bit</h1>
                </div>
                <v-form class="py-0">
                  <v-text-field
                    append-icon="person"
                    type="text"
                    v-model="model.userName"
                    v-validate="'required|min:4|max:50'"
                    :counter="50"
                    label="Username"
                    :error-messages="errors.collect('Username')"
                    data-vv-name="Username"
                    required
                    hint="Username must at least 4 characters"
                  ></v-text-field>

                  <v-text-field
                    append-icon="lock"
                    label="Password"
                    type="password"
                    id="password"
                    v-model="model.password"
                    v-validate="'required|min:4'"
                    :error-messages="errors.collect('password')"
                    data-vv-name="password"
                    required
                    hint="Password must at least 4 characters"
                  ></v-text-field>

                  <v-text-field
                    v-model="model.emailAddress"
                    type="email"
                    v-validate="'required|email'"
                    :error-messages="errors.collect('E-mail')"
                    label="E-mail"
                    data-vv-name="E-mail"
                    required
                    hint="Email is required"
                  ></v-text-field>

                  <v-text-field
                    v-model="model.name"
                    type="text"
                    v-validate="''"
                    :error-messages="errors.collect('name')"
                    label="Name"
                    data-vv-name="name"
                  ></v-text-field>

                  <v-text-field
                    v-model="model.surname"
                    type="text"
                    v-validate="''"
                    :error-messages="errors.collect('surname')"
                    label="Surname"
                    data-vv-name="surname"
                  ></v-text-field>

                  <v-text-field label="Full name" v-model="getFullName" disabled></v-text-field>
                  
                  <v-switch
                    v-model="model.isPublishProfile"
                    v-validate="''"
                    :error-messages="errors.collect('checkbox')"
                    :value="true"
                    label="Allow others view profile"
                    data-vv-name="checkbox"
                    type="switch"
                    class="py-0 my-0"
                  ></v-switch>
                </v-form>
              </v-card-text>
              <v-card-actions class="py-0">
                <v-card-text>
                  <span class="caption">Already have an account?</span>
                  <span
                    class="primary--text caption ml-3"
                    style="cursor: pointer;"
                    @click="$router.push('/login');"
                  >Login here</span>
                </v-card-text>

                <v-spacer></v-spacer>
                <v-btn color="primary" @click="register">Create New Account</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
        <!-- <code>{{model}}</code> -->
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
export default {
  data: () => ({
    loading: false,
    model: {
      username: "admin",
      password: "",
      name: "",
      surname: "",
      emailAddress: "",
      isPublishProfile: true,
      roleNames: ["admin"]
    },
    defaultModel: {
      username: "admin",
      password: "",
      name: "",
      surname: "",
      emailAddress: "",
      isPublishProfile: true,
      roleNames: ["admin"]
    }
  }),
  mounted() {},
  computed: {
    getFullName() {
      return this.model.name + " " + this.model.surname;
    }
  },
  methods: {
    refreshForm() {
      this.model = Object.assign({}, this.defaultModel);
    },
    register() {
      let me = this;
      this.axios
        .post("User/Create", this.model)
        .then(response => {
          if (response.data.success) {
            // console.log(response.data.result);
            me.$notify({
              group: "message",
              duration: 3000,
              type: "success",
              title: "Create account success!",
              text: "Please login to continue"
            });
          } else {
            me.$notify({
              group: "message",
              duration: 3000,
              type: "error",
              title: response.data.error.message,
              text: response.data.error.details
            });
          }
        })
        .then(() => {
          this.$router.push("/login");
        });
    }
  }
};
</script>
<style scoped lang="css">
#register {
  height: 50%;
  width: 100%;
  position: absolute;
  top: 0;
  left: 0;
  content: "";
  z-index: 0;
}
</style>
