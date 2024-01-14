import React, { useState } from 'react';
import Sidebar from "../components/Sidebar.tsx";
import Modal from 'react-modal';
// @ts-ignore
import Select, { ValueType } from 'react-select';
import "./HomeScreen.css";
import Spacer from "../components/Spacer.tsx";

interface Project {
    name: string;
    description: string;
    options: { value: string; label: string }[];
    tasks?: { name: string; studentName: string; status: string; difficulty: string; points: string; feedback: string }[];
}

const options = [
    { value: 'optiune1', label: 'To Do' },
    { value: 'optiune2', label: 'In Progress' },
    { value: 'optiune3', label: 'Done' },
];

const HomeScreen: React.FC = () => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [isTaskModalOpen, setTaskModalOpen] = useState(false);
    const [isFeedbackModalOpen, setFeedbackModalOpen] = useState(false);
    const [projects, setProjects] = useState<Project[]>([]);
    const [currentProjectIndex, setCurrentProjectIndex] = useState<number | null>(null);
    const [projectName, setProjectName] = useState("");
    const [projectDescription, setProjectDescription] = useState("");
    const [selectedOptions, setSelectedOptions] = useState<ValueType<{ value: string; label: string }>>([]);
    const [feedbackText, setFeedbackText] = useState("");
    const [taskName, setTaskName] = useState("");
    const [studentName, setStudentName] = useState("");
    const [taskStatus, setTaskStatus] = useState(options[0]);
    const [taskDifficulty, setTaskDifficulty] = useState("");
    const [taskPoints, setTaskPoints] = useState("");
    const [taskFeedback, setTaskFeedback] = useState("");
    const [editingTaskIndex, setEditingTaskIndex] = useState<number | null>(null);
    const [editedTaskName, setEditedTaskName] = useState("");
    const [editedTaskStatus, setEditedTaskStatus] = useState(options[0]);
    const [editedTaskDifficulty, setEditedTaskDifficulty] = useState("");
    const [editedTaskPoints, setEditedTaskPoints] = useState("");
    const [editedTaskFeedback, setEditedTaskFeedback] = useState("");

    const isEditingTask = editingTaskIndex !== null;

    const handleEditTask = (taskIndex: number) => {
        setEditingTaskIndex(taskIndex);
        const taskToEdit = projects[currentProjectIndex!].tasks![taskIndex];
        setEditedTaskName(taskToEdit.name);
        setStudentName(taskToEdit.studentName);
        setEditedTaskStatus(options.find(option => option.value === taskToEdit.status) || options[0]);
        setEditedTaskDifficulty(taskToEdit.difficulty);
        setEditedTaskPoints(taskToEdit.points);
        setEditedTaskFeedback(taskToEdit.feedback);
        openTaskModal();
    };

    const handleSaveEditedTask = () => {
        if (currentProjectIndex !== null && editingTaskIndex !== null) {
            const updatedProjects = [...projects];
            const project = updatedProjects[currentProjectIndex];
            project.tasks![editingTaskIndex] = {
                name: editedTaskName,
                studentName: studentName,
                status: editedTaskStatus.value,
                difficulty: editedTaskDifficulty,
                points: editedTaskPoints,
                feedback: editedTaskFeedback,
            };
            setProjects(updatedProjects);
            closeTaskModal();
        }
    };

    const openModal = () => setModalOpen(true);
    const closeModal = () => {
        setModalOpen(false);
        setCurrentProjectIndex(null);
        setProjectName("");
        setProjectDescription("");
        setSelectedOptions([]);
    };

    const openTaskModal = () => setTaskModalOpen(true);
    const closeTaskModal = () => {
        setTaskModalOpen(false);
        setTaskName("");
        setStudentName("");
        setTaskStatus(options[0]);
        setTaskDifficulty("");
        setTaskPoints("");
        setTaskFeedback("");
    };

    const openFeedbackModal = () => setFeedbackModalOpen(true);
    const closeFeedbackModal = () => setFeedbackModalOpen(false);

    const handleCreateProject = () => {
        const newProject: Project = {
            name: projectName,
            description: projectDescription,
            options: selectedOptions as { value: string; label: string }[],
        };

        setProjects([...projects, newProject]);
        closeModal();
    };

    const handleEditProject = () => {
        if (currentProjectIndex !== null) {
            const updatedProjects = [...projects];
            const editedProject = updatedProjects[currentProjectIndex];
            editedProject.name = projectName || editedProject.name;
            editedProject.description = projectDescription || editedProject.description;
            editedProject.options = selectedOptions.length ? (selectedOptions as { value: string; label: string }[]) : editedProject.options;
            setProjects(updatedProjects);
            closeModal();
        }
    };

    const handleDeleteProject = () => {
        if (currentProjectIndex !== null) {
            const updatedProjects = [...projects];
            updatedProjects.splice(currentProjectIndex, 1);
            setProjects(updatedProjects);
            closeModal();
        }
    };

    const handleDeleteTask = (taskIndex: number) => {
        if (currentProjectIndex !== null) {
            const updatedProjects = [...projects];
            const project = updatedProjects[currentProjectIndex];
            if (project.tasks && project.tasks.length > taskIndex) {
                project.tasks.splice(taskIndex, 1);
                setProjects(updatedProjects);
            }
        }
    };

    const handleEditButtonClick = (projectIndex: number) => {
        setCurrentProjectIndex(projectIndex);
        const projectToEdit = projects[projectIndex];
        setProjectName(projectToEdit.name);
        setProjectDescription(projectToEdit.description);
        setSelectedOptions(projectToEdit.options);
        openModal();
    };

    const handleAddTask = () => {
        if (currentProjectIndex !== null) {
            const updatedProjects = [...projects];
            const project = updatedProjects[currentProjectIndex];
            const newTask = {
                name: taskName,
                studentName: studentName,
                status: taskStatus.value,
                difficulty: taskDifficulty,
                points: taskPoints,
                feedback: taskFeedback,
            };
            project.tasks = project.tasks ? [...project.tasks, newTask] : [newTask];
            setProjects(updatedProjects);
            closeTaskModal();
        }
    };

    return (
        <div className="home">
            <Sidebar />
            <div className="dreapta">
                <h2 style={{ alignItems: "center" }}>Proiectele mele</h2>
                <div>
                    <button onClick={() => { setCurrentProjectIndex(null); openModal(); }}>Proiect nou</button>
                    <Modal
                        isOpen={isModalOpen}
                        onRequestClose={closeModal}
                        contentLabel="Proiect nou"
                        className="modal-content"
                    >
                        <div>
                            <h2>{currentProjectIndex !== null ? 'Modifică proiect' : 'Proiect nou'}</h2>
                            <label>Nume proiect:</label>
                            <input
                                type="text"
                                value={projectName}
                                onChange={(e) => setProjectName(e.target.value)}
                            />
                            <label>Descriere proiect:</label>
                            <textarea
                                value={projectDescription}
                                onChange={(e) => setProjectDescription(e.target.value)}
                            />
                            <label>Opțiuni:</label>
                            <div className="selectStudents">
                                <Select
                                    options={options}
                                    value={selectedOptions}
                                    onChange={(option) => setSelectedOptions(option as ValueType<{ value: string; label: string }>)}
                                    isMulti={true}
                                />
                            </div>
                            {currentProjectIndex === null ? (
                                <button onClick={handleCreateProject}>Creează proiect</button>
                            ) : (
                                <div>
                                    <button onClick={handleEditProject}>Modifica proiect</button>
                                    <Spacer size={1}></Spacer>
                                    <button onClick={handleDeleteProject}>Sterge proiect</button>
                                    <Spacer size={30}></Spacer>
                                </div>
                            )}
                        </div>
                    </Modal>
                </div>

                <div className="projects-container">
                    <h3>Proiecte create:</h3>
                    <ul>
                        {projects.map((project, projectIndex) => (
                            <li key={projectIndex}>
                                <p><strong>Tema Proiect:</strong> {project.name}</p>
                                <p><strong>Descriere Proiect:</strong> {project.description}</p>
                                <p><strong>Studenti:</strong> {project.options.map((opt) => opt.label).join(', ')}</p>
                                <div className="butoaneProiect">
                                    <button onClick={() => handleEditButtonClick(projectIndex)}>Editeaza proiect</button>
                                    <button onClick={() => {
                                        setCurrentProjectIndex(projectIndex);
                                        openTaskModal();
                                    }}>Adauga task</button>
                                    <button onClick={() => openFeedbackModal()}>Feedback</button>
                                </div>
                                <div className="styleTask">
                                    {project.tasks && project.tasks.map((task, taskIndex) => (
                                        <div key={taskIndex}>
                                            <p><strong>Task:</strong> {task.name}</p>
                                            <div className="numeStatus">
                                                <p><strong>Nume Student:</strong> {task.studentName}</p>
                                                <Spacer size={20}></Spacer>
                                                <p><strong>Status:</strong> {task.status}</p>
                                            </div>
                                            <div className="numeStatus">
                                                <p><strong>Nivel dificultate:</strong> {task.difficulty}</p>
                                                <Spacer size={20}></Spacer>
                                                <p><strong>Puncte:</strong> {task.points}</p>
                                            </div>
                                            <p><strong>Feedback:</strong> {task.feedback}</p>
                                            <div className="butoaneTask">
                                                <button onClick={() => handleEditTask(taskIndex)}>Editare task</button>
                                                <Spacer size={10}></Spacer>
                                                <button onClick={() => handleDeleteTask(taskIndex)}>Sterge task</button>
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            </li>
                        ))}
                    </ul>
                </div>

                <Modal
                    isOpen={isTaskModalOpen}
                    onRequestClose={closeTaskModal}
                    contentLabel="Adauga Task"
                    className="modal-content"
                >
                    <div className="butoaneModalTask">
                        <h2>{isEditingTask ? 'Modifica task' : 'Adauga Task'}</h2>
                        <label>Nume Task:</label>
                        <input
                            type="text"
                            value={isEditingTask ? editedTaskName : taskName}
                            onChange={(e) => (isEditingTask ? setEditedTaskName : setTaskName)(e.target.value)}
                        />
                        <label>Nume Student:</label>
                        <input
                            type="text"
                            value={studentName}
                            onChange={(e) => setStudentName(e.target.value)}
                        />
                        <label>Status Task:</label>
                        <Select
                            options={options}
                            value={isEditingTask ? editedTaskStatus : taskStatus}
                            onChange={(option) => (isEditingTask ? setEditedTaskStatus : setTaskStatus)(option as { value: string; label: string })}
                        />
                        <label>Nivel dificultate:</label>
                        <input
                            type="text"
                            value={isEditingTask ? editedTaskDifficulty : taskDifficulty}
                            onChange={(e) => (isEditingTask ? setEditedTaskDifficulty : setTaskDifficulty)(e.target.value)}
                        />
                        <label>Puncte:</label>
                        <input
                            type="text"
                            value={isEditingTask ? editedTaskPoints : taskPoints}
                            onChange={(e) => (isEditingTask ? setEditedTaskPoints : setTaskPoints)(e.target.value)}
                        />
                        <label>Feedback:</label>
                        <textarea
                            value={isEditingTask ? editedTaskFeedback : taskFeedback}
                            onChange={(e) => (isEditingTask ? setEditedTaskFeedback : setTaskFeedback)(e.target.value)}
                        />
                        {isEditingTask ? (
                            <div>
                                <button onClick={handleSaveEditedTask}>Salveaza</button>
                                <Spacer size={1}></Spacer>
                            </div>
                        ) : (
                            <button onClick={handleAddTask}>Adauga Task</button>
                        )}
                        <button onClick={closeTaskModal}>Inchide</button>
                    </div>
                </Modal>

                <Modal
                    isOpen={isFeedbackModalOpen}
                    onRequestClose={closeFeedbackModal}
                    contentLabel="Feedback proiect"
                    className="modal-content"
                >
                    <div>
                        <h2>Feedback proiect</h2>
                        <label>Feedback:</label>
                        <textarea
                            value={feedbackText}
                            onChange={(e) => setFeedbackText(e.target.value)}
                        />
                        <div>
                            <button onClick={() => {
                                console.log(feedbackText);
                                setFeedbackText("");
                                closeFeedbackModal();
                            }}>Trimite feedback
                            </button>
                            <Spacer size={1}></Spacer>
                            <button onClick={closeFeedbackModal}>Inchide</button>
                        </div>
                    </div>
                </Modal>
            </div>
        </div>
    );

};

export default HomeScreen;
