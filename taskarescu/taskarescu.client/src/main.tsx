import { BrowserRouter } from "react-router-dom";
import { MantineProvider } from "@mantine/core";
import { theme } from "./theme";
import { AuthProvider, useAuth } from "./components/AuthContext";
import { LoginPage } from "./pages/Login.page";
import { Shell } from "./pages/Shell.page";
import React from "react";
import ReactDOM from "react-dom/client";
import "@mantine/core/styles.css";
import "mantine-datatable/styles.layer.css";
import "./layout.css";
function App() {
  const isAuthenticated = true;

  console.log(isAuthenticated);
  return (
    <React.StrictMode>
      <AuthProvider>
        <MantineProvider theme={theme} defaultColorScheme="dark">
          <BrowserRouter>
            {isAuthenticated ? <Shell /> : <LoginPage />}
          </BrowserRouter>
        </MantineProvider>
      </AuthProvider>
    </React.StrictMode>
  );
}

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

root.render(<App />);
