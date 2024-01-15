import {
  MantineProvider,
  createTheme,
  MantineColorsTuple,
} from "@mantine/core";

const begginer: MantineColorsTuple = [
  "#f9fdec",
  "#f1fad9",
  "#e2f6ac",
  "#d1f17b",
  "#c4ed54",
  "#bceb3e",
  "#b6ea32",
  "#a0cf26",
  "#8db81e",
  "#789f0e",
];

const intermediate: MantineColorsTuple = [
  "#ebfaff",
  "#d7f3fb",
  "#a9e6f9",
  "#7bdaf8",
  "#5dcff6",
  "#4fc8f6",
  "#46c5f7",
  "#39aedc",
  "#2a9ac5",
  "#0086ad",
];
const blue: MantineColorsTuple = [
  "#dffcff",
  "#cff2fd",
  "#a5e3f3",
  "#76d2eb",
  "#51c5e4",
  "#37bce0",
  "#23b8df",
  "#04a2c6",
  "#0090b2",
  "#007d9e",
];

const master: MantineColorsTuple = [
  "#ffe8f4",
  "#ffcfe0",
  "#fd9ebe",
  "#fa6999",
  "#f83e7b",
  "#f72267",
  "#f7115d",
  "#dd014e",
  "#c60044",
  "#ae0039",
];

export const theme = createTheme({
  colors: {
    theme: blue,
    begginer: begginer,
    intermediate: intermediate,
    master: master,
  },
});
