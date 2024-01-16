import { IconBookmark, IconHeart, IconShare } from "@tabler/icons-react";
import {
  Card,
  Image,
  Text,
  ActionIcon,
  Badge,
  Group,
  Center,
  Avatar,
  useMantineTheme,
  rem,
  Tooltip,
  Modal,
  Button,
} from "@mantine/core";
import classes from "./ProjectCard.module.css";
import { Avatars } from "./Avatars";
import { getDecodedJWT } from "../AuthContext";
import { useEffect, useState } from "react";
import axios from "axios";
import { DataTable } from "mantine-datatable";

const CustomModal = ({ onClose, tasks }) => {
  return (
    <Modal
      opened
      onClose={onClose}
      title={`View Tasks`}
      overlayProps={{
        backgroundOpacity: 0.55,
        blur: 3,
      }}
    >
      <DataTable
        striped
        highlightOnHover
        columns={[
          { accessor: "name" },
          { accessor: "description", width: 150 },
          { accessor: "deadline" },
          { accessor: "statusId" },
          { accessor: "userId" },
        ]}
        records={tasks}
      />

      <Button fullWidth onClick={onClose}>
        Close
      </Button>
    </Modal>
  );
};

export function ProjectCard({ title, description, id, createdBy }) {
  const linkProps = {
    target: "_blank",
  };
  const theme = useMantineTheme();
  const jwtToken = getDecodedJWT();

  const [teacher, setTeacher] = useState("Test");
  const [tasks, setTasks] = useState([]);
  useEffect(() => {
    axios
      .get("/api/Users/" + createdBy, {
        headers: {
          Authorization: `Bearer ${jwtToken.jwt}`,
        },
      })
      .then((response) => {
        let t = response.data.response;
        setTeacher(t.firstName + " " + t.lastName);
      })
      .catch((error) => {
        console.error("Error fetching :", error);
      })
      .then(() => {
        axios
          .get("/api/Project/" + id + "/tasks", {
            headers: {
              Authorization: `Bearer ${jwtToken.jwt}`,
            },
          })
          .then((response) => {
            setTasks(response.data.response);
          })
          .catch((error) => {
            console.error("Error fetching :", error);
          });
      });
  }, [id, createdBy]);

  const [modalOpen, setModalOpen] = useState(false);
  const [modalParams, setModalParams] = useState({});

  const showModal = (params) => {
    setModalParams(params);
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
  };

  return (
    <Card withBorder radius="md" className={classes.card}>
      {modalOpen && <CustomModal onClose={closeModal} tasks={tasks} />}

      <Card.Section>
        <a {...linkProps} onClick={() => showModal(id)}>
          <Image src="https://picsum.photos/800/800" height={180} />
        </a>
      </Card.Section>

      <Badge
        className={classes.rating}
        variant="gradient"
        gradient={{ from: "yellow", to: "red" }}
      >
        outstanding
      </Badge>

      <Text className={classes.title} fw={500}>
        {title}
      </Text>

      <Text fz="sm" c="dimmed" lineClamp={4}>
        {description}
      </Text>

      <Group justify="space-between" className={classes.footer}>
        <Avatars teacher={teacher} />

        <Group gap={8} mr={0}>
          <ActionIcon className={classes.action}>
            <IconHeart
              style={{ width: rem(16), height: rem(16) }}
              color={theme.colors.red[6]}
            />
          </ActionIcon>
          <ActionIcon className={classes.action}>
            <IconBookmark
              style={{ width: rem(16), height: rem(16) }}
              color={theme.colors.yellow[7]}
            />
          </ActionIcon>
          <ActionIcon className={classes.action}>
            <IconShare
              style={{ width: rem(16), height: rem(16) }}
              color={theme.colors.blue[6]}
            />
          </ActionIcon>
        </Group>
      </Group>
    </Card>
  );
}
