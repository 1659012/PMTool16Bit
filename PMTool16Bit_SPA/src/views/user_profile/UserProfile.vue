
<template>
  <v-container fluid grid-list-lg>
    <h3>Profile</h3>
    <v-layout row wrap>
      <v-flex lg4>
        <div>
          <v-img v-if="editedItem.avatarUrl" :src="editedItem.avatarUrl" alt="profile_avatar" height="300" lazy-src></v-img>

          <v-expansion-panel>
            <v-expansion-panel-content class="deep-purple darken-1">
              <template v-slot:actions>
                <v-icon color="white">$vuetify.icons.expand</v-icon>
              </template>
              <template v-slot:header>
                <div>
                  <h5 class="subheading white--text">Change avatar</h5>
                </div>
              </template>
              <v-card>
                <v-card-text>
                  <UploadAvatar :returnAvatarId.sync="editedItem.avatarId" :returnAvatarUrl.sync="editedItem.avatarUrl" />
                </v-card-text>
              </v-card>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </div>
        <v-spacer></v-spacer>

        <div>
          <v-btn color="primary" block @click="update">Update info</v-btn>
          <v-btn color="primary" block @click="logout(true)">Logout</v-btn>
        </div>
      </v-flex>
      <v-flex lg8>
        <v-expansion-panel v-model="panel" expand>
          <v-expansion-panel-content class="deep-purple darken-1">
            <template v-slot:actions>
              <v-icon color="white">$vuetify.icons.expand</v-icon>
            </template>
            <template v-slot:header>
              <div>
                <h5 class="subheading white--text">Personal info</h5>
              </div>
            </template>
            <v-card>
              <v-card-text>
                <v-form>
                  <v-text-field
                    append-icon="person"
                    type="text"
                    v-model="editedItem.userName"
                    v-validate="'required|min:4|max:50'"
                    label="Username"
                    :error-messages="errors.collect('Username')"
                    data-vv-name="Username"
                    required
                    hint="Username must at least 4 characters"
                    disabled
                  ></v-text-field>

                  <v-text-field
                    v-model="editedItem.emailAddress"
                    type="email"
                    v-validate="'required|email'"
                    :error-messages="errors.collect('E-mail')"
                    label="E-mail"
                    data-vv-name="E-mail"
                    required
                    hint="Email is required"
                  ></v-text-field>

                  <v-text-field
                    v-model="editedItem.name"
                    type="text"
                    v-validate="''"
                    :error-messages="errors.collect('name')"
                    label="Name"
                    data-vv-name="name"
                  ></v-text-field>

                  <v-text-field
                    v-model="editedItem.surname"
                    type="text"
                    v-validate="''"
                    :error-messages="errors.collect('surname')"
                    label="Surname"
                    data-vv-name="surname"
                  ></v-text-field>

                  <v-text-field label="Full name" v-model="getFullName" disabled></v-text-field>

                  <v-switch v-model="editedItem.isPublishProfile" label="Allow others view profile"></v-switch>

                  <v-btn color="primary" @click="update">Update info</v-btn>
                </v-form>
              </v-card-text>
            </v-card>
          </v-expansion-panel-content>

          <v-expansion-panel-content class="deep-purple darken-1" :disabled="$route.params.id">
            <template v-slot:actions>
              <v-icon color="white">$vuetify.icons.expand</v-icon>
            </template>
            <template v-slot:header>
              <div>
                <h5 class="subheading white--text">Change password</h5>
              </div>
            </template>
            <v-card>
              <v-card-text>
                <v-form>
                  <v-text-field
                    :append-icon="showCurrentPassword ? 'visibility' : 'visibility_off'"
                    :type="showCurrentPassword ? 'text' : 'password'"
                    @click:append="showCurrentPassword = !showCurrentPassword"
                    label="Current password"
                    id="currentPassword"
                    v-model="passwordModel.currentPassword"
                    v-validate="'required|min:4'"
                    :error-messages="errors.collect('currentPassword')"
                    data-vv-name="currentPassword"
                    required
                    hint="Password must at least 4 characters"
                  ></v-text-field>

                  <v-text-field
                    :append-icon="showNewPassword ? 'visibility' : 'visibility_off'"
                    :type="showNewPassword ? 'text' : 'password'"
                    @click:append="showNewPassword = !showNewPassword"
                    label="New password"
                    id="newPassword"
                    v-model="passwordModel.newPassword"
                    v-validate="'required|min:4'"
                    :error-messages="errors.collect('showNewPassword')"
                    data-vv-name="showNewPassword"
                    required
                    hint="Password must at least 4 characters"
                  ></v-text-field>
                  <v-btn color="primary" @click="changePassword">Update password</v-btn>
                </v-form>
              </v-card-text>
            </v-card>
          </v-expansion-panel-content>

          <v-expansion-panel-content class="deep-purple darken-1" :disabled="$route.params.id">
            <template v-slot:actions>
              <v-icon color="white">$vuetify.icons.expand</v-icon>
            </template>
            <template v-slot:header>
              <div>
                <h5 class="subheading white--text">Theme settings</h5>
              </div>
            </template>
            <v-card>
              <v-card-text>
                comming soon
                <!-- theme setting -->
                <v-btn small fab dark class="setting-fab" color="green" @click="openThemeSettings">
                  <v-icon>settings</v-icon>
                </v-btn>
              </v-card-text>
            </v-card>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-flex>
    </v-layout>
    <v-navigation-drawer class="settingDrawer" temporary right v-model="rightDrawer" hide-overlay fixed>
      <theme-settings></theme-settings>
    </v-navigation-drawer>
    <!-- <code>{{editedItem}}</code> -->
    <!-- <code>{{passwordModel.userId}}</code> -->
  </v-container>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
import UploadAvatar from "./UploadAvatar";
import ThemeSettings from "../../components/layout_tool/ThemeSettings";
export default {
  title: "User profile",
  components: { UploadAvatar, ThemeSettings },
  props: [],
  data: () => ({
    editedItem: { name: "", surname: "" },
    passwordModel: { userId: null, currentPassword: "", newPassword: "" },
    loading: false,
    panel: [true, false, false],
    showCurrentPassword: false,
    showNewPassword: false,
    rightDrawer: false
  }),

  computed: {},

  watch: {},
  mounted() {
    this.initialize();
    // this.getUser();
  },
  computed: {
    getFullName() {
      return this.editedItem.name + " " + this.editedItem.surname;
    }
  },
  methods: {
    initialize() {
      this.passwordModel.userId =
        !this.$route.params.id ||
        this.$route.params.id == this.$store.state.userId
          ? this.$store.state.userId
          : null;

      this.loadData();
    },

    loadData() {
      this.loading = true;
      let userId = this.$route.params.id
        ? this.$route.params.id
        : this.$store.state.userId;
      let me = this;
      this.axios
        .get("User/GetUserProfile", {
          params: {
            id: userId
          }
        })
        .then(response => {
          if (response.data.success) {
            me.editedItem = response.data.result;
            me.loading = false;
            // console.log(me.items);
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    getUser() {
      this.axios.get("User/GetCurrentUserName").then(response => {
        if (response.data.success) {
          console.log(response.data.result);
        }
      });
    },

    changePassword() {
      let me = this;
      this.axios
        .post("User/ChangePassword", me.passwordModel)
        .then(response => {
          if (response.data.success) {
            me.$notify({
              group: "message",
              duration: 5000,
              type: "success",
              title: "Change password success! You must login to continue",
              text: ""
            });
            this.logout(true);
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    logout(checkLogout = false) {
      this.$root.logout(checkLogout);
    },
    openThemeSettings() {
      this.$vuetify.goTo(0);
      this.rightDrawer = !this.rightDrawer;
    },
    create() {
      this.$root.createItem(this.editedItem, "User/Create", this, false);
    },
    update() {
      this.$root.updateItem(this.editedItem, "User/Update", this, false);
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