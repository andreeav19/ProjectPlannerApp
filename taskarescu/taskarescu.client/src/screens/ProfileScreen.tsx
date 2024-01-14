import React from 'react';
import Sidebar from "../components/Sidebar.tsx";
import "./HomeScreen.css"
import Badge from "../components/Badge.tsx";
import Spacer from "../components/Spacer.tsx";

const ProfileScreen: React.FC = () => {
    return (
        <div className="home">
            <Sidebar/>
            <div className="dreapta">
                <h2 style={{alignItems: "center"}}>Profilul meu</h2>
                <Spacer size={10}></Spacer>
                <Badge imageSrc={"/1.png"} text={"Felicitari! Ti-ai facut cont nou! :)"}/>
            </div>
        </div>
    );
};

export default ProfileScreen;
