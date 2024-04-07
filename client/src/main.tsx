import ReactDOM from "react-dom/client";
import QueryProvider from "@/context/QueryProvider";
import "./index.css";
import "./i18n.js";
import SessionProvider from "@/context/SessionProvider";
import RouterProvider from "@/context/RouterProvider";
import ThemeProvider from "@/context/ThemeProvider";
import { Toaster } from "sonner";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <ThemeProvider>
    <QueryProvider>
      <SessionProvider>
        <RouterProvider />
        <Toaster />
      </SessionProvider>
    </QueryProvider>
  </ThemeProvider>
);
