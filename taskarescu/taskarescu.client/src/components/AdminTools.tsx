import { ActionIcon, Box, Group, Select, TextInput } from "@mantine/core";
import { IconEdit, IconEye, IconTrash } from "@tabler/icons-react";
import { DataTable } from "mantine-datatable";
import { useDisclosure } from "@mantine/hooks";
import { Modal, Button, Checkbox, CheckboxGroup } from "@mantine/core";
import { useEffect, useState } from "react";
import axios from "axios";
import { getDecodedJWT } from "./AuthContext";

const CustomModal = ({
  onClose,
  user
}) => {
  const [selectedRole, setSelectedRole] = useState(user.roleName);
  const [error, setError] = useState(null);

  const handleRoleChange = (value) => {
    setSelectedRole(value);
  }

  const handleSaveChanges = () => {
    axios
      .put(`/api/Users/${user.userId}/role`, selectedRole, {
        headers: {
          Authorization: `Bearer ${getDecodedJWT().jwt}`,
          "Content-Type": "application/json"
        }
      })
      .then((response) =>{
        // console.log(response);
        onClose();
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

  return (
    <Modal
      opened
      onClose={() => {
        setError(null);
        onClose();
      }}
      title={`View user: ${user.firstName + " " + user.lastName}`}
      overlayProps={{
        backgroundOpacity: 0.55,
        blur: 3,
      }}
    >
      {/* Add your custom modal content here */}
      <p>User Id: {user.userId}</p>
      <p>Username: {user.userName}</p>
      <p>Email: {user.email}</p>
      <p>
        <Group wrap="nowrap">
          Role: <Select
          data={["Student", "Prof", "Admin"]}
          value={selectedRole}
          allowDeselect={false}
          onChange={handleRoleChange}
          />
        </Group>
      </p>
      
      {error && <p style={{ color: "red" }}>{error}</p>}

      <Group justify="center" wrap="nowrap">
        <Button fullWidth color="blue" onClick={handleSaveChanges}>
          Save
        </Button>
        <Button fullWidth onClick={onClose}>
          Close
        </Button>
      </Group> 
    </Modal>
  );
};

export function AdminTools() {
  const [modalOpen, setModalOpen] = useState(false);
  const [modalParams, setModalParams] = useState({});
  const [records, setRecords] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedRoles, setSelectedRoles] = useState(['Student', 'Prof', 'Admin']);

  const showModal = (params) => {
    setModalParams(params);
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
  };

  const filteredRecords = records.filter((user) => {
    const includesSearchTerm =
      Object.values(user).some(
        (value) =>
          value &&
          value.toString().toLowerCase().includes(searchTerm.toLowerCase())
      );
  
    const includesSelectedRoles =
    selectedRoles.length === 0 || selectedRoles.some(role => user.roleName.toLowerCase().includes(role.toLowerCase()));
  
  
    return includesSearchTerm && includesSelectedRoles;
  });

  const toggleRoleFilter = (role) => {
    setSelectedRoles((prevRoles) => {
      if (prevRoles.includes(role)) {
        return prevRoles.filter((r) => r !== role);
      } else {
        return [...prevRoles, role];
      }
    });
  };

  useEffect(() => {
    axios
      .get("/api/Users", {
        headers: {
          Authorization: `Bearer ${getDecodedJWT().jwt}`,
        },
      })
      .then((response) => {
        console.log(response.data.response);
        setRecords(response.data.response);
      })
      .catch((error) => {
        console.error("Error fetching projects:", error);
      });
  }, [modalOpen]);

  

  return (
    <div>
      <Group>

      <TextInput 
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        placeholder="Search..."
        style={{ width: '25%' }}/>
      <Checkbox.Group
        defaultValue={['Student', 'Prof', 'Admin']}
        withAsterisk
      >
        <Group mt="xs">
          <Checkbox color="cyan" value="Student" label="Student" checked={selectedRoles.includes('Student')}
              onChange={() => toggleRoleFilter('Student')} />
          <Checkbox color="cyan" value="Prof" label="Prof" checked={selectedRoles.includes('Prof')}
              onChange={() => toggleRoleFilter('Prof')}/>
          <Checkbox color="cyan" value="Admin" label="Admin" checked={selectedRoles.includes('Admin')}
              onChange={() => toggleRoleFilter('Admin')}/>
        </Group>
      </Checkbox.Group>
      </Group>
      <br />
      <DataTable
        withTableBorder
        records={filteredRecords}
        columns={[
          { accessor: "userId" },
          { accessor: "userName" },
          { accessor: "email" },
          { accessor: "firstName" },
          { accessor: "lastName" },
          { accessor: "roleName" },

          {
            accessor: "actions",
            title: <Box mr={6}>Edit role</Box>,
            textAlign: "center",
            render: (user) => (

              <Group gap={2} justify="center" wrap="nowrap">
                {
                  user.userId == getDecodedJWT().nameIdentifier ? (<Box mr={6}/>) : 
                  (
                    <ActionIcon
                      size="sm"
                      variant="subtle"
                      color="blue"
                      onClick={() => showModal({ user })}
                    >
                      <IconEdit size={16} />
                    </ActionIcon>
                  )
                }
              </Group>
            ),
          },
        ]}
      />

      {modalOpen && <CustomModal onClose={closeModal} {...modalParams} />}
    </div>
  );
}
