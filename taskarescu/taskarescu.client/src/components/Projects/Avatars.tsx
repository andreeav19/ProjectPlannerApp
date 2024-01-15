import { Avatar, Group, Text, Tooltip } from "@mantine/core";

const createAvatarComponent = (name) => (
  <Tooltip label={name} withArrow key={name}>
    <Avatar variant="light" radius="xl" size={32}>
      {name.substring(0, Math.min(2, name.length))}
    </Avatar>
  </Tooltip>
);

const createTooltipComponent = (students) => (
  <Tooltip
    withArrow
    label={students.map((student) => (
      <div key={student.name}>{student.name}</div>
    ))}
  >
    <Avatar
      variant="light"
      radius="xl"
      size={24}
    >{`+${students.length}`}</Avatar>
  </Tooltip>
);

const data = [
  { name: "Salazar Troop", avatar: "https://thispersondoesnotexist.com/" },
  { name: "Bandit Crimes", avatar: "https://thispersondoesnotexist.com/" },
  { name: "Jane Rata", avatar: "https://thispersondoesnotexist.com/" },
  { name: "John Outcast", avatar: "https://thispersondoesnotexist.com/" },
  { name: "mah guyy", avatar: "https://thispersondoesnotexist.com/" },
  { name: "h", avatar: "https://thispersondoesnotexist.com/" },
];

export const Avatars = ({ teacher }: { teacher: string }) => {
  const students = () => {
    const renderedStudents = data
      .slice(0, 3)
      .map((student) => createAvatarComponent(student.name));

    if (data.length > 3) {
      const remainingStudents = data.slice(3);
      renderedStudents.push(createTooltipComponent(remainingStudents));
    }

    return renderedStudents;
  };

  return (
    <Group justify="space-between" gap="xl">
      <Group justify="flex-start" gap="xs">
        <Avatar variant="light" size={48} radius="xl" mr="xs" color="theme.4">
          {teacher.substring(0, Math.min(2, teacher.length))}
        </Avatar>
        <Text fz="sm" inline>
          {teacher}
        </Text>
      </Group>

      <Group justify="flex-end">
        <Tooltip.Group openDelay={300} closeDelay={100}>
          <Avatar.Group spacing="xs">{students()}</Avatar.Group>
        </Tooltip.Group>
      </Group>
    </Group>
  );
};
