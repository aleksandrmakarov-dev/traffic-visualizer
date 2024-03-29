import { useStationContext } from "@/context/StationContext";
import { StationDirection } from "@/entities/station";
import { Station } from "@/lib/contracts/station/station";
import { useTranslation } from "react-i18next";

export function StationDetails() {
  const { selectedStation, language } = useStationContext();
  const { t } = useTranslation(["tooltip", "roadworks", "sensors", "units"]);

  if (!selectedStation) {
    return null;
  }

  console.log(selectedStation.roadworks);

  return (
    <div className="bg-white pt-14 h-screen overflow-auto">
      <div className="p-5">
        <h4 className="font-medium text-xl">
          {selectedStation.names[language as keyof Station["names"]]}
        </h4>
      </div>
      <div className="p-5 border-t border-border">
        <StationDirection
          direction="left"
          station={selectedStation}
          sensors={[
            {
              id: "5116",
              label: "Flow",
              unitsName: "amount",
            },
            {
              id: "5122",
              label: "Speed",
              unitsName: "speed",
            },
          ]}
        />
      </div>
      <div className="p-5 border-t border-border">
        <StationDirection
          direction="right"
          station={selectedStation}
          sensors={[
            {
              id: "5119",
              label: "Flow",
              unitsName: "amount",
            },
            {
              id: "5125",
              label: "Speed",
              unitsName: "speed",
            },
          ]}
        />
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
