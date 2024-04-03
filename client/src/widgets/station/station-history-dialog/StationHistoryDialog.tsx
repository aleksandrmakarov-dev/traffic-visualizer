import { DialogBase } from "@/shared/components/DialogBase";
import { FormAlert } from "@/shared/components/FormAlert";
import { useState } from "react";

interface StationHistoryDialogProps {
  trigger?: JSX.Element;
  stationId: string;
}

export function StationHistoryDialog({
  trigger,
  stationId,
}: StationHistoryDialogProps) {
  const [open, setOpen] = useState<boolean>(false);

  return (
    <DialogBase trigger={trigger} title="History" open={open} setOpen={setOpen}>
      <FormAlert className="mb-2" />
    </DialogBase>
  );
}
