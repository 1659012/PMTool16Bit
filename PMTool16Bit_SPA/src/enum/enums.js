export const PriorityLevels = [
  {
    value: 0,
    text: "Low",
    color: "grey lighten-2",
    icon: "alarm"
  },
  {
    value: 1,
    text: "Medium",
    color: "blue darken-1",
    icon: "alarm"
  },
  {
    value: 2,
    text: "High",
    color: "yellow lighten-2",
    icon: "alarm"
  },
  {
    value: 3,
    text: "Critical",
    color: "deep-orange darken-1",
    icon: "alarm"
  }
];
export const Roles = {
  projectOwner: { label: "Project owner", value: "Project_Owner",description:"Full authorizes" },
  admin: {
    label: "Admin",
    value: "Admin",
    description: "View-Edit-Create-Delete Tasks; Add-Remove Members"
  },
  member: { label: "Member", value: "Member", description: "View-Edit Tasks" }
};

export default { PriorityLevels, Roles };
