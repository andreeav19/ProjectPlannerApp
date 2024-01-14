import React, { ReactNode } from "react";
import "./Button.css";

interface ButtonProps {
    onPress?: () => void;
    children?: ReactNode;
}

const Button: React.FC<ButtonProps> = ({ onPress, children }) => {
    return (
        <button className="customButton" onClick={onPress}>
            {children}
        </button>
    );
};

export default Button;
