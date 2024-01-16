"use client";

import { IconChevronUp, IconSelector } from "@tabler/icons-react";
import { DataTable, DataTableSortStatus } from "mantine-datatable";
import sortBy from "lodash/sortBy";
import { useEffect, useRef, useState } from "react";
import { IconSearch, IconX } from "@tabler/icons-react";
import {
  ActionIcon,
  Button,
  Checkbox,
  MultiSelect,
  Stack,
  TextInput,
  Text,
  Group,
} from "@mantine/core";
import { useDebouncedValue } from "@mantine/hooks";
import { getDecodedJWT } from "./AuthContext";
import axios from "axios";

interface User {
  userName: string;
  firstName: string;
  lastName: string;
  points: number;
}

export function Leaderboard() {
  const [sortStatus, setSortStatus] = useState<DataTableSortStatus<User>>({
    columnAccessor: "name",
    direction: "asc",
  });
  const [records, setRecords] = useState([]);
  const [query, setQuery] = useState("");
  const [debouncedQuery] = useDebouncedValue(query, 200);
  const didMount = useRef(false);
  useEffect(() => {
    if (!didMount.current) {
      axios
        .get("/api/Users/leaderboard", {
          headers: {
            Authorization: `Bearer ${getDecodedJWT().jwt}`,
          },
        })
        .then((response) => {
          const inc = sortBy(response.data.response, "points");
          setRecords(inc);
          didMount.current = true;
        })
        .catch((error) => {
          console.error("Error fetching projects:", error);
        });
    }
    let data = [...records];
    data = sortBy(data, sortStatus.columnAccessor) as User[];
    setRecords(sortStatus.direction === "desc" ? data.reverse() : data);
    data = records.filter((user) => {
      if (
        debouncedQuery !== "" &&
        !`${user.userName}`
          .toLowerCase()
          .includes(debouncedQuery.trim().toLowerCase())
      )
        return false;

      return true;
    });
  }, [debouncedQuery, sortStatus]);

  return (
    <DataTable
      striped
      highlightOnHover
      records={records}
      columns={[
        {
          accessor: "userName",
          width: "30%",
          sortable: true,
          filter: (
            <TextInput
              label="Students"
              description="Show students by their username"
              placeholder="Search students..."
              leftSection={<IconSearch size={16} />}
              rightSection={
                <ActionIcon
                  size="sm"
                  variant="transparent"
                  c="dimmed"
                  onClick={() => setQuery("")}
                >
                  <IconX size={14} />
                </ActionIcon>
              }
              value={query}
              onChange={(e) => setQuery(e.currentTarget.value)}
            />
          ),
          filtering: query !== "",
        },
        { accessor: "firstName", width: "20%" },
        { accessor: "lastName", width: "20%" },
        { accessor: "points", width: "30%", sortable: true },
      ]}
      idAccessor="userName"
      sortStatus={sortStatus}
      onSortStatusChange={setSortStatus}
      sortIcons={{
        sorted: <IconChevronUp size={14} />,
        unsorted: <IconSelector size={14} />,
      }}
      rowExpansion={{
        content: ({ record }) => (
          <Stack p="xs" gap={6} bg="theme.8">
            <Group gap={6}>
              <Text fz="sm">Buna mama</Text>
            </Group>
          </Stack>
        ),
      }}
    />
  );
}
