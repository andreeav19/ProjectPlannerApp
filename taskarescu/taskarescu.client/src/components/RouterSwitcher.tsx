import { Route, Routes } from "react-router-dom";
import { Leaderboard } from "./Leaderboard";
import { Profile } from "./Profile";
import { NotFound } from "../pages/NotFound";
import { Projects } from "./Projects/Projects";

export function RouterSwitcher() {
  return (
    <Routes>
      <Route path="/profile" element={<Profile />} />
      <Route path="/projects" element={<Projects />} />
      <Route path="/leaderboard" element={<Leaderboard />} />
      <Route path="/error" element={<NotFound />} />
    </Routes>
  );
}
