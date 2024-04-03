import { DialogBase } from "@/shared/components/DialogBase";
import { FormAlert } from "@/shared/components/FormAlert";
import { useState } from "react";
import { Legend, YAxis } from "recharts";
import { Tooltip } from "recharts";
import { CartesianGrid, XAxis } from "recharts";
import { LineChart, ResponsiveContainer } from "recharts";

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
    <DialogBase
      className="w-full"
      trigger={trigger}
      title="History"
      open={open}
      setOpen={setOpen}
    >
      <FormAlert className="mb-2" />
      <ResponsiveContainer width="100%" height={300}>
        <LineChart>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="date" />
          <YAxis />
          <Tooltip />
          <Legend />
        </LineChart>
      </ResponsiveContainer>
    </DialogBase>
  );
}
