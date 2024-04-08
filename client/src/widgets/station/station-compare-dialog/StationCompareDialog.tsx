import { useStationContext } from "@/context/StationProvider";
import { DialogBase } from "@/shared/components/DialogBase";
import { useEffect, useState } from "react";

export function StationCompareDialog() {
  const { selected, comparator } = useStationContext();
  const [open, setOpen] = useState<boolean>(true);

  useEffect(() => {
    setOpen(!!selected && !!comparator);
  }, [selected, comparator]);

  if (!selected || !comparator) return null;

  return (
    <DialogBase
      className="w-full max-w-2xl"
      title="Compare"
      open={open}
      setOpen={setOpen}
    >
      <div className="grid grid-cols-2">
        <div>
          <h5>{selected.name}</h5>
        </div>
        <div>
          <h5>{comparator.name}</h5>
        </div>
      </div>
    </DialogBase>
  );
}
