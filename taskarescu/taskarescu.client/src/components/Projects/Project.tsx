import { DataTable, useDataTableColumns } from "mantine-datatable";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { getDecodedJWT } from "../AuthContext";
import { useNavigate } from "react-router-dom";
import { ActionIcon, Button, Group, Modal, Box, TextInput, Select, NumberInput } from "@mantine/core";
import { DateInput } from '@mantine/dates';

import axios from "axios"
import { IconArrowAutofitRight, IconArrowLeft, IconArrowRight, IconEdit, IconPlus, IconTrash } from "@tabler/icons-react";

const FeedbackModal = ({
    onClose,
    task,
    action
}) => {
    const feedback = task.feedback;
    console.log(action);

    let initialFeedbackDescription = '';
    let initialFeedbackPoints = 0;
    let initialFeedbackDifficulty = '';
    let initialFeedbackDifficultyId = 0;

    if (action === 'edit') {
        initialFeedbackDescription = task.feedback.description;
        initialFeedbackPoints = task.feedback.points;
        initialFeedbackDifficulty = task.feedback.difficultyName;
        initialFeedbackDifficultyId = task.feedback.difficultyId;
    }

    const [feedbackDescription, setFeedbackDescription] = useState(initialFeedbackDescription);
    const [feedbackPoints, setFeedbackPoints] = useState(initialFeedbackPoints);
    const [feedbackDifficulty, setFeedbackDifficulty] = useState(initialFeedbackDifficulty);
    const [feedbackDifficultyId, setFeedbackDifficultyId] = useState(initialFeedbackDifficultyId);

    
    const [error, setError] = useState(null);

    useEffect(() => {
        if (feedback != null) {
            setFeedbackDescription(feedback.description);
            setFeedbackPoints(feedback.points);
            setFeedbackDifficulty(feedback.difficultyName);
            setFeedbackDifficultyId(feedback.difficultyId);
        }
      }, [feedback]);

    const handleDescriptionChange = (event) => {
        const value = event.currentTarget.value;
        setFeedbackDescription(value);
    }

    const handlePointsChange = (value) => {
        setFeedbackPoints(value);
    }

    const handleDifficultyChange = (value) => {
        setFeedbackDifficulty(value);
        
        switch(value) {
            case 'Easy':
                setFeedbackDifficultyId(1);
                break;
            case 'Moderate':
                setFeedbackDifficultyId(2);
                break;
            case 'Intermediate':
                setFeedbackDifficultyId(3);
                break;
            case 'Challenging':
                setFeedbackDifficultyId(4);
                break;
            case 'Advanced':
                setFeedbackDifficultyId(5);
                break;
            default:
                console.log("cumva ai ratat dificultatea")
        }
    }

    const handleSaveChanges = async () => {
        console.log(feedbackPoints);
        if (!feedbackPoints) {
            setError("Eroare: Feedback-ul trebuie sa aiba punctaj!");
            return;
        }

        const feedbackDto = {
            description: feedbackDescription,
            points: feedbackPoints,
            difficultyId: feedbackDifficultyId,
        };

        try {
            if (action === 'edit') {
                await axios.put(`/api/Users/${getDecodedJWT().nameIdentifier}/tasks/${task.id}/feedback`, feedbackDto, {
                    headers: {
                    Authorization: `Bearer ${getDecodedJWT().jwt}`,
                    },
                });
            } else {
                await axios.post(`/api/Users/${getDecodedJWT().nameIdentifier}/tasks/${task.id}/feedback`, feedbackDto, {
                    headers: {
                    Authorization: `Bearer ${getDecodedJWT().jwt}`,
                    },
                });
            }

            onClose(task);

        } catch (err) {
        if (err.response) {
            const errorMessage = err.response.data.errors && err.response.data.errors.length
            ? err.response.data.errors[0]
            : 'Eroare necunoscuta';

            setError(`Eroare: ${errorMessage}`);
        }
        console.log(err);
        }
    }

    return (
        <Modal
            opened
            onClose={onClose}
            title={action==='edit' ? `View feedback for ${task.name}` : `Create feedback for ${task.name}`}
            overlayProps={{
                backgroundOpacity: 0.55,
                blur: 3,
            }}
            size="md"
            centered
        >
            <TextInput 
                label="Description"
                defaultValue={feedbackDescription}
                onChange={(v) => {handleDescriptionChange(v)}}
            />
            <br/>
            <NumberInput
                label="Points (0 - 10)"
                defaultValue={feedbackPoints}
                allowDecimal={false}
                min={1}
                max={10}
                onChange={handlePointsChange}
                required
            />
            <br />
            <Select
                label="Difficulty"
                value={feedbackDifficulty}
                data={["Easy", "Moderate", "Intermediate", "Challenging", "Advanced"]}
                onChange={handleDifficultyChange}
            />
            <br />
            {error && <p style={{ color: "red" }}>{error}</p>}

            <Group justify="center" wrap="nowrap">
                <Button fullWidth color="blue" onClick={() => handleSaveChanges()}>
                    Save
                </Button>
                <Button fullWidth onClick={() => onClose(task)}>
                    Close
                </Button>
            </Group>
        </Modal>
    )
}

const TaskModal = ({
    onClose,
    task,
    action,
    usersAssign
}) => {
    const { projectId } = useParams();

    let initialName = ''
    let initialDescription = ''
    let initialUsername = ''
    let initialStatus = ''
    let initialDeadline = null;

    if (action === 'edit') {
        initialName = task.name
        initialDescription = task.description
        initialUsername = task.username
        initialStatus = task.statusName
        initialDeadline = new Date(task.deadline)
    }

    const [taskName, setTaskName ] = useState(initialName);
    const [taskDescription, setTaskDescription] = useState(initialDescription);
    const [taskDeadline, setTaskDeadline] = useState<Date | null>(initialDeadline);
    const [taskUsername, setTaskUsername] = useState(initialUsername);
    const [taskStatus, setTaskStatus] = useState(initialStatus);
    const [error, setError] = useState(null);

    const handleNameChange = (event) => {
        const value = event.currentTarget.value;
        setTaskName(value);
    }

    const handleDescriptionChange = (event) => {
        const value = event.currentTarget.value;
        setTaskDescription(value);
    }

    const handleDeadlineChange = (value) => {
        setTaskDeadline(value);
    }

    const handleUsernameChange = (value) => {
        setTaskUsername(value);
    }

    const handleStatusChange = (value) => {
        setTaskStatus(value);
    }

    const handleSaveChanges = () => {
        if (!taskName) {
            setError('Eroare: Campul \'Name\' nu a fost completat!')
            return;
        }

        const taskDto = {
            name: taskName,
            description: taskDescription,
            deadline: taskDeadline,
            username: taskUsername,
            statusName: taskStatus
        };

        let axiosCall;
        let url;

        if (action === 'add') {
            axiosCall = axios.post;
            url = `/api//Project/${projectId}/tasks`
        } else {
            axiosCall = axios.put;
            url = `/api//Project/${projectId}/tasks/${task.id}`
        }

        axiosCall(url, taskDto, {
            headers: {
                Authorization: `Bearer ${getDecodedJWT().jwt}`
            }
        }).then(response => {
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
          });
    }

    return (
        <Modal
            opened
            onClose={onClose}
            title={`${action === 'edit' ? "View Task " + task.name : "Create Task"}`}
            overlayProps={{
                backgroundOpacity: 0.55,
                blur: 3,
            }}
            size="lg"
            centered
        >
            
            <TextInput
                label="Name"
                defaultValue={taskName}
                onChange={handleNameChange}
                required
            />
            <br/> 
            <TextInput
                label="Description"
                defaultValue={taskDescription}
                onChange={handleDescriptionChange}
            />
            <br/> 

            {/* nu stiu ce se petrece dar cand pun un date input calendarul e pocit ca drq */}
            <DateInput
                value={taskDeadline}
                onChange={handleDeadlineChange}
                label="Deadline"
                nextIcon={<ActionIcon color="cyan" size="xs"><IconArrowRight/></ActionIcon>}
                previousIcon={<ActionIcon color="cyan" size="xs"><IconArrowLeft/></ActionIcon>}
            />
            <br/> 

            <Select
                label="User assigned"
                data={usersAssign}
                value={taskUsername}
                onChange={handleUsernameChange}
            />
            <br/> 

            <Select
                label="Status"
                data={["To Do", "In Progress", "Done"]}
                value={taskStatus}
                onChange={handleStatusChange}
            />
            <br/>

            <Group justify="center" wrap="nowrap">
                <Button fullWidth color="blue" onClick={handleSaveChanges}>
                    Save
                </Button>
                <Button fullWidth color="blue" onClick={onClose}>
                    Close
                </Button>
            </Group>
        </Modal>
    )
}

export function Project() {
    const { projectId } = useParams();
    const jwtToken = getDecodedJWT();
    const navigate = useNavigate();
    
    const [tasks, setTasks] = useState([]);
    const [taskModalOpen, setTaskModalOpen] = useState(false);
    const [taskModalParams, setTaskModalParams] = useState({});
    const [usersAssign, setUsersAssign] = useState([]);
    const [feedbackModalOpen, setFeedbackModalOpen] = useState(false);
    const [feedbackModalParams, setFeedbackModalParams] = useState([]);

    const deleteTask = (taskId) => {

        axios
        .delete(`/api/tasks/${taskId}`, {
            headers: {
                Authorization: `Bearer ${getDecodedJWT().jwt}`
            }
        })
        .then(response => {
            setTasks(prevTasks => prevTasks.filter(task => task.id !== taskId));
        })
        .catch(err => {console.log(err)})
    
    }

    const showTaskModal = (params, actionType) => {
        setTaskModalParams({...params, actionType});
        setTaskModalOpen(true);
    }

    const closeTaskModal = () => {
        setTaskModalOpen(false);
    }

    const showFeedbackModal = (params, actionType) => {
        setFeedbackModalParams({...params, actionType});
        setFeedbackModalOpen(true);
    }

    const closeFeedbackModal = async (task) => {
        console.log(task.feedback);

        try {
            const feedbackResponse = await axios.get(`/api/tasks/${task.id}/feedback`, {
                headers: {
                    Authorization: `Bearer ${getDecodedJWT().jwt}`
                }
            });
            
            console.log(feedbackResponse);

            if (feedbackResponse.data.response) {
    
                const updatedTasks = tasks.map(t => {
                    if (t.id === task.id) {
                        return { ...t, feedback: feedbackResponse.data.response };
                    }
                    return t;
                });

                setTasks(updatedTasks);
            }

            setFeedbackModalOpen(false);
        } catch (error) {
            console.error("Error fetching feedback for task:", error);
        }

    }

    useEffect(() => {
        const fetchData = async () => {
            try {
                const tasksResponse = await axios.get(`/api/Project/${projectId}/tasks`, {
                    headers: {
                        Authorization: `Bearer ${jwtToken.jwt}`
                    },
                });
    
                const formattedTasks = tasksResponse.data.response.map(task => {
                    const formattedTask = {
                        ...task,
                        deadlineFormatted: task.deadline ? new Date(task.deadline).toLocaleDateString("en-GB", {
                            day: "numeric",
                            month: "short",
                            year: "numeric",
                        }) : null,
                    };
    
                    return formattedTask;
                });
    
                setTasks(formattedTasks);
    
                const feedbackPromises = formattedTasks.map(async (formattedTask) => {
                    try {
                        const feedbackResponse = await axios.get(`/api/tasks/${formattedTask.id}/feedback`, {
                            headers: {
                                Authorization: `Bearer ${jwtToken.jwt}`
                            }
                        });
    
                        setTasks((prevTasks) => {
                            const updatedTasks = [...prevTasks];
                            const index = updatedTasks.findIndex(t => t.id === formattedTask.id);
                            updatedTasks[index] = { ...formattedTask, feedback: feedbackResponse.data.response };

                            return updatedTasks;
                        });
                    } catch (error) {
                        console.error("Error fetching feedback for task:", error);
                    }
                });
    
                await Promise.all(feedbackPromises);
    
            } catch (error) {
                console.error("Error fetching tasks:", error);
            }
        };

        fetchData();
    
    }, [projectId, jwtToken.jwt, taskModalOpen]);

    useEffect(() => {
        axios
            .get(`/api/Project/${projectId}/students`, {
                headers: {
                    Authorization: `Bearer ${getDecodedJWT().jwt}`
                }
            })
            .then((response) => {
                setUsersAssign(response.data.response)
            })
            .catch(err => {
                console.log(err);
            })
    }, [projectId]);

    console.log("Tasks:", tasks);

    return (
    <div>
        <DataTable
            striped
            highlightOnHover
            groups={[
            {
                id: "task",
                columns: getDecodedJWT().role === 'Student' ? [
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
                    accessor: "deadlineFormatted",
                    align: "center",
                    headerAlign: "center",
                    sortable: true,
                    title: "Deadline"
                },
                {
                    accessor: "statusName",
                    align: "center",
                    headerAlign: "center",
                    sortable: true,
                    title: "Status",
                },
                {
                    accessor: "username",
                    align: "center",
                    headerAlign: "center",
                    sortable: true,
                    title: "Username",
                },
                {
                    accessor: "taskActions",
                    title: <Box mr={6}>Task Actions</Box>,
                    textAlign: "center",
                    render: (task) => task.feedback === null &&
                         (
                            <Group gap={2} justify="center" wrap="nowrap">
                                <ActionIcon
                                    size="sm"
                                    variant="subtle"
                                    color="blue"
                                    onClick={() => showTaskModal(task, 'edit')}
                                >
                                    <IconEdit size={16}/>
                                </ActionIcon>
                                <ActionIcon
                                    size="sm"
                                    variant="subtle"
                                    color="red"
                                    onClick={() => deleteTask(task.id)}
                                >
                                    <IconTrash size={16}/>
                                </ActionIcon>
                            </Group>
                        )
                    
                }
                ] : [
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
                        accessor: "deadlineFormatted",
                        align: "center",
                        headerAlign: "center",
                        sortable: true,
                        title: "Deadline"
                    },
                    {
                        accessor: "statusName",
                        align: "center",
                        headerAlign: "center",
                        sortable: true,
                        title: "Status",
                    },
                    ],
            },
            {
                id: "feedback",
                columns: getDecodedJWT().role !== 'Student' ? [
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
                    accessor: "feedback.difficultyName",
                    align: "center",
                    headerAlign: "center",
                    sortable: true,
                    title: "Difficulty",
                },
                {
                    accessor: "feedbackActions",
                    title: <Box mr={6}>Feedback Actions</Box>,
                    textAlign: "center",
                    render: (task) => (
                        task.feedback !== null ? (
                            <ActionIcon
                                size="sm"
                                variant="subtle"
                                color="blue"
                                onClick={() => showFeedbackModal(task, 'edit')}
                            >
                                <IconEdit size={16}/>
                            </ActionIcon>
                        ) : (
                            <ActionIcon 
                                size="sm"
                                variant="subtle"
                                color="green"
                                onClick={() => showFeedbackModal(task, 'add')}
                            >
                                <IconPlus size={16} />
                            </ActionIcon>
                        )
                        
                    ),
                }
                ] : [
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
                        accessor: "feedback.difficultyName",
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
        <br></br>
        <Group>
            <Button onClick={() => navigate("/projects")}>
                Go back
            </Button>
            { getDecodedJWT().role === 'Student' && (
                <Button onClick={() => showTaskModal(null, 'add')}>
                    Add Task
                </Button>
            )}
        </Group>

        {taskModalOpen && (
            <TaskModal onClose={closeTaskModal}
            task={taskModalParams}
            usersAssign={usersAssign}
            action={taskModalParams.actionType}
            />
        )}
        {feedbackModalOpen && (
            <FeedbackModal onClose={() => closeFeedbackModal(feedbackModalParams)} 
            task={feedbackModalParams} 
            action={feedbackModalParams.actionType}/>
        )}

    </div>
    );
}