import { ProjectCard } from "./ProjectCard";
import { SimpleGrid } from "@mantine/core";
import { useEffect, useState } from "react";
import { getDecodedJWT } from "../AuthContext";
import axios from "axios";
export function Projects() {
  const [projects, setProjects] = useState([]);

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
        console.log(projects);
      })
      .catch((error) => {
        console.error("Error fetching projects:", error);
      });
  }, []);

  return (
    <SimpleGrid cols={3} spacing="lg" verticalSpacing="sm" mx="xl">
      {projects.map((item) => (
        <ProjectCard
          key={item.id}
          title={item.name}
          description={item.description}
          id={item.id}
          createdBy={item.userId}
        />
      ))}
    </SimpleGrid>
  );
}
