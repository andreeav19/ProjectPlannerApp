import { useState } from "react";
import { Group, Code, NavLink, Space } from "@mantine/core";
import { FaProjectDiagram } from "react-icons/fa";
import { MdLeaderboard } from "react-icons/md";
import { CgProfile } from "react-icons/cg";
import { FiLogOut } from "react-icons/fi";
import classes from "./Navbar.module.css";
import { useNavigate } from "react-router-dom";
import { AuthProvider, AuthContext } from "../AuthContext";
import { useContext } from "react";
import { FaTools } from "react-icons/fa";

const data = [
  { link: "/profile", label: "Profile", icon: CgProfile },
  { link: "/projects", label: "Projects", icon: FaProjectDiagram },
  { link: "/leaderboard", label: "Leaderboard", icon: MdLeaderboard },
];

export function Navbar() {
  const [active, setActive] = useState("Projects");

  const navigate = useNavigate();
  const links = data.map((item) => (
    <NavLink
      key={item.label}
      active={item.label === active}
      label={item.label}
      // description={item.label}
      // rightSection={item.rightSection}
      leftSection={item.icon && <item.icon size="1rem" />}
      // icon={<item.icon size="1rem" />}
      onClick={() => {
        setActive(item.label);
        navigate(item.link);
      }}
      color="theme"
    />
  ));

  const { authenticated, setAuthicated } = useContext(AuthContext);

  const handleLogout = (event) => {
    console.log("Before", authenticated);
    event.preventDefault();
    setAuthicated(false);
    navigate("/login");
    console.log("After", authenticated);
  };

  return (
    <div className={classes.navbarMain}>
      <Group className={classes.header} justify="space-between"></Group>

      {links}

      <div className={classes.footer}>
        <a href="#" className={classes.link} onClick={handleLogout}>
          <FiLogOut className={classes.linkIcon} />
          <span>Logout</span>
        </a>
        <Space h="xl" />
        <NavLink
          label="Admin Tools"
          description="Danger Zone"
          leftSection={<FaTools size="1rem" />}
          onClick={() => {
            navigate("/admin");
          }}
          color="theme"
        />
      </div>
    </div>
  );
}
