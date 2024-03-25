import { useStationContext } from "@/context/StationContext";
import { Station } from "@/lib/contracts/station/station";
import { Button } from "@/shared/components/ui/button";
import { MoveLeft, MoveRight } from "lucide-react";
import { useTranslation } from "react-i18next";

export function StationDetails() {
  const { selectedStation, language } = useStationContext();
  const { t } = useTranslation(["tooltip", "roadworks", "sensors", "units"]);

  if (!selectedStation) {
    return null;
  }

  console.log(selectedStation);

  return (
    <div className="bg-white pt-14 h-screen overflow-auto">
      <div className="p-5">
        <h4 className="font-medium text-xl">
          {selectedStation.names[language as keyof Station["names"]]}
        </h4>
      </div>
      <div className="p-5 border-t border-border">
        <div className="flex items-center justify-between gap-x-3">
          <h5 className="text-lg font-medium mb-1.5">
            {selectedStation.direction1Municipality}
          </h5>
          <MoveRight />
        </div>
        <p>
          Flow:{" "}
          {selectedStation.sensors?.find((e) => e.sensorId === "5116")?.value ||
            "-"}{" "}
          {t(["amount"])}
        </p>
        <p className="mb-1.5">
          Speed:{" "}
          {selectedStation.sensors?.find((e) => e.sensorId === "5122")?.value ||
            "-"}{" "}
          {t("speed")}
        </p>
        <div className="text-center">
          <Button variant="ghost">More details</Button>
        </div>
      </div>
      <div className="p-5 border-t border-border">
        <div className="flex items-center justify-between gap-x-3">
          <h5 className="text-lg font-medium mb-1.5">
            {selectedStation.direction2Municipality}
          </h5>
          <MoveLeft />
        </div>
        <p>
          Flow:{" "}
          {selectedStation.sensors?.find((e) => e.sensorId === "5119")?.value ||
            "-"}{" "}
          {t(["amount"])}
        </p>
        <p className="mb-1.5">
          Speed:{" "}
          {selectedStation.sensors?.find((e) => e.sensorId === "5125")?.value ||
            "-"}{" "}
          {t("speed")}
        </p>
        <div className="text-center">
          <Button variant="ghost">More details</Button>
        </div>
      </div>
      {selectedStation.roadworks && selectedStation.roadworks.length > 0 && (
        <div className="p-5 border-t border-border">
          <h5 className="text-lg font-medium">Roadworks</h5>
          {selectedStation.roadworks.map((rw) => (
            <div key={rw.id}>
              <p style={{ marginTop: 0, marginBottom: "1.33em" }}>
                {new Date(rw.startTime).toLocaleDateString("fi-FI")} -{" "}
                {new Date(rw.endTime).toLocaleDateString("fi-FI")}
              </p>
              <h4 style={{ marginBottom: 0 }}>
                {t("worktypes", { ns: "roadworks" })}:
              </h4>
              {rw.workTypes.map((workType, index) => (
                <li key={index}>
                  {language === "fi"
                    ? workType.description !== ""
                      ? workType.description
                      : "Muu"
                    : ""}
                </li>
              ))}
              <h4 style={{ marginBottom: 0 }}>
                {t("restrictions", { ns: "roadworks" })}
              </h4>
              {rw.restrictions.map((restriction, index) => (
                <li key={index}>
                  {language === "fi" ? restriction.name : ""}
                  {restriction.quantity && restriction.unit
                    ? " (" + restriction.quantity + " " + restriction.unit + ")"
                    : null}
                </li>
              ))}
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
