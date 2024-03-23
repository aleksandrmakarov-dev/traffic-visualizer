export type RoadworkResponse = {
  id: string;
  primaryPointRoadNumber: number | null;
  primaryPointRoadSection: number | null;
  secondaryPointRoadNumber: number | null;
  secondaryPointRoadSection: number | null;
  direction: string | null;
  startTime: string;
  endTime: string;
  severity: string | null;
  workTypes: WorkType[];
  workingHours: WorkingHours[];
  restrictions: Restriction[];
};

export type WorkType = {
  type: string;
  description: string;
};

export type WorkingHours = {
  weekday: string;
  startTime: string;
  endTime: string;
};

export interface Restriction {
  type: string;
  name: string;
  quantity: number;
  unit: string;
}
