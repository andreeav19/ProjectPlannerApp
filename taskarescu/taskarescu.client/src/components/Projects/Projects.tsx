import { ProjectCard } from "./ProjectCard";
import { SimpleGrid } from "@mantine/core";
export function Projects() {
  let data = [1, 2, 3, 4, 5];

  return (
    <SimpleGrid cols={3} spacing="lg" verticalSpacing="sm" mx="xl">
      {data.map((item) => (
        <ProjectCard />
      ))}
    </SimpleGrid>
  );
}
