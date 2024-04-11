import { cn } from "@/lib/utils";
import { HTMLAttributes } from "react";
import { Button, ButtonProps } from "./ui/button";
import { useSidebarContext } from "@/context/SidebarProvider";
import React from "react";

interface SidebarProps extends HTMLAttributes<HTMLDivElement> {
  children?: React.ReactNode;
}

export function Sidebar({ children, className, ...other }: SidebarProps) {
  const { key } = useSidebarContext();

  return (
    <div
      className={cn(
        "max-w-md grid grid-cols-[auto_1fr]",
        { "w-full": key },
        className
      )}
      {...other}
    >
      {children}
    </div>
  );
}

interface SidebarNavigationProps extends HTMLAttributes<HTMLDivElement> {
  children?: React.ReactNode;
}

export function SidebarNavigation({
  children,
  className,
  ...other
}: SidebarNavigationProps) {
  return (
    <div
      className={cn(
        "flex flex-col items-center gap-y-2.5 py-2.5 bg-white h-screen w-16 shadow-md border-r border-border dark:bg-gray-950",
        className
      )}
      {...other}
    >
      {children}
    </div>
  );
}

interface SidebarNavigationItemProps extends ButtonProps {
  value: string;
  children?: React.ReactNode;
}

export function SidebarNavigationItem({
  value,
  children,
  ...other
}: SidebarNavigationItemProps) {
  const { setKey } = useSidebarContext();

  const onClick = () => {
    // set current value to context
    setKey(value);
  };

  return (
    <Button size="icon" variant="secondary" onClick={onClick} {...other}>
      {children}
    </Button>
  );
}

interface SidebarContentProps extends HTMLAttributes<HTMLDivElement> {
  value: string;
  children?: React.ReactNode;
}

export function SidebarContent({
  value,
  children,
  className,
  ...other
}: SidebarContentProps) {
  const { key } = useSidebarContext();

  if (key !== value) return null;

  return (
    <div
      className={cn("bg-white h-screen flex shadow-md", className)}
      {...other}
    >
      {children}
    </div>
  );
}
