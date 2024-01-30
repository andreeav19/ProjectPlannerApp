import { SimpleGrid,
  Group, 
  Button, 
  Modal, 
  TextInput, 
  Card, 
  Image, 
  Text, 
  ActionIcon,
  useMantineTheme,
  rem,
  Badge
} from "@mantine/core";
import { getDecodedJWT } from "../AuthContext";
import { IconEdit, IconTrash } from "@tabler/icons-react";
import classes from "./ProjectCard.module.css";
import { Avatars } from "./Avatars";
import { useEffect, useState } from "react";
import axios from "axios";
import { useSpring, animated} from "react-spring";
import { useNavigate } from "react-router-dom";

const ProjectModal = ({
  onClose,
  project,
  action
}) => {
  let initialProjectDescription = ''
  let initialProjectName = ''

  if (action === 'edit') {
    initialProjectDescription = project.description;
    initialProjectName = project.name;
  }

  const [projectName, setProjectName] = useState(initialProjectName);
  const [projectDescription, setProjectDescription] = useState(initialProjectDescription);
  const [error, setError] = useState(null);

  const handleNameChange = (event) => {
    const value = event.currentTarget.value;
    setProjectName(value);
  }

  const handleDescription = (event) => {
    const value = event.currentTarget.value;
    setProjectDescription(value);
  }

  const handleSaveChanges = () => {
    if (!projectName) {
      setError('Eroare: Campul \'Name\' nu a fost completat!');
      return;
    }

    const projectDto = {
      name: projectName,
      description: projectDescription
    };

    let axiosCall;
    let paramId;
    if (action === 'add') {
      paramId = getDecodedJWT().nameIdentifier;
      axiosCall = axios.post
    } else {
      paramId = project.id
      axiosCall = axios.put
    }

    axiosCall(`/api/Project/${paramId}`, projectDto, {
      headers: {
        Authorization: `Bearer ${getDecodedJWT().jwt}`
      }
    })
    .then(response => {
      onClose();
    })
    .catch(err => {
      if (err.response) {
        const errorMessage =
        err.response.data.errors && err.response.data.errors.length
          ? err.response.data.errors[0]
          : "Eroare necunoscuta";

        setError(`Eroare: ${errorMessage}`);
      }
      console.log(err)
    })
  }

  return (
    <Modal
      opened
      onClose={onClose}
      title={action === 'edit' ? `View project ${project.name}` : `Create project`}
      overlayProps={{
        backgroundOpacity: 0.55,
        blur: 3,
      }}
      size="md"
    >
      <TextInput 
          label="Name"
          defaultValue={projectName}
          onChange={handleNameChange}
      />
      <br/>
      <TextInput 
          label="Description"
          defaultValue={projectDescription}
          onChange={handleDescription}
      />
      <br/>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <br/>
      <Group justify="center" wrap="nowrap">
          <Button fullWidth color="blue" onClick={() => handleSaveChanges()}>
              Save
          </Button>
          <Button fullWidth onClick={() => onClose()}>
              Close
          </Button>
      </Group>
    </Modal>
  )
}

export function Projects() {
  const [projects, setProjects] = useState([]);  
  const [projectModalOpen, setProjectModalOpen] = useState(false);
  const [projectModalParams, setProjectModalParams] = useState({});
  const [toggleRender, setToggleRender] = useState(false);

  const showProjectModal = (params, action) => {
    setProjectModalParams({...params, action});
    console.log(projectModalOpen);
    setProjectModalOpen(true);
  }

  const closeProjectModal = () => {
    console.log(projectModalOpen);
    setProjectModalOpen(false);
    setToggleRender(!toggleRender);
  }

  const deleteProject = (projectId) => {
    axios
    .delete(`/api/Project/${projectId}`, {
      headers: {
        Authorization: `Bearer ${getDecodedJWT().jwt}`
      }
    })
    .then(response => {
      setToggleRender(!toggleRender);
    })
    .catch(err => {
      console.log(err);
    })
  }

  useEffect(() => {
    const jwtToken = getDecodedJWT();
    const userId = jwtToken.nameIdentifier;

    axios
      .get("/api/Project/users/" + userId, {
        headers: {
          Authorization: `Bearer ${jwtToken.jwt}`,
        },
      })
      .then((response) => {
        setProjects(response.data.response);
      })
      .catch((error) => {
        console.error("Error fetching projects:", error);
      });

  }, [toggleRender]);

  const ProjectCard = ({ title, description, id, createdBy, project }) => {
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
  
    return (
      <animated.div
        onMouseEnter={() => setHovered(true)}
        onMouseLeave={() => setHovered(false)}
        style={springProps}
      >
      <Card withBorder radius="md" className={classes.card}>
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
  
          { (getDecodedJWT().role !== 'Student') ? (<Group gap={8} mr={0}>
            <ActionIcon className={classes.action}>
              <IconEdit
                style={{ width: rem(16), height: rem(16) }}
                color={theme.colors.blue[6]}
                onClick={() => showProjectModal(project, 'edit')}
              />
            </ActionIcon>
            <ActionIcon className={classes.action}>
              <IconTrash
                style={{ width: rem(16), height: rem(16) }}
                color={theme.colors.red[7]}
                onClick={() => deleteProject(project.id)}
              />
            </ActionIcon>
          </Group> ) : (null)}
        </Group>
      </Card>
      </animated.div>
    );
  }

  return (
    <div>
      {projectModalOpen && (
        <ProjectModal 
        onClose={closeProjectModal} 
        project={projectModalParams}
        action={projectModalParams.action} />
      )}
      <Button
        color="cyan"
        onClick={() => showProjectModal(null, 'add')}
      >
        Create Project
      </Button>
      <br /> <br />
      <SimpleGrid cols={3} spacing="lg" verticalSpacing="sm" mx="xl">
        {projects.map((item) => (
          <ProjectCard
            key={item.id}
            title={item.name}
            description={item.description}
            id={item.id}
            createdBy={item.userId}
            project={item}
          />
        ))}
      </SimpleGrid>
      
    </div>
  );
}
