import { DataTable } from "mantine-datatable";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { getDecodedJWT } from "../AuthContext";

import axios from "axios"

export function Project() {
    const { projectId } = useParams();
    const jwtToken = getDecodedJWT();
    
    const [tasks, setTasks] = useState([]);

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

    console.log("Tasks:", tasks);

    return (
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
    );
}