import { SensorItem, SensorList } from "@/entities/sensor";
import { StationDirectionValue } from "@/lib/contracts/station/station";
import { MoveLeft, MoveRight } from "lucide-react";
import { useTranslation } from "react-i18next";

interface StationDirectionProps {
  direction: StationDirectionValue;
  slotTop?: React.ReactNode;
  slotBottom?: React.ReactNode;
}

export function StationDirection({
  direction,
  slotTop,
  slotBottom,
}: StationDirectionProps) {
  const { t } = useTranslation(["sensors", "units"]);

  return (
    <div className="flex flex-col gap-y-2">
      {slotTop}
      <div className="flex items-center justify-between gap-x-3">
        <h5 className="text-lg font-medium">{direction.name}</h5>
        {direction.side == 1 ? <MoveLeft /> : <MoveRight />}
      </div>
      {direction.sensors && direction.sensors.length > 0 ? (
        <SensorList
          items={direction.sensors}
          render={(sensor) => (
            <SensorItem
              key={`station-direction-sensor-${sensor.id}`}
              label={t(sensor.name, { ns: "sensors" })}
              value={sensor.value}
              units={t(sensor.unit === "***" ? "%" : sensor.unit, {
                ns: "units",
              })}
            />
          )}
        />
      ) : (
        <p>No measurements found</p>
      )}
      {slotBottom}
    </div>
  );
}
