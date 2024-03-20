import ReactDOM from "react-dom/client";
import QueryProvider from "@/context/QueryProvider";
import "./index.css";
import "./i18n.js";
import Provider from "@/context/StationContext";
import SessionProvider from "@/context/SessionProvider";
import RouterProvider from "@/context/RouterProvider";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <Provider>
    <QueryProvider>
      <SessionProvider>
        <RouterProvider />
      </SessionProvider>
    </QueryProvider>
  </Provider>
);
