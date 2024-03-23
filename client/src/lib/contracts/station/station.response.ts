export type Coordinates = {
  longitude: number;
  latitude: number;
};

export type Names = {
  fi: string;
  sv: string;
  en: string;
};

export type StationResponse = {
  id: string;
  tmsNumber: number;
  dataUpdatedTime: string;
  name: string;
  names: Names;
  coordinates: Coordinates;
  roadNumber: number;
  roadSection: number;
  carriageway: string;
  side: string;
  municipality: string;
  municipalityCode: number;
  province: string;
  provinceCode: number;
  direction1Municipality: string;
  direction1MunicipalityCode: number;
  direction2Municipality: string;
  direction2MunicipalityCode: number;
  freeFlowSpeed1: number | null;
  freeFlowSpeed2: number | null;
};
