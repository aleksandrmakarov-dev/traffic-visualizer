import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function mapValueToColor(value: number) {
  const min = -1;
  const max = 100;

  // Clamp traffic percentage between 0 and 100
  const clampedPercentage = Math.min(Math.max(value, min), max);

  let color: string;

  if (value == -1) {
    return "#95a5a6";
  }

  if (clampedPercentage >= 90) {
    // dark green
    color = "#16a085";
  } else if (clampedPercentage >= 80) {
    // green
    color = "#2ecc71";
  } else if (clampedPercentage >= 70) {
    // yellow
    color = "#f1c40f";
  } else if (clampedPercentage >= 60) {
    // orange
    color = "#f39c12";
  } else if (clampedPercentage >= 50) {
    // dark orange
    color = "#d35400";
  } else {
    // red
    color = "#c0392b";
  }

  return color;
}

export function capitalize(value: string) {
  return value.replace(/\w\S*/g, function (txt) {
    return txt.charAt(0).toUpperCase() + txt.slice(1).toLowerCase();
  });
}
