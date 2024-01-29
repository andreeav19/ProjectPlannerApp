import { ActionIcon, Box, Group, Select } from "@mantine/core";
import { IconEdit, IconEye, IconTrash } from "@tabler/icons-react";
import { DataTable } from "mantine-datatable";
import { useDisclosure } from "@mantine/hooks";
import { Modal, Button } from "@mantine/core";
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

  const showModal = (params) => {
    setModalParams(params);
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
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
      <DataTable
        withTableBorder
        records={records}
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
