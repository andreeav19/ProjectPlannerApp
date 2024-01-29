import { NavLink, Route, Routes } from "react-router-dom";
import { Leaderboard } from "./Leaderboard";
import { Profile } from "./Profile";
import { NotFound } from "../pages/NotFound";
import { Projects } from "./Projects/Projects";
import { LoginPage } from "../pages/Login.page";
import { useContext } from "react";
import { AuthProvider, AuthContext } from "./AuthContext";
import { AdminTools } from "./AdminTools";
import { Project } from "./Projects/Project";
export function RouterSwitcher() {
  return (
    <Routes>
      <Route path="/error" element={<NotFound />} />
      <Route path="/profile" element={<Profile />} />
      <Route path="/projects" element={<Projects />} />
      <Route path="/leaderboard" element={<Leaderboard />} />
      <Route path="/admin" element={<AdminTools />} />
      <Route path="/project/:projectId" element={<Project />} />
    </Routes>
  );
}
