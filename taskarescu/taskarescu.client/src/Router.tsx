import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { LoginPage } from "./pages/Login.page";
import { Shell } from "./pages/Shell.page";
const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/",
    element: <Shell />,
  },
]);

export function Router() {
  return <RouterProvider router={router} />;
}
