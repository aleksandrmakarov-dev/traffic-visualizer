import useWebSocket from "react-use-websocket";

export const useNotificationSubWebSocket = () => {
  return useWebSocket(`wss://localhost:8080/api/Notifications/sub`, {});
};
