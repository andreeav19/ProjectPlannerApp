import { MantineProvider, createTheme, MantineColorsTuple } from '@mantine/core';

const pink: MantineColorsTuple = [
  "#ffe8fb",
  "#ffcfef",
  "#ff9ddb",
  "#fc67c6",
  "#fa3ab4",
  "#fa1da9",
  "#fa08a4",
  "#e0008f",
  "#c80080",
  "#af006f"
];

const blue: MantineColorsTuple = [
  '#dffcff',
  '#cff2fd',
  '#a5e3f3',
  '#76d2eb',
  '#51c5e4',
  '#37bce0',
  '#23b8df',
  '#04a2c6',
  '#0090b2',
  '#007d9e'
];
export const theme = createTheme({
  colors: {
    'mypink': pink,
    "theme": blue
  }
});
