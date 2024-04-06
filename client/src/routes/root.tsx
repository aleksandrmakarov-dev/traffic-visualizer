import { MapContainer, TileLayer, ZoomControl } from "react-leaflet";
import { Map } from "leaflet";
import "./leaflet.css";
import "./root.css";
import { Fragment, Suspense, useEffect, useRef } from "react";
import Geoman from "./components/Geoman";
import { MapLayers } from "./components/MapLayers";

export default function Root(): JSX.Element {
  // const { language } = useStationContext();
  const mapRef = useRef<Map | null>(null);

  // useEffect(() => {
  //   if (!mapRef.current) return;

  // //   mapRef.current.setView(center, zoom);
  // // }, [center]);

  // useEffect(() => {
  //   const lang = language === "fi" ? "fi" : "en";
  //   mapRef.current?.pm.setLang(lang);
  // }, [language]);

  return (
    <Fragment>
      <MapContainer
        center={[60, 24]}
        maxBoundsViscosity={0.9}
        zoomDelta={1}
        zoom={12}
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
    </Fragment>
  );
}
