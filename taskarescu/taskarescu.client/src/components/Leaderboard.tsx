"use client";

import { IconChevronUp, IconSelector } from "@tabler/icons-react";
import { DataTable, DataTableSortStatus } from "mantine-datatable";
import sortBy from "lodash/sortBy";
import { useEffect, useState } from "react";
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

interface User {
  name: string;
  company: string;
  email: string;
}

const users = [
  {
    name: "Athena Weissnat",
    company: "Little - Rippin",
    email: "Elouise.Prohaska@yahoo.com",
  },
  {
    name: "Deangelo Runolfsson",
    company: "Greenfelder - Krajcik",
    email: "Kadin_Trantow87@yahoo.com",
  },
  {
    name: "Danny Carter",
    company: "Kohler and Sons",
    email: "Marina3@hotmail.com",
  },
  {
    name: "Trace Tremblay PhD",
    company: "Crona, Aufderhar and Senger",
    email: "Antonina.Pouros@yahoo.com",
  },
  {
    name: "Derek Dibbert",
    company: "Gottlieb LLC",
    email: "Abagail29@hotmail.com",
  },
  {
    name: "Viola Bernhard",
    company: "Funk, Rohan and Kreiger",
    email: "Jamie23@hotmail.com",
  },
  {
    name: "Austin Jacobi",
    company: "Botsford - Corwin",
    email: "Genesis42@yahoo.com",
  },
  {
    name: "Hershel Mosciski",
    company: "Okuneva, Farrell and Kilback",
    email: "Idella.Stehr28@yahoo.com",
  },
  {
    name: "Mylene Ebert",
    company: "Kirlin and Sons",
    email: "Hildegard17@hotmail.com",
  },
  {
    name: "Lou Trantow",
    company: "Parisian - Lemke",
    email: "Hillard.Barrows1@hotmail.com",
  },
  {
    name: "Dariana Weimann",
    company: "Schowalter - Donnelly",
    email: "Colleen80@gmail.com",
  },
  {
    name: "Dr. Christy Herman",
    company: "VonRueden - Labadie",
    email: "Lilyan98@gmail.com",
  },
  {
    name: "Katelin Schuster",
    company: "Jacobson - Smitham",
    email: "Erich_Brekke76@gmail.com",
  },
  {
    name: "Melyna Macejkovic",
    company: "Schuster LLC",
    email: "Kylee4@yahoo.com",
  },
  {
    name: "Pinkie Rice",
    company: "Wolf, Trantow and Zulauf",
    email: "Fiona.Kutch@hotmail.com",
  },
  {
    name: "Brain Kreiger",
    company: "Lueilwitz Group",
    email: "Rico98@hotmail.com",
  },
];

const initialRecords = sortBy(users, "name");

export function Leaderboard() {
  const [sortStatus, setSortStatus] = useState<DataTableSortStatus<User>>({
    columnAccessor: "name",
    direction: "asc",
  });
  const [records, setRecords] = useState(initialRecords);
  const [query, setQuery] = useState("");
  const [debouncedQuery] = useDebouncedValue(query, 200);

  useEffect(() => {
    console.log(getDecodedJWT());

    let data = initialRecords.filter((user) => {
      if (
        debouncedQuery !== "" &&
        !`${user.name}`
          .toLowerCase()
          .includes(debouncedQuery.trim().toLowerCase())
      )
        return false;

      return true;
    });
    data = sortBy(data, sortStatus.columnAccessor) as User[];
    setRecords(sortStatus.direction === "desc" ? data.reverse() : data);
  }, [debouncedQuery, sortStatus]);

  return (
    <DataTable
      striped
      highlightOnHover
      records={records}
      columns={[
        {
          accessor: "name",
          width: "40%",
          sortable: true,
          filter: (
            <TextInput
              label="Employees"
              description="Show employees whose names include the specified text"
              placeholder="Search employees..."
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
        { accessor: "company", width: "30%" },
        { accessor: "email", width: "30%", sortable: true },
      ]}
      idAccessor="name"
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
