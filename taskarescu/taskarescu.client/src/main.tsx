// import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import NewAccountPaige from "./screens/NewAccountPaige.tsx";
import HomeScreen from "./screens/HomeScreen.tsx";
import ProfileScreen from "./screens/ProfileScreen.tsx";
import Leaderboard from "./screens/Leaderboard.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
    },
    {
        path: "newaccount",
        element: <NewAccountPaige/>,
    },
    {
        path: "home",
        element: <HomeScreen/>,
    },
    {
        path: "profile",
        element: <ProfileScreen/>,
    },
    {
        path: "leaderboard",
        element: <Leaderboard/>,
    },
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
    <RouterProvider router={router}/>
)
