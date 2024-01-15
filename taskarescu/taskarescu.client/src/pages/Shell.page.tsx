import { ColorSchemeToggle } from "../components/ColorSchemeToggle";
import { useDisclosure } from "@mantine/hooks";
import {
  AppShell,
  Burger,
  Divider,
  Flex,
  Group,
  Text,
  Code,
} from "@mantine/core";
import { Logo } from "../components/Logo";
import { Navbar } from "../components/Navbar/Navbar";
import { RouterSwitcher } from "../components/RouterSwitcher";
import { Footer } from "../components/Footer/Footer";
export function Shell() {
  const [opened, { toggle }] = useDisclosure();

  return (
    <AppShell
      layout="alt"
      header={{ height: 80 }}
      footer={{ height: 60 }}
      navbar={{ width: 200, breakpoint: "sm", collapsed: { mobile: !opened } }}
      aside={{
        width: 10,
        breakpoint: "md",
        collapsed: { desktop: false, mobile: true },
      }}
      padding="xl"
    >
      <AppShell.Header>
        <Group h="100%" px="md" justify="space-between">
          <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
          <div></div>
          <Logo size={64} />
          <ColorSchemeToggle />
        </Group>
      </AppShell.Header>

      <AppShell.Navbar p="md">
        <Group justify="center">
          <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
          <Text size="xl">Tăskărescu</Text>
          <Code fw={700}>{new Date().toLocaleDateString("en-GB")}</Code>
        </Group>
        <Navbar />
      </AppShell.Navbar>

      <AppShell.Main>
        <RouterSwitcher />
      </AppShell.Main>

      <AppShell.Footer p="md">
        <Footer />
      </AppShell.Footer>
    </AppShell>
  );
}
