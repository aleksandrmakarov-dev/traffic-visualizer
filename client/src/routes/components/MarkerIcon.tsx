import L from "leaflet";
import getTrafficColor from "../scripts/colourSpectrum";
import { Station } from "@/lib/contracts/station/station";

const createIcon = (html: string) => {
  return L.divIcon({
    html: html,
    className: "customMarker",
    iconSize: [24, 24],
    iconAnchor: [24, 24],
  });
};

const rwIcon = `
        <svg xmlns="http://www.w3.org/2000/svg" height="1.15em" viewBox="0 0 640 512" style="position:absolute;bottom:-1px;left:-11px">
        <!--! Font Awesome Free 6.4.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
        <path 
          style="
            fill: yellow; 
            stroke:#000000; 
            opacity: 1; 
            stroke-width: 6mm; 
            stroke-opacity: 1;
          " 
        d="M213.2 32H288V96c0 17.7 14.3 32 32 32s32-14.3 32-32V32h74.8c27.1 0 51.3 17.1 60.3 42.6l42.7 120.6c-10.9-2.1-22.2-3.2-33.8-3.2c-59.5 0-112.1 29.6-144 74.8V224c0-17.7-14.3-32-32-32s-32 14.3-32 32v64c0 17.7 14.3 32 32 32c2.3 0 4.6-.3 6.8-.7c-4.5 15.5-6.8 31.8-6.8 48.7c0 5.4 .2 10.7 .7 16l-.7 0c-17.7 0-32 14.3-32 32v64H86.6C56.5 480 32 455.5 32 425.4c0-6.2 1.1-12.4 3.1-18.2L152.9 74.6C162 49.1 186.1 32 213.2 32zM496 224a144 144 0 1 1 0 288 144 144 0 1 1 0-288zm0 240a24 24 0 1 0 0-48 24 24 0 1 0 0 48zm0-192c-8.8 0-16 7.2-16 16v80c0 8.8 7.2 16 16 16s16-7.2 16-16V288c0-8.8-7.2-16-16-16z"/>
        </svg>
`;

const icon = `
<svg height="24" version="1.1" width="24" xmlns="http://www.w3.org/2000/svg" xmlns:cc="http://creativecommons.org/ns#" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#">
  <g transform="translate(0 -1028.4)">
    <path d="m12.031 1030.4c-3.8657 0-6.9998 3.1-6.9998 7 0 1.3 0.4017 2.6 1.0938 3.7 0.0334 0.1 0.059 0.1 0.0938 0.2l4.3432 8c0.204 0.6 0.782 1.1 1.438 1.1s1.202-0.5 1.406-1.1l4.844-8.7c0.499-1 0.781-2.1 0.781-3.2 0-3.9-3.134-7-7-7zm-0.031 3.9c1.933 0 3.5 1.6 3.5 3.5 0 2-1.567 3.5-3.5 3.5s-3.5-1.5-3.5-3.5c0-1.9 1.567-3.5 3.5-3.5z" fill="#c0392b"/><path d="m12.031 1.0312c-3.8657 0-6.9998 3.134-6.9998 7 0 1.383 0.4017 2.6648 1.0938 3.7498 0.0334 0.053 0.059 0.105 0.0938 0.157l4.3432 8.062c0.204 0.586 0.782 1.031 1.438 1.031s1.202-0.445 1.406-1.031l4.844-8.75c0.499-0.963 0.781-2.06 0.781-3.2188 0-3.866-3.134-7-7-7zm-0.031 3.9688c1.933 0 3.5 1.567 3.5 3.5s-1.567 3.5-3.5 3.5-3.5-1.567-3.5-3.5 1.567-3.5 3.5-3.5z" fill="#e74c3c" transform="translate(0 1028.4)"/>
  </g>
</svg>`;

export function getSwgImage(station: Station) {
  try {
    let colorDirection1 = "gray";
    let colorDirection2 = "gray";

    const findSensorValue = (sensorId: string) =>
      station.sensors?.find((sensor) => sensor.sensorId === sensorId)?.value;

    const sensorValue5158 = findSensorValue("5158");

    if (sensorValue5158 !== undefined) {
      colorDirection1 = getTrafficColor(sensorValue5158);
    }

    const sensorValue5161 = findSensorValue("5161");

    if (sensorValue5161 !== undefined) {
      colorDirection2 = getTrafficColor(sensorValue5161);
    }

    let baseIcon = `
              <svg xmlns="http://www.w3.org/2000/svg" xmlns:svg="http://www.w3.org/2000/svg" width="31px" height="40px" viewBox="0 0 102.30963 133.72514" version="1.1" id="svg5">
              <g id="g450" transform="translate(-76.460176,-68.607115)">
              <path
              xmlns="http://www.w3.org/2000/svg"
              d="m 129.4992,68.607117 c -28.04582,0 -53.039022,22.769503 -53.039022,50.815333 0,23.12458 37.581502,65.92356 51.154622,82.90981 -0.18282,-133.641124 4.48377,-74.00643 1.8844,-133.725143 z"
              id="path191"
              style="
                fill: ${colorDirection1};
                opacity: 0.70;
                stroke: #000000;
                stroke-width: 1mm;
                stroke-dasharray: none;
                stroke-opacity: 1;
              "
            />
            <path
              xmlns="http://www.w3.org/2000/svg"
              d="m 128.50552,68.813498 c 25.61443,-0.01145 50.2643,22.562696 50.2643,50.608522 0,23.12458 -37.58151,65.92356 -51.15463,82.90981 0.18282,-133.641121 -1.13341,-133.483369 0.89033,-133.518332 z"
              id="path404"
              style="
                fill: ${colorDirection2};
                opacity: 0.70;
                stroke: #000000;
                stroke-width: 1mm;
                stroke-dasharray: none;
                stroke-opacity: 1;
              "
            />      
            </svg>
        `;
    if (station.roadworks && station.roadworks.length !== 0) baseIcon += rwIcon;
    return createIcon(baseIcon);
  } catch (error: any) {
    console.log(error.message);
  }
}
