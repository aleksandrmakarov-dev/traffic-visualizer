import { SensorItem, SensorList } from "@/entities/sensor";
import { Station } from "@/lib/contracts/station/station";
import { Button } from "@/shared/components/ui/button";
import { MoveLeft, MoveRight } from "lucide-react";
import { useTranslation } from "react-i18next";

interface StationDirectionProps {
  station: Station;
  direction: "left" | "right";
  sensors?: { label: string; id: string; unitsName: string }[];
}

export function StationDirection({
  station,
  direction,
  sensors,
}: StationDirectionProps) {
  const { t } = useTranslation(["tooltip"]);

  return (
    <div>
      <div className="flex items-center justify-between gap-x-3 mb-1.5">
        <h5 className="text-lg font-medium">
          {direction == "left"
            ? station.direction1Municipality
            : station.direction2Municipality}
        </h5>
        {direction == "left" ? <MoveLeft /> : <MoveRight />}
      </div>
      <SensorList
        items={sensors?.map((sensor) => ({
          ...sensor,
          value: station.sensors?.find((s) => sensor.id === s.sensorId)?.value,
        }))}
        render={({ id, label, unitsName, value }) => (
          <SensorItem
            key={`station-direction-sensor-${id}`}
            label={label}
            value={value}
            units={t(unitsName)}
          />
        )}
      />
      <div className="text-center mt-1.5">
        <Button variant="ghost">More details</Button>
      </div>
    </div>
  );
}
