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
import { StationHistoryDialog } from "..";

export function StationDetails() {
  const { selectedStation, setSelectedStation, language } = useStationContext();
  useTranslation(["tooltip", "roadworks", "sensors", "units", "modal"]);

  const [selectedDirection, setSelectedDirection] =
    useState<StationDirectionValue | null>();

  if (!selectedStation) {
    return null;
  }

  const onCancel = () => {
    setSelectedStation(null);
  };

  const onBack = () => {
    setSelectedDirection(null);
  };

  return (
    <div className="bg-white pt-14 h-screen flex flex-col">
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
              <h4 className="font-medium text-xl">
                {selectedStation.names[language as keyof Station["names"]]}
              </h4>
            </div>
            <div className="p-5 border-t border-border">
              <StationDirection
                direction={{
                  side: 1,
                  name: selectedStation.direction1Municipality,
                  sensors: selectedStation.sensors?.filter((item) =>
                    ["5116", "5122"].includes(item.sensorId)
                  ),
                }}
                slotBottom={
                  <Button
                    variant="ghost"
                    onClick={() =>
                      setSelectedDirection({
                        side: 1,
                        name: selectedStation.direction1Municipality,
                        sensors: selectedStation.sensors,
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
                  name: selectedStation.direction2Municipality,
                  sensors: selectedStation.sensors?.filter((item) =>
                    ["5119", "5125"].includes(item.sensorId)
                  ),
                }}
                slotBottom={
                  <Button
                    variant="ghost"
                    onClick={() =>
                      setSelectedDirection({
                        side: 2,
                        name: selectedStation.direction2Municipality,
                        sensors: selectedStation.sensors,
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
                station={selectedStation}
                trigger={
                  <Button className="w-full" variant="secondary">
                    Open History Data
                  </Button>
                }
              />
            </div>
            {selectedStation.roadworks &&
              selectedStation.roadworks.length > 0 && (
                <div className="p-5 border-t border-border">
                  <h5 className="text-lg font-medium mb-1.5 flex items-center">
                    <span className="mr-1.5">Roadworks</span>
                    <TriangleAlert className="w-5 h-5 text-yellow-500" />
                  </h5>
                  <RoadworkDetails roadworks={selectedStation.roadworks} />
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
