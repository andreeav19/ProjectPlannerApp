import React from "react";
import "./LeaderboardRow.css"
import Spacer from "./Spacer.tsx";

interface LeaderboardRowProps {
    firstName: string;
    lastName: string;
    points: number;
}

const LeaderboardRow: React.FC<LeaderboardRowProps> = ({ firstName, lastName, points }) => {
    return (
        <div className="LeaderRow">
            <Spacer size={10}></Spacer>
            <div>
                <p className="text-lg font-semibold">
                    {firstName} {lastName}
                </p>
            </div>
            <div className="puncte">
                <p className="text-lg font-semibold">{points} puncte</p>
            </div>
        </div>
    );
};

export default LeaderboardRow;
