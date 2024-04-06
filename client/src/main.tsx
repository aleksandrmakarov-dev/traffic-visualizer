import ReactDOM from "react-dom/client";
import QueryProvider from "@/context/QueryProvider";
import "./index.css";
import "./i18n.js";
import StationProvider from "@/context/StationContext";
import SessionProvider from "@/context/SessionProvider";
import RouterProvider from "@/context/RouterProvider";
import { Provider } from "react-redux";
import { store } from "./store/store.ts";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <Provider store={store}>
    <QueryProvider>
      <SessionProvider>
        <StationProvider>
          <RouterProvider />
        </StationProvider>
      </SessionProvider>
    </QueryProvider>
  </Provider>
);
