import { cn } from "@/lib/utils";
import { HTMLAttributes } from "react";
import { Button, ButtonProps } from "./ui/button";
import { useSidebarContext } from "@/context/SidebarProvider";
import React from "react";

interface SidebarProps extends HTMLAttributes<HTMLDivElement> {
  children?: React.ReactNode;
}

export function Sidebar({ children, className, ...other }: SidebarProps) {
  return (
    <div
      className={cn("flex pointer-events-none h-screen", className)}
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
        "flex flex-col pointer-events-auto items-center gap-y-2.5 py-2.5 bg-white w-16 shadow-md border-r border-border dark:bg-gray-950",
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

export function SidebarContainer({ children }: { children?: React.ReactNode }) {
  const { key } = useSidebarContext();

  return (
    <div
      className={cn("w-96 flex flex-col h-screen", {
        "bg-white shadow-md pointer-events-auto dark:bg-gray-900": key,
      })}
    >
      {children}
    </div>
  );
}

interface SidebarContentProps {
  value: string;
  children?: React.ReactNode;
}

export function SidebarContent({ value, children }: SidebarContentProps) {
  const { key } = useSidebarContext();

  if (key !== value) return null;

  return children;
}

export function SidebarHeader({ children }: { children: React.ReactNode }) {
  return <div className="pointer-events-auto px-5 py-2.5">{children}</div>;
}
