import {
  Group,
  Grid,
  SimpleGrid,
  Container,
  Text,
  Title,
  Divider,
  Space,
  Badge,
} from "@mantine/core";
import { RewardBadge } from "./RewardBadge";
import { FaBook, FaMoon, FaStar, FaSun } from "react-icons/fa";
import classes from "./Profile.module.css";
import { getDecodedJWT } from "./AuthContext";
import { v4 as uuidv4 } from "uuid";
import { MdOutlineRamenDining } from "react-icons/md";
import { GiDeadWood } from "react-icons/gi";
import { SiKatana } from "react-icons/si";
import { useEffect, useState } from "react";
import axios from "axios";

const RewardCol = ({ icon, backgroundcolor, size, name, description }) => {
  return (
    // <Grid.Col span={2}>
    <RewardBadge
      icon={icon}
      backgroundcolor={backgroundcolor}
      size={size}
      name={name}
      description={description}
    />
    // </Grid.Col>
  );
};

const mapToRewardCols = (apiElement) => {
  const stringToIndex = (str, maxIndex) => {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
      hash = (hash << 5) - hash + str.charCodeAt(i);
    }
    return (hash >>> 0) % maxIndex;
  };

  const getRandomIcon = (str) => {
    const fontAwesomeIcons = [
      FaMoon,
      FaStar,
      GiDeadWood,
      FaBook,
      SiKatana,
      MdOutlineRamenDining,
    ];
    const maxIndex = fontAwesomeIcons.length;

    const index = stringToIndex(str, maxIndex);
    return fontAwesomeIcons[index];
  };

  // Function to determine the rank based on the length of the description
  const getRank = (descriptionLength) => {
    if (descriptionLength < 20) {
      return "beginner";
    } else if (descriptionLength < 50) {
      return "intermediate";
    } else {
      return "master";
    }
  };

  const { name, description } = apiElement;

  return (
    <RewardCol
      icon={getRandomIcon(name)}
      backgroundcolor={getRank(description.length)}
      size={100}
      name={name}
      description={description}
      key={uuidv4()}
    />
  );
};

export function Profile() {
  const [data, setData] = useState([]);

  useEffect(() => {
    let jwt = getDecodedJWT();
    axios
      .get("/api/Users/" + jwt.nameIdentifier + "/badges", {
        headers: {
          Authorization: `Bearer ${jwt.jwt}`,
        },
      })
      .then((response) => {
        setData(response.data.response);
      })
      .catch((error) => {
        console.error("Error fetching projects:", error);
      });
  });

  return (
    <Container size="lg" py="xl">
      <Group justify="center">
        <Badge variant="gradient" size="xl">
          {getDecodedJWT().name}'s profile
        </Badge>
      </Group>

      <Title order={2} className={classes.title} ta="center" mt="sm">
        View your progress
      </Title>
      <Group justify="center">
        <Text c="dimmed" className={classes.description} ta="center" mt="md">
          Mastering the art of progress, one step at a time. Embrace the
          journey, honor the process, and witness the evolution of your true
          self in the dojo of self-improvement.
        </Text>
      </Group>
      <Space h="xl" />

      <SimpleGrid cols={{ base: 2, sm: 4, lg: 6 }} spacing="lg">
        {data.map(mapToRewardCols)}
      </SimpleGrid>
    </Container>
  );
}
