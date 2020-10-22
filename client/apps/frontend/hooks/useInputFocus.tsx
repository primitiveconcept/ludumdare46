import { useEffect, useContext } from "react";
import keycode from "keycode";
import { CommandContext } from "@botnet/commands";

export const useInputFocus = (
  onSubmit: () => void,
  inputRef: React.RefObject<HTMLInputElement>,
) => {
  const { setNextCommand, setPrevCommand } = useContext(CommandContext);
  useEffect(() => {
    const focusInput = (event: KeyboardEvent): void => {
      // don't interfere with accessibility
      if (event.keyCode === keycode.codes.tab) {
        return;
      }

      inputRef.current?.focus();
      if (event.keyCode === keycode.codes.enter) {
        return onSubmit();
      }
      if (event.keyCode === keycode.codes.up) {
        // prevent scrolling, that would be bad
        event.preventDefault();
        setPrevCommand();
        return;
      }
      if (event.keyCode === keycode.codes.down) {
        // prevent scrolling, that would be bad
        event.preventDefault();
        setNextCommand();
        return;
      }
    };
    document.addEventListener("keydown", focusInput);

    return () => {
      document.removeEventListener("keydown", focusInput);
    };
  }, [inputRef, onSubmit, setNextCommand, setPrevCommand]);
};
