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
  Badge,
  Box
} from "@mantine/core";
import { getDecodedJWT } from "../AuthContext";
import { IconEdit, IconTrash, IconUserMinus, IconUsersGroup } from "@tabler/icons-react";
import classes from "./ProjectCard.module.css";
import { Avatars } from "./Avatars";
import { useEffect, useState } from "react";
import axios from "axios";
import { useSpring, animated} from "react-spring";
import { useNavigate } from "react-router-dom";
import { DataTable } from "mantine-datatable";

const StudentsModal = ({
  onClose,
  project
}) => {

  const [students, setStudents] = useState([]);
  const [toggleModified, setToggleModified] = useState(false);
  const [error, setError] = useState(null);
  const [username, setUsername] = useState('');

  const handleUsernameChange = (event) => {
    const value = event.currentTarget.value;
    setUsername(value);
  }

  useEffect(() => {
    const fetchUsersForProject = async () => {
      try {
        const studentsResponse = await axios.get(`/api/Project/${project.id}/students`, {
          headers: {
            Authorization: `Bearer ${getDecodedJWT().jwt}`,
          },
        });
  
        const studentUsernames = studentsResponse.data.response;
        console.log(studentUsernames);
  
        const userRequests = studentUsernames.map(async (username) => {
          try {
            const userResponse = await axios.get(`/api/Users/username/${username}`, {
              headers: {
                Authorization: `Bearer ${getDecodedJWT().jwt}`,
              },
            });
            return userResponse.data;
          } catch (error) {
            console.error(`Eroare la preluarea detaliilor utilizatorului ${username}:`, error);
            return null;
          }
        });
  
        const usersListResponses = await Promise.all(userRequests);
        const usersList = usersListResponses.map((user) => {
          if (user && user.response) {
            return user.response;
          }
          return null; 
        });
  
        setStudents(usersList.filter(user => user !== null)); // Filtrăm pentru a elimina valorile nule

  
      } catch (error) {
        console.error("Eroare la preluarea utilizatorilor pentru proiect:", error);
      }
    };

    fetchUsersForProject();
  }, [project.id, toggleModified]);

  const modifyStudents = (username, action) => {
    console.log(username, action);
  
    let axiosCall;
  
    if (action === 'add') {
      axios.post(`/api/Project/${project.id}/students/${username}`, null, {
        headers: {
          Authorization: `Bearer ${getDecodedJWT().jwt}`
        }
      })
        .then(response => {
          setToggleModified(!toggleModified);
        })
        .catch((err) => {
          if (err.response) {
            const errorMessage =
            err.response.data.errors && err.response.data.errors.length
              ? err.response.data.errors[0]
              : "Eroare necunoscuta";
  
            setError(`Eroare: ${errorMessage}`);
          }
          console.log(err)
        })
    } else {
      axios.delete(`/api/Project/${project.id}/students/${username}`, {
        headers: {
          Authorization: `Bearer ${getDecodedJWT().jwt}`
        }
      })
        .then(response => {
          setToggleModified(!toggleModified);
        })
        .catch((err) => {
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
  };

  return (

      <Modal
        opened
        onClose={onClose}
        title={`Project: ${project.name}`}
        overlayProps={{
          backgroundOpacity: 0.55,
          blur: 3,
        }}
        size="lg"
      >
        <DataTable
          withTableBorder
          records={students}
          columns={[
            {
              accessor:"userName",
              align: "left",
              headerAlign: "left",
              title: "Username",
            },
            {
              accessor:"firstName",
              align: "left",
              headerAlign: "left",
              title: "First Name",
            },
            {
              accessor:"lastName",
              align: "left",
              headerAlign: "left",
              title: "Last Name",
            },
            {
              accessor:"email",
              align: "left",
              headerAlign: "left",
              title: "Email",
            },
            {
              accessor: "actions",
              title: <Box mr={6}>Remove</Box>,
              textAlign: "center",
              render: (student) => (
                <Group gap={2} justify="center" wrap="nowrap">
                  <ActionIcon
                    size="sm"
                    variant="subtle"
                    color="red"
                    onClick={() => modifyStudents(student.userName, 'delete')}
                  >
                    <IconUserMinus size={16}/>
                  </ActionIcon>
                </Group>
              ),
            },
            

          ]} 
        >
          
        </DataTable>
        <br />
        {error && <p style={{ color: "red" }}>{error}</p>}
        <br />
        <Group wrap="nowrap" gap="lg" justify="center">

        <Button onClick={() => modifyStudents(username, 'add')}>
          Add to project
        </Button>
        <TextInput
        onChange={(v) => handleUsernameChange(v)}
        placeholder='Search student by username...'
        style={{ width: '50%' }}
        />

        </Group>
        
        <br />
        <Group justify="center" wrap="nowrap">
            <Button fullWidth onClick={() => onClose()}>
                Close
            </Button>
        </Group>
      </Modal>
  )
}


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
  const [studentsModalOpen, setStudentsModalOpen] = useState(false);
  const [studentsModalParams, setStudentsModalParams] = useState([]);

  const showStudentsModal = (params) => {
    setStudentsModalParams(params);
    setStudentsModalOpen(true);
  }

  const closeStudentsModal = () => {
    setStudentsModalOpen(false);
  }

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
              <IconUsersGroup 
                style={{ width: rem(16), height: rem(16) }}
                color={theme.colors.yellow[7]}
                onClick={() => showStudentsModal(project)}
              />
            </ActionIcon>
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
      {studentsModalOpen && (
        <StudentsModal 
        onClose={closeStudentsModal} 
        project={studentsModalParams}
        />
      )}
      {(getDecodedJWT().role != 'Student') &&
      <Button
        color="cyan"
        onClick={() => showProjectModal(null, 'add')}
      >
        Create Project
      </Button>
      }
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
