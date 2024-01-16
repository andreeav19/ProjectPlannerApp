import { BrowserRouter } from "react-router-dom";
import { MantineProvider } from "@mantine/core";
import { theme } from "./theme";
import { AuthProvider, AuthContext } from "./components/AuthContext";
import { LoginPage } from "./pages/Login.page";
import { Shell } from "./pages/Shell.page";
import React, { useContext, useEffect } from "react";
import ReactDOM from "react-dom/client";

export function App() {
  const { authenticated } = useContext(AuthContext);

  console.log("authenticated", authenticated);

  return <>{authenticated ? <Shell /> : <LoginPage />}</>;
}
