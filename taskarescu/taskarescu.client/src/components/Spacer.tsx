import React from "react";

interface SpacerProps {
    size: number;
}

const Spacer: React.FC<SpacerProps> = ({ size }: SpacerProps) => {
    return <div style={{ height: size, width: size }} />;
};

export default Spacer;
