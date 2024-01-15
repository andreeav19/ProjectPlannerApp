import { Auth } from "../components/Auth/Auth";
import { Logo } from "../components/Logo";
import { Card, Center, Container , Stack} from "@mantine/core";

export function LoginPage() {
  return (
    <Center m="lg">
        <Stack
      bg="var(--mantine-color-body)"
      align="center"
      justify="center"
      gap="lg"
    >
          <Logo size={256}/>
          <Auth />
        </Stack>
    </Center>
  );
}
