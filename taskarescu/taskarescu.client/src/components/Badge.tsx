import "./Badge.css"
import React from "react";

interface BadgeProps {
    imageSrc: string;
    text: string;
}

const Badge: React.FC<BadgeProps> = ({ imageSrc, text }) => {
    return (
        <div className="textBadge">
            <div className="borderImage">
                <img src={imageSrc} alt="Badge" style={{
                    height: 150,
                    width: 150,
                    borderRadius: 75,
                }}/>
            </div>
            <p className="text-lg font-semibold">{text}</p>
        </div>
    );
};
// className="borderImage"
export default Badge;
