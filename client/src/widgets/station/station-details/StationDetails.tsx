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

export function StationDetails() {
  const { selected, setSelected, favoriteStations } = useStationContext();
  const { setKey } = useSidebarContext();
  useTranslation(["tooltip", "roadworks", "sensors", "units", "modal"]);

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
  };

  const onBack = () => {
    setSelectedDirection(null);
  };

  return (
    <div className="bg-white w-full flex flex-col dark:bg-gray-900">
      {selectedDirection ? (
        <div className="h-full overflow-auto p-5">
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
          <div className="h-full overflow-auto">
            <div className="p-5">
              <h4 className="font-medium text-xl flex items-center">
                <span className="mr-1.5">
                  {selected.names["en" as keyof Station["names"]]}
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
                        sensors: sensors,
                      })
                    }
                  >
                    More
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
                        sensors: sensors,
                      })
                    }
                  >
                    More
                  </Button>
                }
              />
            </div>
            <div className="p-5 border-t border-border">
              <h5 className="text-lg font-medium mb-1.5">History</h5>
              <StationHistoryDialog
                station={selected}
                trigger={
                  <Button className="w-full" variant="secondary">
                    Open History Data
                  </Button>
                }
              />
            </div>
            {selected.roadworks && selected.roadworks.length > 0 && (
              <div className="p-5 border-t border-border">
                <h5 className="text-lg font-medium mb-1.5 flex items-center">
                  <span className="mr-1.5">Roadworks</span>
                  <TriangleAlert className="w-5 h-5 text-yellow-500" />
                </h5>
                <RoadworkDetails roadworks={selected.roadworks} />
              </div>
            )}
          </div>
          <div className="p-2.5">
            <Button type="button" className="w-full" onClick={onCancel}>
              Close
            </Button>
          </div>
        </>
      )}
    </div>
  );
}
