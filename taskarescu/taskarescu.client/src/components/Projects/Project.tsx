import { DataTable } from "mantine-datatable";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { getDecodedJWT } from "../AuthContext";
import { useNavigate } from "react-router-dom";
import { ActionIcon, Button, Group, Modal, Box, TextInput, Select } from "@mantine/core";
import { DatePickerInput } from '@mantine/dates';

import axios from "axios"
import { IconEdit, IconTrash } from "@tabler/icons-react";

const TaskModal = ({
    onClose,
    task,
    usersAssign
}) => {
    const [taskName, setTaskName ] = useState(task.name);
    const [taskDescription, setTaskDescription] = useState(task.description);
    // const [taskDeadline, setTaskDeadline] = useState(task.deadline);
    const [taskUsername, setTaskUsername] = useState(task.username);
    const [taskStatus, setTaskStatus] = useState(task.statusName);

    const handleNameChange = (value) => {
        setTaskName(value);
    }

    const handleDescriptionChange = (value) => {
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

    return (
        <Modal
            opened
            onClose={onClose}
            title={`View Task ${task.name}`}
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
            />
            <br/> 
            <TextInput
                label="Description"
                defaultValue={taskDescription}
                onChange={handleDescriptionChange}
            />

            {/* nu stiu ce se petrece dar cand pun un date picker calendarul e pocit ca drq */}
            {/* <DatePickerInput
                // valueFormat="YYYY MMM DD"
                // type="multiple"
                label="Deadline"
                
                // placeholder="Pick date"
            /> */}
            <br />

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
                <Button fullwidth color="blue" onClick={onClose}>
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
    const [showTaskCol, setShowTaskCol] = useState(false);

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

    const showTaskModal = (params) => {
        setTaskModalParams(params);
        setTaskModalOpen(true);
    }

    const closeTaskModal = () => {
        setTaskModalOpen(false);
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
                        deadline: task.deadline ? new Date(task.deadline).toLocaleDateString("en-GB", {
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
    
    }, [projectId, jwtToken.jwt]);

    useEffect(() => {
        if (jwtToken.role === "student") {
            console.log("STUDENT")
            setShowTaskCol(false);
        } else {
            console.log("ADMIN PROF")
            setShowTaskCol(true);
        }
    }, [jwtToken.role])

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
                    show: showTaskCol,
                    render: (task) => task.feedback === null &&
                         (
                            <Group gap={2} justify="center" wrap="nowrap">
                                <ActionIcon
                                    size="sm"
                                    variant="subtle"
                                    color="blue"
                                    onClick={() => showTaskModal(task)}
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
        <Button onClick={() => navigate("/projects")}>
            Go back
        </Button>

        {taskModalOpen && (
            <TaskModal onClose={closeTaskModal} task={taskModalParams} usersAssign={usersAssign}/>
        )}
    </div>
    );
}