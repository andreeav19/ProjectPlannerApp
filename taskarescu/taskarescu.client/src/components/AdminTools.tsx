import { ActionIcon, Box, Group } from "@mantine/core";
import { IconEdit, IconEye, IconTrash } from "@tabler/icons-react";
import { DataTable } from "mantine-datatable";
import { useDisclosure } from "@mantine/hooks";
import { Modal, Button } from "@mantine/core";
import { useEffect, useState } from "react";
import axios from "axios";
import { getDecodedJWT } from "./AuthContext";

const CustomModal = ({
  onClose,
  company,
  action,
  customParam1,
  customParam2,
}) => {
  return (
    <Modal
      opened
      onClose={onClose}
      title={`View ${company}`}
      overlayProps={{
        backgroundOpacity: 0.55,
        blur: 3,
      }}
    >
      {/* Add your custom modal content here */}
      <p>Action: {action}</p>
      <p>Custom Parameter 1: {customParam1}</p>
      <p>Custom Parameter 2: {customParam2}</p>

      <Button fullWidth onClick={onClose}>
        Close
      </Button>
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
            title: <Box mr={6}>Row actions</Box>,
            textAlign: "right",
            render: (company) => (
              <Group gap={2} justify="right" wrap="nowrap">
                <ActionIcon
                  size="sm"
                  variant="subtle"
                  color="blue"
                  onClick={() => showModal({ company, action: "edit" })}
                >
                  <IconEdit size={16} />
                </ActionIcon>
                <ActionIcon
                  size="sm"
                  variant="subtle"
                  color="red"
                  onClick={() => showModal({ company, action: "delete" })}
                >
                  <IconTrash size={16} />
                </ActionIcon>
              </Group>
            ),
          },
        ]}
      />

      {modalOpen && <CustomModal onClose={closeModal} {...modalParams} />}
    </div>
  );
}
