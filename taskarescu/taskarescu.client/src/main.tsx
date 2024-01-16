import { BrowserRouter } from "react-router-dom";
import { MantineProvider } from "@mantine/core";
import { theme } from "./theme";
import { AuthProvider, AuthContext } from "./components/AuthContext";
import { LoginPage } from "./pages/Login.page";
import { Shell } from "./pages/Shell.page";
import React, { useContext, useEffect } from "react";
import ReactDOM from "react-dom/client";
import "@mantine/core/styles.css";
import "mantine-datatable/styles.layer.css";
import "./layout.css";
import { App } from "./App";
function Start() {
  return (
    <React.StrictMode>
      <AuthProvider>
        <MantineProvider theme={theme} defaultColorScheme="dark">
          <BrowserRouter>
            <App />
          </BrowserRouter>
        </MantineProvider>
      </AuthProvider>
    </React.StrictMode>
  );
}

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

root.render(<Start />);
