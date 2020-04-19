import { useEffect } from "react";
import keycode from "keycode";

export const useInputFocus = (
  onSubmit: () => void,
  inputRef: React.RefObject<HTMLInputElement>,
) => {
  useEffect(() => {
    const focusInput = (event: KeyboardEvent) => {
      inputRef.current?.focus();
      if (event.keyCode === keycode.codes.enter) {
        onSubmit();
      }
    };
    document.addEventListener("keypress", focusInput);

    return () => {
      document.removeEventListener("keypress", focusInput);
    };
  }, [inputRef, onSubmit]);
};
