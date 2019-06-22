
<template>
  <v-container fluid grid-list-lg>
    <h3 class="headline">Profile</h3>
    <v-layout row wrap>
      <v-flex lg4>
        <h5 class="body-2">Avatar here</h5>
        <v-spacer></v-spacer>
        <v-btn color="primary" block @click="updateUser" >Update info</v-btn>
      </v-flex>
      <v-flex lg8>
        <div class="text-xs-center mb-3">{{ panel }}</div>
        <v-expansion-panel v-model="panel" expand>
          <v-expansion-panel-content class="deep-purple darken-1">
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
                    disabled
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

                  <v-switch
                    v-model="editedItem.isPublishProfile"                  
                    label="Allow others view profile"
                  ></v-switch>
                </v-form>
              </v-card-text>
            </v-card>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-flex>
    </v-layout>
    <!-- <code>{{editedItem}}</code> -->
  </v-container>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
export default {
  title: "User profile",
  components: {},
  props: [],
  data: () => ({
    editedItem: {name:"",surname:""},
    loading: false,
    panel: [true, true, true],
    source:{}
  }),

  computed: {},

  watch: {},
  mounted() {
    this.initialize();
  },
  computed: {
    getFullName() {
      return this.editedItem.name + " " + this.editedItem.surname;
    }
  },
  methods: {
    initialize() {
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
      updateUser() {
      // this.loading = true;
      this.source= this.editedItem;
      let me = this;
      this.axios
        .put("User/Update", this.source)
        .then(response => {
          // me.loading = false;
          if (response.data.success) {
            me.$notify({
              group: "message",
              duration: 3000,
              type: "success",
              title: "update",
              text: response.data.result
            });
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

    create() {
      this.$root.createItem(this.editedItem, "User/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "User/Update", this);
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