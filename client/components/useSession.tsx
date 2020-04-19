import { useCookies } from "react-cookie";
import { useCallback } from "react";

// sessionId is handled by the server, but we keep track of the name.
export const useSession = () => {
  const [cookies, setCookie] = useCookies();

  const username = cookies.username ?? "";
  const setUsername = useCallback(
    (name: string) => {
      const oneYearFromNow = new Date();
      oneYearFromNow.setFullYear(oneYearFromNow.getFullYear() + 1);
      setCookie("username", name, {
        expires: oneYearFromNow,
      });
    },
    [setCookie],
  );
  return [username, setUsername];
};
