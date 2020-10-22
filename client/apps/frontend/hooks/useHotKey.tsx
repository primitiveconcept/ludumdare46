import keycode from "keycode";
import { useEffect, useState } from "react";

type Keycode = keyof typeof keycode.codes;

export const useHotKey = () => {
  const [keys, setKeys] = useState<Keycode[]>([]);

  useEffect(() => {
    const keydownHandler = (event: KeyboardEvent) => {
      const keyCode = keycode(event) as Keycode;
      if (event.keyCode && !keys.includes(keyCode)) {
        setKeys([...keys.filter((key) => key !== keyCode), keyCode]);
      }
    };
    const keyupHandler = (event: KeyboardEvent) => {
      setKeys(keys.filter((key) => key !== keycode(event)));
    };
    window.addEventListener("keydown", keydownHandler);
    window.addEventListener("keyup", keyupHandler);
    return () => {
      window.removeEventListener("keydown", keydownHandler);
      window.removeEventListener("keyup", keyupHandler);
    };
  });
  return keys;
};

export const useKeyHandler = (handler: (keyPressed: Keycode) => void) => {
  useEffect(() => {
    const keydownHandler = (event: KeyboardEvent) => {
      const keyPressed = keycode(event);
      if (keyPressed) {
        handler(keyPressed as Keycode);
      }
    };
    window.addEventListener("keydown", keydownHandler);
    return () => {
      window.removeEventListener("keydown", keydownHandler);
    };
  }, [handler]);
};
