import useWebSocket from "react-use-websocket";

export const useNotificationSubWebSocket = () => {
  return useWebSocket(
    `${import.meta.env.VITE_WS_BACKEND_BASE_URL}/notifications/sub`,
    {}
  );
};
