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
  Rating,
} from "@mantine/core";
import classes from "./ProjectCard.module.css";
import { Avatars } from "./Avatars";
import { getDecodedJWT } from "../AuthContext";
import { useEffect, useState } from "react";
import axios from "axios";
import { DataTable } from "mantine-datatable";
import { useSpring, animated} from "react-spring";
import { useNavigate } from "react-router-dom";


const CustomModal = ({ onClose, tasks }) => {
  const jwtToken = getDecodedJWT();
  useEffect(() => {
    tasks.map((task) => {
      axios
        .get("/api/tasks/" + task.id + "/feedback", {
          headers: {
            Authorization: `Bearer ${jwtToken.jwt}`,
          },
        })
        .then((response) => {
          let t = response.data.response;
          task["feedback"] = t;
        })
        .catch((error) => {
          task["feedback"] = "";
          console.error("Error fetching :", error);
        });
    });
  }, []);

  return (
    <Modal
      opened
      onClose={onClose}
      title={`View Tasks`}
      overlayProps={{
        backgroundOpacity: 0.55,
        blur: 3,
      }}
      size="70%"
    >
      <DataTable
        striped
        highlightOnHover
        groups={[
          {
            id: "task",
            columns: [
              {
                accessor: "name",
                align: "left",
                headerAlign: "left",
                sortable: true,
                title: "Task Name",
              },
              {
                accessor: "description",
                align: "left",
                headerAlign: "left",
                width: 150,
                sortable: true,
                title: "Task Description",
              },
              {
                accessor: "deadline",
                align: "center",
                headerAlign: "center",
                sortable: true,
                title: "Deadline",
              },
              {
                accessor: "statusId",
                align: "center",
                headerAlign: "center",
                sortable: true,
                title: "Status ID",
              },
              {
                accessor: "userId",
                align: "center",
                headerAlign: "center",
                sortable: true,
                title: "User ID",
              },
            ],
          },
          {
            id: "feedback",
            columns: [
              {
                accessor: "feedback.description",
                align: "left",
                headerAlign: "left",
                width: 150,
                sortable: true,
                title: "Description",
              },
              {
                accessor: "feedback.points",
                align: "center",
                headerAlign: "center",
                sortable: true,
                title: "Points",
              },
              {
                accessor: "feedback.difficultyId",
                align: "center",
                headerAlign: "center",
                sortable: true,
                title: "Difficulty",
              },
            ],
          },
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
  const navigate = useNavigate();

  const [teacher, setTeacher] = useState("Test");
  const [tasks, setTasks] = useState([]);
  const [hovered, setHovered] = useState(false);

  const springProps = useSpring({
    transform: hovered ? 'scale(1.05)' : 'scale(1)'
  });

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
            // console.log(response.data.response);
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
    <animated.div
      onMouseEnter={() => setHovered(true)}
      onMouseLeave={() => setHovered(false)}
      style={springProps}
    >
    <Card withBorder radius="md" className={classes.card}>
      {modalOpen && <CustomModal onClose={closeModal} tasks={tasks} />}

      <Card.Section>
        <a {...linkProps} onClick={() => navigate(`/project/${id}`)}>
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
    </animated.div>
  );
}
