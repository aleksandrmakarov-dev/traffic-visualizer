export type LocationResponse = {
  place_id: number;
  lat: number;
  lon: number;
  category: string;
  type: string;
  place_rank: number;
  importance: number;
  addresstype: string;
  name: string;
  display_name: string;
  boundingbox: Array<number>;
}