import { useState } from "react";
import { Group, Code, NavLink } from "@mantine/core";
import { FaProjectDiagram } from "react-icons/fa";
import { MdLeaderboard } from "react-icons/md";
import { CgProfile } from "react-icons/cg";
import { FiLogOut } from "react-icons/fi";
import classes from "./Navbar.module.css";
const data = [
  { link: "/profile", label: "Profile", icon: CgProfile },
  { link: "/projects", label: "Projects", icon: FaProjectDiagram },
  { link: "/leaderboard", label: "Leaderboard", icon: MdLeaderboard },
];

export function Navbar() {
  const [active, setActive] = useState("Billing");

  const links = data.map((item) => (
    <NavLink
      href={item.link}
      key={item.label}
      active={item.label === active}
      label={item.label}
      // description={item.label}
      // rightSection={item.rightSection}
      leftSection={item.icon && <item.icon size="1rem" />}
      // icon={<item.icon size="1rem" />}
      onClick={() => setActive(item.label)}
      color="theme"
    />
  ));

  return (
    <div className={classes.navbarMain}>
      <Group className={classes.header} justify="space-between"></Group>

      {links}

      <div className={classes.footer}>
        <a
          href="#"
          className={classes.link}
          onClick={(event) => event.preventDefault()}
        >
          <FiLogOut className={classes.linkIcon} />
          <span>Logout</span>
        </a>
      </div>
    </div>
  );
}
