import { useStationContext } from "@/context/StationProvider";
import { StationDirection } from "@/entities/station";
import {
  Station,
  StationDirectionValue,
} from "@/lib/contracts/station/station";
import { Button } from "@/shared/components/ui/button";
import { RoadworkDetails } from "@/widgets/roadwork";
import { ChevronLeft, TriangleAlert } from "lucide-react";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import {
  StationFavoriteButton,
  StationHistoryDialog,
  StationUnfavoriteButton,
} from "..";
import { useSensors } from "@/entities/sensor";
import _ from "lodash";
import { useSidebarContext } from "@/context/SidebarProvider";
import { useThemeContext } from "@/context/ThemeProvider";

export function StationDetails() {
  const { language } = useThemeContext();
  const { t } = useTranslation(["station"]);
  const { selected, setSelected, favoriteStations, setMode, mode } =
    useStationContext();
  const { setKey } = useSidebarContext();

  const { data: sensors } = useSensors(
    { stationId: selected?.id },
    { enabled: !!selected }
  );

  const [selectedDirection, setSelectedDirection] =
    useState<StationDirectionValue | null>();

  if (!selected) {
    return null;
  }

  const onCancel = () => {
    setKey(null);
    setSelected(null);
    setMode("select");
  };

  const onBack = () => {
    setSelectedDirection(null);
  };

  return (
    <>
      {selectedDirection ? (
        <div className="overflow-auto p-5">
          <StationDirection
            direction={selectedDirection}
            slotTop={
              <Button variant="secondary" size="icon" onClick={onBack}>
                <ChevronLeft />
              </Button>
            }
          />
        </div>
      ) : (
        <>
          <div className="grow overflow-auto">
            <div className="p-5">
              <h4 className="font-medium text-xl flex items-center">
                <span className="mr-1.5">
                  {selected.names[language as keyof Station["names"]]}
                </span>
                {_.includes(favoriteStations, selected.id) ? (
                  <StationUnfavoriteButton stationId={selected.id} />
                ) : (
                  <StationFavoriteButton stationId={selected.id} />
                )}
              </h4>
            </div>
            <div className="p-5 border-t border-border">
              <StationDirection
                direction={{
                  side: 1,
                  name: selected.direction1Municipality,
                  sensors: sensors?.filter((item) =>
                    ["5116", "5122"].includes(item.sensorId)
                  ),
                }}
                slotBottom={
                  <Button
                    variant="ghost"
                    onClick={() =>
                      setSelectedDirection({
                        side: 1,
                        name: selected.direction1Municipality,
                        sensors: sensors?.filter((s) => s.name.endsWith("1")),
                      })
                    }
                  >
                    {t("moreBtn")}
                  </Button>
                }
              />
            </div>
            <div className="p-5 border-t border-border">
              <StationDirection
                direction={{
                  side: 2,
                  name: selected.direction2Municipality,
                  sensors: sensors?.filter((item) =>
                    ["5119", "5125"].includes(item.sensorId)
                  ),
                }}
                slotBottom={
                  <Button
                    variant="ghost"
                    onClick={() =>
                      setSelectedDirection({
                        side: 2,
                        name: selected.direction2Municipality,
                        sensors: sensors?.filter((s) => s.name.endsWith("2")),
                      })
                    }
                  >
                    {t("moreBtn")}
                  </Button>
                }
              />
            </div>
            <div className="p-5 border-t border-border">
              <h5 className="text-lg font-medium mb-1.5">
                {t("historyTitle")}
              </h5>
              <StationHistoryDialog
                station={selected}
                trigger={
                  <Button className="w-full" variant="secondary">
                    {t("historyBtn")}
                  </Button>
                }
              />
            </div>
            <div className="p-5 border-t border-border">
              <h5 className="text-lg font-medium mb-1.5">
                {t("compareTitle")}
              </h5>
              <Button
                className="w-full"
                variant="secondary"
                onClick={() =>
                  setMode((prev) => (prev === "select" ? "compare" : "select"))
                }
              >
                {mode === "select" ? t("compareBtn") : t("waitingBtn")}
              </Button>
            </div>
            {selected.roadworks && selected.roadworks.length > 0 && (
              <div className="p-5 border-t border-border">
                <h5 className="text-lg font-medium mb-1.5 flex items-center">
                  <span className="mr-1.5">{t("roadworksTitle")}</span>
                  <TriangleAlert className="w-5 h-5 text-yellow-500" />
                </h5>
                <RoadworkDetails roadworks={selected.roadworks} />
              </div>
            )}
          </div>
          <div className="p-2.5">
            <Button type="button" className="w-full" onClick={onCancel}>
              {t("closeBtn")}
            </Button>
          </div>
        </>
      )}
    </>
  );
}
