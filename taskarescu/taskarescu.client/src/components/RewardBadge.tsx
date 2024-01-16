import React from "react";
import { IconType } from "react-icons";
import styled from "styled-components";
import { Card, Text, Group, Badge, Center } from "@mantine/core";
import classes from "./RewardBadge.module.css";
interface RewardBadgeProps {
  size: number;
  icon: IconType;
  backgroundcolor: string;
  bordercolor?: string;
  style?: React.CSSProperties;
}

const HexagonContainer = styled.div<{
  size: number;
  bordercolor: string;
  backgroundcolor: string;
}>`
  position: relative;
  width: ${({ size }) => size}px;
  height: ${({ size }) => size}px;
  background: ${({ bordercolor }) => bordercolor};
  display: inline-block;
  position: relative;
  clip-path: polygon(50% 0%, 100% 25%, 100% 75%, 50% 100%, 0% 75%, 0% 25%);
  overflow: hidden;

  &::before {
    content: "";
    position: absolute;
    top: 4px;
    left: 4px;
    height: calc(100% - 8px);
    width: calc(100% - 8px);
    background: ${({ backgroundcolor }) =>
      `var(--mantine-color-${backgroundcolor}-7)`};
    clip-path: polygon(50% 0%, 100% 25%, 100% 75%, 50% 100%, 0% 75%, 0% 25%);
    box-sizing: border-box;
  }
`;

const HexagonIcon = styled.div<{ size: number }>`
  position: absolute;
  top: 54%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: ${({ size }) => size / 2}px;
  color: white;
  z-index: 1;
`;

export const RewardBadge: React.FC<RewardBadgeProps> = ({
  size,
  bordercolor,
  backgroundcolor,
  icon: Icon,
  style,
}) => {
  backgroundcolor = backgroundcolor || "ffffff";
  bordercolor = bordercolor || "#000000";
  return (
    <Card
      shadow="sm"
      padding="lg"
      radius="md"
      withBorder
      className={classes.card}
    >
      <Card.Section>
        <Center>
          <HexagonContainer
            size={size}
            bordercolor={bordercolor}
            backgroundcolor={backgroundcolor}
            style={style}
          >
            <HexagonIcon size={size}>
              <Icon />
            </HexagonIcon>
          </HexagonContainer>
        </Center>
      </Card.Section>

      <Group justify="space-between" mt="md" mb="xs">
        <Text fw={500}>Test frate</Text>
        <Badge
          autoContrast
          variant="gradient"
          gradient={{
            from: backgroundcolor + ".5",
            to: backgroundcolor + ".9",
            deg: 125,
          }}
        >
          {backgroundcolor}
        </Badge>
      </Group>

      <Text size="sm" c="dimmed">
        Buna mama
      </Text>
    </Card>
  );
};

export const RewardBadgeImage: React.FC<RewardBadgeProps> = ({
  size,
  bordercolor,
  backgroundcolor,
  icon: Icon,
  style,
}) => {
  backgroundcolor = backgroundcolor || "ffffff";
  bordercolor = bordercolor || "#000000";
  return (
    <HexagonContainer
      size={size}
      bordercolor={bordercolor}
      backgroundcolor={backgroundcolor}
      style={style}
    >
      <HexagonIcon size={size}>
        <Icon />
      </HexagonIcon>
    </HexagonContainer>
  );
};
