import { Roles } from "../enum/enums";

export default {
  data: () => ({
    roles: Roles,
    userId: null
  }),
  mounted() {
    this.userId = this.$store.state.userId;
  },
  methods: {
    checkAdmin(projectRole) {
      return Roles.admin.value == projectRole ||
        Roles.projectOwner.value == projectRole
        ? true
        : false;
    },
    checkProjectOwner(projectRole) {
      return Roles.projectOwner.value == projectRole ? true : false;
    },
    isProjectOwner(projectMembers) {
      if (Array.isArray(projectMembers)) {
        let projectMember = projectMembers.find(
          item => item.memberId == this.userId
        );
        if (projectMember) {
          return Roles.projectOwner.value == projectMember.projectRole
            ? true
            : false;
        }
      }
      return false;
    },
    isAdmin(projectMembers) {
      if (Array.isArray(projectMembers)) {
        let userId = this.$store.state.userId;
        let projectMember = projectMembers.find(
          item => item.memberId == userId
        );
        if (projectMember) {
          return Roles.admin.value == projectMember.projectRole ||
            Roles.projectOwner.value == projectMember.projectRole
            ? true
            : false;
        }
      }
      return false;
    },

    checkCurrentProjectOwner(id) {
      return id == this.$store.state.userId ? true : false;
    }
  }
};
