const Menu = [
  { header: "Project Management Tool" },
  {
    title: "Dashboard",
    group: "projectTool",
    icon: "dashboard",
    name: "Dashboard"
  },
  {
    title: "Project list",
    group: "projectTool",
    icon: "widgets",
    name: "ProjectList"
  },
  // {
  //   title: "Project list",
  //   group: "projectTool",
  //   component: "testPageList",
  //   icon: "widgets",
  //   items: [
  //     { name: "ProjectList", title: "Project list", component: "ProjectList" }
  //   ]
  // },
  { header: "User info" },
  {
    name: "profile",
    title: "Profile",
    group: "userInfo",
    icon: "donut_small",
    component: "UserProfile"
  },
  // { divider: true },
  // { header: "UI Elements" },
  // {
  //   title: "Pickers",
  //   group: "pickers",
  //   component: "picker",
  //   icon: "filter_vintage",
  //   items: [
  //     {
  //       name: "timepicker",
  //       title: "Timepicker",
  //       component: "pickers/timepicker"
  //     },
  //     {
  //       name: "datepicker",
  //       title: "Datepicker",
  //       component: "pickers/datepicker"
  //     }
  //   ]
  // },
  // {
  //   title: "Layout",
  //   group: "layout",
  //   component: "layout",
  //   icon: "view_compact",
  //   items: [
  //     {
  //       name: "bottom-sheets",
  //       title: "Bottom panels",
  //       component: "components/bottom-sheets"
  //     },
  //     {
  //       name: "expansion-panels",
  //       title: "Expansion panels",
  //       component: "components/expansion-panels"
  //     },
  //     { name: "footer", title: "Footer", component: "components/footer" },
  //     { name: "lists", title: "Lists", component: "components/lists" },
  //     {
  //       name: "jumbotrons",
  //       title: "Jumbotrons",
  //       badge: "new",
  //       component: "components/jumbotrons"
  //     },
  //     { name: "menus", title: "Menus", component: "components/menus" },
  //     { name: "tabs", title: "Tabs", component: "components/tabs" },
  //     { name: "toolbar", title: "Toolbars", component: "components/toolbar" },
  //     { name: "timeline", title: "Timeline", component: "components/timeline" }
  //   ]
  // },
  // {
  //   title: "Forms & Controls",
  //   group: "forms",
  //   component: "forms",
  //   icon: "edit",
  //   items: [
  //     { name: "basic", title: "General", component: "components/basic-forms" },
  //     {
  //       name: "selects",
  //       title: "Selects",
  //       badge: "new",
  //       component: "components/selects"
  //     },
  //     {
  //       name: "selection-controls",
  //       title: "Selection Controls",
  //       component: "components/selection-controls"
  //     },
  //     {
  //       name: "text-fields",
  //       title: "Text Fields",
  //       component: "components/text-fields"
  //     },
  //     { name: "steppers", title: "Steppers", component: "components/steppers" },
  //     { name: "editors", title: "Editors", component: "components/editors" }
  //   ]
  // },

  { divider: true },

  { header: "PMTool16Bit info" },
  {
    name: "AboutUs",
    title: "About us",
    group: "projectInfo",
    icon: "donut_small",
    component: "AboutUs"
  },
  // {
  //   title: "About us",
  //   group: "aboutus",
  //   icon: "list",
  //   items: [
  //     { name: "Login", title: "Login", component: "Login" },
  //     { name: "404", title: "404", component: "NotFound" },
  //     { name: "403", title: "403", component: "AccessDenied" },
  //     { name: "500", title: "500", component: "ServerError" }
  //   ]
  // },
  // {
  //   title: "Contact us",
  //   group: "contact",
  //   icon: "list",
  //   items: [
  //     { name: "Login", title: "Login", component: "Login" },
  //     { name: "404", title: "404", component: "NotFound" },
  //     { name: "403", title: "403", component: "AccessDenied" },
  //     { name: "500", title: "500", component: "ServerError" }
  //   ]
  // }
];

// reorder menu
Menu.forEach(item => {
  if (item.items) {
    item.items.sort((x, y) => {
      let textA = x.title.toUpperCase();
      let textB = y.title.toUpperCase();
      return textA < textB ? -1 : textA > textB ? 1 : 0;
    });
  }
});

export default Menu;
