import { v4 as uuidV4 } from "uuid";
import { useCookies } from "react-cookie";

export const useSession = () => {
  const [cookies, setCookie] = useCookies();
  let sessionId: string | undefined = cookies.sessionId;
  if (sessionId) {
    return sessionId;
  }
  const id = uuidV4();
  setCookie("sessionId", id);
  return id;
};
