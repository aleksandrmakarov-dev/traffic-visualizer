import L from "leaflet";

const icon = (
  color1: string,
  color2: string,
  roadwork: boolean = false,
  shadowOpacity: number = 0.25
) => `
<span style="position:relative;">
  <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="48" height="48" viewBox="0 0 256 256" xml:space="preserve">
    <g style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: none; fill-rule: nonzero; opacity: 1;" transform="translate(1.4065934065934016 1.4065934065934016) scale(2.81 2.81)" >
      <path d="M 48.647 69.718 c 13.692 0.652 24.265 4.924 24.265 10.098 C 72.912 85.44 60.415 90 45 90 s -27.912 -4.56 -27.912 -10.184 c 0 -5.173 10.573 -9.446 24.265 -10.098" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: #353b48; fill-rule: nonzero; opacity: ${shadowOpacity};" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
      <path d="M 45 79.665 l 21.792 -6.211 c -3.033 -1.381 -7.032 -2.466 -11.622 -3.122 L 45 79.665 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: #2f3640; fill-rule: nonzero; opacity: ${shadowOpacity};" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
      <path d="M 45 0 C 30.802 0 19.291 11.51 19.291 25.709 c 0 20.07 21.265 33.961 25.709 53.956 C 48.304 53.11 48.304 26.555 45 0 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: ${color1}; fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
      <path d="M 45 14.965 c -6.011 0 -10.885 4.873 -10.885 10.885 S 38.989 36.735 45 36.735 C 47.897 29.478 47.897 22.222 45 14.965 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(255,255,255); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
      <path d="M 45 0 c 14.198 0 25.709 11.51 25.709 25.709 c 0 20.07 -21.265 33.961 -25.709 53.956 V 0 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: ${color2}; fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
      <path d="M 45 14.965 c 6.011 0 10.885 4.873 10.885 10.885 S 51.011 36.735 45 36.735 V 14.965 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(255,255,255); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
    </g>
  </svg>
${
  roadwork
    ? `<svg xmlns="http://www.w3.org/2000/svg" height="1.15em" viewBox="0 0 640 512" style="position:absolute; top:24; left:-8;">
        <!--! Font Awesome Free 6.4.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
        <path 
          style="
            position: absolute;
            fill: yellow; 
            stroke:#000000; 
            opacity: 1; 
            stroke-width: 6mm; 
            stroke-opacity: 1;
          " 
        d="M213.2 32H288V96c0 17.7 14.3 32 32 32s32-14.3 32-32V32h74.8c27.1 0 51.3 17.1 60.3 42.6l42.7 120.6c-10.9-2.1-22.2-3.2-33.8-3.2c-59.5 0-112.1 29.6-144 74.8V224c0-17.7-14.3-32-32-32s-32 14.3-32 32v64c0 17.7 14.3 32 32 32c2.3 0 4.6-.3 6.8-.7c-4.5 15.5-6.8 31.8-6.8 48.7c0 5.4 .2 10.7 .7 16l-.7 0c-17.7 0-32 14.3-32 32v64H86.6C56.5 480 32 455.5 32 425.4c0-6.2 1.1-12.4 3.1-18.2L152.9 74.6C162 49.1 186.1 32 213.2 32zM496 224a144 144 0 1 1 0 288 144 144 0 1 1 0-288zm0 240a24 24 0 1 0 0-48 24 24 0 1 0 0 48zm0-192c-8.8 0-16 7.2-16 16v80c0 8.8 7.2 16 16 16s16-7.2 16-16V288c0-8.8-7.2-16-16-16z"/>
      </svg>`
    : ""
}
</span>
`;

export type CreateIconParams = {
  leftColor: string;
  rightColor: string;
  isRoadwork: boolean;
};

export const createIcon = ({
  leftColor,
  rightColor,
  isRoadwork,
}: CreateIconParams) => {
  return L.divIcon({
    html: icon(leftColor, rightColor, isRoadwork),
    className: "customMarker",
    iconSize: [48, 48],
    iconAnchor: [24, 48],
    popupAnchor: [0, -36],
  });
};
