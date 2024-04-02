import { RoadworkItem, RoadworkList } from "@/entities/roadwork";
import { RoadworkResponse } from "@/lib/contracts/roadwork/roadwork.response";

interface RoadworkDetailsProps {
  roadworks?: RoadworkResponse[];
}

export function RoadworkDetails({ roadworks }: RoadworkDetailsProps) {
  if (!roadworks) {
    return null;
  }

  return (
    <RoadworkList
      items={roadworks}
      render={(item) => (
        <RoadworkItem
          key={`sidebar-roadwork-${item.id}`}
          className="mb-5"
          roadwork={item}
        />
      )}
    />
  );
}
