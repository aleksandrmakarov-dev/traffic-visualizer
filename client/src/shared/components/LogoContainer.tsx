import { cn } from "@/lib/utils";
import { HTMLAttributes } from "react";

interface LogoContainerProps extends HTMLAttributes<HTMLDivElement> {}

export function LogoContainer({ className, ...other }: LogoContainerProps) {
  return (
    <div className={cn("pointer-events-none", className)} {...other}>
      <img
        className="h-10"
        src="./images/techtitans_logo.png"
        alt="TechTitans"
      />
    </div>
  );
}
