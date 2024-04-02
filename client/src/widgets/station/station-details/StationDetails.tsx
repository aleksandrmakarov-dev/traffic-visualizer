import { useStationContext } from "@/context/StationContext";
import { StationDirection } from "@/entities/station";
import { Station } from "@/lib/contracts/station/station";
import { Button } from "@/shared/components/ui/button";
import { RoadworkDetails } from "@/widgets/roadwork";
import { TriangleAlert } from "lucide-react";
import { useTranslation } from "react-i18next";

export function StationDetails() {
  const { selectedStation, setSelectedStation, language } = useStationContext();
  useTranslation(["tooltip", "roadworks", "sensors", "units"]);

  if (!selectedStation) {
    return null;
  }

  const onCancel = () => {
    setSelectedStation(null);
  };

  return (
    <div className="bg-white pt-14 h-screen flex flex-col">
      <div className="h-full overflow-auto">
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
            <h5 className="text-lg font-medium mb-1.5 flex items-center">
              <span className="mr-1.5">Roadworks</span>
              <TriangleAlert className="w-5 h-5 text-orange-600" />
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
    </div>
  );
}
