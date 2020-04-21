import { useState, useEffect, useCallback } from "react";

export const useCommandHistory = (
  history: string[],
  setCommand: (command: string) => void,
) => {
  const [current, setCurrent] = useState(history.length - 1);

  useEffect(() => {
    setCurrent(history.length - 1);
  }, [history]);

  const setPrevCommand = useCallback(() => {
    if (!history.length) {
      return;
    }
    const newCurrent = Math.max(0, current - 1);
    setCommand(history[newCurrent]);
    setCurrent(newCurrent);
  }, [current, history, setCommand]);

  const setNextCommand = useCallback(() => {
    if (!history.length) {
      return;
    }
    if (current === history.length - 1) {
      setCommand("");
    } else {
      const newCurrent = Math.min(history.length - 1, current + 1);
      setCommand(history[newCurrent]);
      setCurrent(newCurrent);
    }
  }, [current, history, setCommand]);

  return { setPrevCommand, setNextCommand };
};
