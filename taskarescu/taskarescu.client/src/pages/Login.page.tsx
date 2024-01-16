import { Logo } from "../components/Logo";
import { useToggle, upperFirst } from "@mantine/hooks";
import { useForm } from "@mantine/form";
import { ColorSchemeToggle } from "../components/ColorSchemeToggle";
import {
  Center,
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  PaperProps,
  Button,
  Divider,
  Checkbox,
  Anchor,
  Stack,
} from "@mantine/core";
import Cookies from "js-cookie";
import axios from "axios";
import { AuthProvider, AuthContext } from "../components/AuthContext";
import { useContext } from "react";
import { useNavigate } from "react-router-dom";

export function Auth(props: PaperProps) {
  const navigate = useNavigate();
  const { authenticated, setAuthicated } = useContext(AuthContext);
  const [type, toggle] = useToggle(["login", "register"]);

  const form = useForm({
    initialValues: {
      firstName: "",
      lastName: "",
      email: "",
      username: "",
      password: "",
    },

    validate: {
      email: (val) => (/^\S+@\S+$/.test(val) ? null : "Invalid email"),
      password: (val) => {
        if (val.length < 6) {
          return "Password should include at least 6 characters";
        }

        // Check for at least one uppercase letter
        if (!/[A-Z]/.test(val)) {
          return "Password should include at least one uppercase letter";
        }

        // Check for at least one lowercase letter
        if (!/[a-z]/.test(val)) {
          return "Password should include at least one lowercase letter";
        }

        // Check for at least one digit
        if (!/\d/.test(val)) {
          return "Password should include at least one number";
        }

        // Check for at least one special character
        if (!/[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]/.test(val)) {
          return "Password should include at least one special character";
        }

        return null;
      },
      username: (val) =>
        val.length >= 5
          ? null
          : "Username should include at least 5 characters",
      firstName: (val) =>
        val.length >= 3
          ? null
          : "First name should include at least 3 characters",
      lastName: (val) =>
        val.length >= 3
          ? null
          : "Last name should include at least 3 characters",
    },
  });

  const handleButton = async (event) => {
    const errors = form.validate();

    if (type == "register") {
      if (errors.hasErrors) {
        console.log("Register form validation failed:", errors);
        return;
      }
    } else if (type == "login") {
      if (errors.errors.username || errors.errors.password) {
        console.log("Login form validation failed:", errors);
        return;
      }
    }
    switch (type.toLowerCase()) {
      case "login":
        try {
          const response = await loginRequest(
            form.values.username,
            form.values.password
          );
          handleSuccess(response);
        } catch (error) {
          console.error("Login failed:", error);
          alert("Eroare la logare :(");
        }
        break;

      case "register":
        try {
          const registerResponse = await registerRequest(form.values);
          handleSuccess(registerResponse);
        } catch (error) {
          console.error("Registration failed:", error);
          alert("ba frate care ai oprit serveru");
        }
        break;

      default:
        alert("Ceva a mers oribil de rau va rog nu ne picati");
    }
  };

  const loginRequest = async (username: string, password: string) => {
    const url = "/api/Auth/login";

    try {
      const response = await axios.post(url, {
        username,
        password,
      });

      return response.data;
    } catch (error) {
      throw new Error("Login failed");
    }
  };

  const registerRequest = async (userData: {
    firstName: string;
    lastName: string;
    email: string;
    username: string;
    password: string;
  }) => {
    const url = "/api/Auth/register";

    try {
      const response = await axios.post(url, userData);

      return response.data;
    } catch (error) {
      throw new Error("Registration failed");
    }
  };

  const handleSuccess = (response: { value: any }) => {
    const jwtToken = response.value;

    Cookies.set("jwtToken", jwtToken, { path: "/" });
    console.log("Login successful!");
    setAuthicated(true);
    navigate("/projects");
  };

  return (
    <Paper radius="md" p="xl" withBorder {...props}>
      <Group>
        <Text size="lg" fw={500}>
          Welcome to Tăskărescu!
        </Text>
        <ColorSchemeToggle />
      </Group>
      <Divider labelPosition="center" my="lg" />

      <form onSubmit={form.onSubmit(() => {})}>
        <Stack>
          {type === "register" && (
            <>
              <TextInput
                required
                label="Email"
                placeholder="psoviany@daw.unibuc"
                value={form.values.email}
                onChange={(event) =>
                  form.setFieldValue("email", event.currentTarget.value)
                }
                error={form.errors.email && "Invalid email"}
                radius="md"
              />

              <TextInput
                required
                label="First name"
                placeholder="Petru"
                value={form.values.firstName}
                onChange={(event) =>
                  form.setFieldValue("firstName", event.currentTarget.value)
                }
                error={form.errors.firstName}
                radius="md"
              />

              <TextInput
                required
                label="Last name"
                placeholder="Soviany"
                value={form.values.lastName}
                onChange={(event) =>
                  form.setFieldValue("lastName", event.currentTarget.value)
                }
                error={form.errors.lastName}
                radius="md"
              />
            </>
          )}

          <TextInput
            required
            label="Username"
            placeholder="petruS"
            value={form.values.username}
            onChange={(event) =>
              form.setFieldValue("username", event.currentTarget.value)
            }
            error={form.errors.username}
            radius="md"
          />
          <PasswordInput
            required
            label="Password"
            placeholder="Your password"
            value={form.values.password}
            onChange={(event) =>
              form.setFieldValue("password", event.currentTarget.value)
            }
            error={form.errors.password}
            radius="md"
          />

          {type === "register"}
        </Stack>

        <Group justify="space-between" mt="xl">
          <Anchor
            component="button"
            type="button"
            c="dimmed"
            onClick={() => toggle()}
            size="xs"
          >
            {type === "register"
              ? "Already have an account? Login"
              : "Don't have an account? Register"}
          </Anchor>
          <Button
            type="submit"
            radius="xl"
            color="theme"
            onClick={(event) => handleButton(event)}
          >
            {upperFirst(type)}
          </Button>
        </Group>
      </form>
    </Paper>
  );
}

export function LoginPage() {
  return (
    <Center m="lg">
      <Stack
        bg="var(--mantine-color-body)"
        align="center"
        justify="center"
        gap="lg"
      >
        <Logo size={256} />
        <Auth />
      </Stack>
    </Center>
  );
}
