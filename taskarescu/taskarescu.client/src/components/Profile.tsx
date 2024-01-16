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
import { FaMoon, FaSun } from "react-icons/fa";
import classes from "./Profile.module.css";
const RewardCol = ({ icon, backgroundcolor, size }) => {
  return (
    // <Grid.Col span={2}>
    <RewardBadge icon={icon} backgroundcolor={backgroundcolor} size={size} />
    // </Grid.Col>
  );
};

export function Profile() {
  return (
    <Container size="lg" py="xl">
      <Group justify="center">
        <Badge variant="gradient" size="xl">
          username's profile
        </Badge>
      </Group>

      <Title order={2} className={classes.title} ta="center" mt="sm">
        Integrate effortlessly with any technology stack
      </Title>

      <Text c="dimmed" className={classes.description} ta="center" mt="md">
        Every once in a while, you’ll see a Golbat that’s missing some fangs.
        This happens when hunger drives it to try biting a Steel-type Pokémon.
      </Text>

      <Space h="xl" />

      <SimpleGrid cols={{ base: 2, sm: 4, lg: 6 }} spacing="lg">
        <RewardCol icon={FaMoon} backgroundcolor="begginer" size={100} />

        <RewardCol icon={FaMoon} backgroundcolor="intermediate" size={100} />

        <RewardCol icon={FaSun} backgroundcolor="master" size={100} />

        <RewardCol icon={FaMoon} backgroundcolor="master" size={100} />

        <RewardCol icon={FaSun} backgroundcolor="master" size={100} />

        <RewardCol icon={FaSun} backgroundcolor="master" size={100} />

        <RewardCol icon={FaMoon} backgroundcolor="master" size={100} />
      </SimpleGrid>
    </Container>
  );
}
