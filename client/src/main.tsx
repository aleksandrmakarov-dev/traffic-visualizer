import ReactDOM from "react-dom/client";
import QueryProvider from "@/context/QueryProvider";
import "./index.css";
import "./i18n.js";
import Provider from "@/context/StationContext";
import SessionProvider from "@/context/SessionProvider";
import RouterProvider from "@/context/RouterProvider";
import { TooltipProvider } from "./shared/components/ui/tooltip.js";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <QueryProvider>
    <SessionProvider>
      <TooltipProvider>
        <Provider>
          <RouterProvider />
        </Provider>
      </TooltipProvider>
    </SessionProvider>
  </QueryProvider>
);
