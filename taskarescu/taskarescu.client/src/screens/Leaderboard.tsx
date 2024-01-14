import React from 'react';
import Sidebar from "../components/Sidebar.tsx";
import "./HomeScreen.css"
import Spacer from "../components/Spacer.tsx";
import LeaderboardRow from "../components/LeaderboardRow.tsx";
import "./Leaderboard.css"

const Leaderboard: React.FC = () => {
    return (
        <div className="home">
            <Sidebar/>
            {/*<div className={"leader"}>*/}
                <div className="dreapta">
                    <h2 style={{alignItems: "center"}}>Leaderboard</h2>
                    <div className="totLeaderboard">
                        <div className="topLeaderRow">
                            <Spacer size={10}></Spacer>
                            <text>Nume</text>
                            <div className="puncte">
                                <p className="text-lg font-semibold">Puncte</p>
                            </div>
                        </div>
                    </div>
                    <Spacer size={20}></Spacer>
                    <LeaderboardRow firstName={"Matache"} lastName={"Alexandru"} points={50}/>
                    <Spacer size={20}></Spacer>
                    <LeaderboardRow firstName={"Matachesdasda"} lastName={"Alexandru"} points={500}/>
                    <Spacer size={20}></Spacer>
                <LeaderboardRow firstName={"Matache"} lastName={"Alexandrufasvdsvsaaadvasva"} points={150}/>
                </div>
            {/*</div>*/}
        </div>
    );
};

export default Leaderboard;
