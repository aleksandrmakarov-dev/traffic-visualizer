import { MapContainer, TileLayer, ZoomControl } from "react-leaflet";
import { Map, PM } from "leaflet";
import "./leaflet.css";
import "./root.css";
import { Suspense, useEffect, useRef } from "react";
import Geoman from "./components/Geoman";
import { MapLayers } from "./components/MapLayers";
import { useMapContext } from "@/context/MapProvider";
import { useThemeContext } from "@/context/ThemeProvider";
import { StationCompareDialog } from "@/widgets/station";

export default function Root(): JSX.Element {
  const { center, zoom } = useMapContext();
  const { language } = useThemeContext();

  const mapRef = useRef<Map | null>(null);

  useEffect(() => {
    if (!mapRef.current) return;

    mapRef.current.setView(center, zoom);
  }, [center]);

  useEffect(() => {
    mapRef.current?.pm.setLang(language as PM.SupportLocales);
  }, [language]);

  return (
    <>
      <MapContainer
        center={center}
        maxBoundsViscosity={0.9}
        zoomDelta={1}
        zoom={zoom}
        minZoom={7}
        maxZoom={20}
        ref={mapRef}
        zoomControl={false}
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors | <a target="_blank" href="https://wimma-lab-2023.pages.labranet.jamk.fi/iotitude/core-traffic-visualizer/80-Documents-and-reporting/gdpr-statement/">GDPR</a> | <a target="_blank" href="https://wimma-lab-2023.pages.labranet.jamk.fi/iotitude/core-traffic-visualizer/80-Documents-and-reporting/user-guide/">User Guide for Tukko</a>'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Geoman />
        <Suspense>
          <MapLayers />
        </Suspense>
        <ZoomControl position="bottomright" />
      </MapContainer>
      <StationCompareDialog />
    </>
  );
}
