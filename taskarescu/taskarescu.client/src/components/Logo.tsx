import { Image } from '@mantine/core';

interface LogoProps {
  size: number; 
}

export function Logo({size}: LogoProps) {
  return (
    <Image
      radius="md"
      h="auto"
      w={size}
      fit="contain"
      src="/logo.png"
    />
  );
}
