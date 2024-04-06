import { useStationsHistoryById } from "@/entities/station";
import { SensorResponse } from "@/lib/contracts/sensor/sensor.response";
import { DialogBase } from "@/shared/components/DialogBase";
import { FormAlert } from "@/shared/components/FormAlert";
import _ from "lodash";
import { useEffect, useState } from "react";
import { Legend, Line, YAxis } from "recharts";
import { Tooltip } from "recharts";
import { CartesianGrid, XAxis } from "recharts";
import { LineChart, ResponsiveContainer } from "recharts";
import moment from "moment";
import { StationResponse } from "@/lib/contracts/station/station.response";
import { useStationContext } from "@/context/StationProvider";
import { Button } from "@/shared/components/ui/button";
import { Checkbox } from "@/shared/components/ui/checkbox";
import { useTranslation } from "react-i18next";
import { TextSearch } from "lucide-react";

interface StationHistoryDialogProps {
  trigger?: JSX.Element;
  station: StationResponse;
}

interface LineData {
  date: string; // X-axis label
  [key: string]: number | string; // Each line data can have multiple values
}

const lineColors = [
  "#1f77b4", // Blue
  "#ff7f0e", // Orange
  "#2ca02c", // Green
  "#d62728", // Red
  "#9467bd", // Purple
  "#8c564b", // Brown
  "#e377c2", // Pink
  "#7f7f7f", // Gray
  "#bcbd22", // Yellow-green
  "#17becf", // Cyan
  "#aec7e8", // Light blue
  "#ffbb78", // Light orange
  "#98df8a", // Light green
  "#ff9896", // Light red
  "#c5b0d5", // Light purple
  "#c49c94", // Light brown
  "#f7b6d2", // Light pink
  "#c7c7c7", // Light gray
];

export function StationHistoryDialog({
  trigger,
  station,
}: StationHistoryDialogProps) {
  const { language } = useStationContext();
  const { t } = useTranslation(["modal"]);

  const { data, isError, error, refetch } = useStationsHistoryById(
    {
      id: station.id,
    },
    { enabled: false }
  );

  const [lines, setLines] = useState<LineData[]>([]);
  const [open, setOpen] = useState<boolean>(false);
  const [uniqueSensors, setUniqueSensors] = useState<SensorResponse[]>([]);

  useEffect(() => {
    if (!data) {
      return;
    }

    const sensors = _(data.sensors)
      .map((s) => ({
        ...s,
        measuredTime: moment(s.measuredTime).format("DD-MM-YYYY HH:mm"),
      }))
      .sortBy((item) => item.measuredTime)
      .groupBy("measuredTime")
      .map(
        (group, key): LineData => ({
          date: key,
          ...group.reduce(
            (acc: Record<string, number>, item: SensorResponse) => {
              acc[item.sensorId] = item.value;
              return acc;
            },
            {}
          ),
        })
      )
      .value();

    const unique = _.uniqBy(data?.sensors, (item) => item.sensorId);

    setUniqueSensors(unique);

    setLines(sensors);
  }, [data]);

  return (
    <DialogBase
      className="w-full max-w-6xl"
      trigger={trigger}
      title={station.names[language as keyof StationResponse["names"]]}
      open={open}
      setOpen={setOpen}
    >
      <div className="grid grid-cols-3">
        <div>
          <p>{t("range")}</p>
          <Button onClick={() => refetch()}>Load</Button>
        </div>
        <div>
          <h5 className="text-xl font-medium mb-1.5">
            Direction: {station.direction1Municipality}
          </h5>
          <ul>
            {_.filter(uniqueSensors, (item) => item.name.endsWith("1")).map(
              (item) => (
                <li key={`checkbox-${item.id}`}>
                  <Checkbox className="mr-1.5" />
                  <label>{t(item.name, { ns: "modal" })}</label>
                </li>
              )
            )}
          </ul>
        </div>
        <div>
          <h5 className="text-xl font-medium mb-1.5">
            Direction: {station.direction2Municipality}
          </h5>
          <ul>
            {_.filter(uniqueSensors, (item) => item.name.endsWith("2")).map(
              (item) => (
                <li key={`checkbox-${item.id}`}>
                  <Checkbox className="mr-1.5" />
                  <label>{t(item.name, { ns: "modal" })}</label>
                </li>
              )
            )}
          </ul>
        </div>
      </div>
      <FormAlert
        className="mb-2"
        isError={isError}
        error={error?.response?.data}
      />
      {data ? (
        <ResponsiveContainer width="100%" height={300}>
          <LineChart data={lines}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="date" />
            <YAxis />
            <Tooltip />
            <Legend />
            {uniqueSensors.map((item, index) => (
              <Line
                key={item.sensorId}
                type="monotone"
                name={t(item.name, { ns: "modal" })}
                dataKey={item.sensorId}
                stroke={lineColors[index % lineColors.length]}
                strokeWidth={2}
              />
            ))}
          </LineChart>
        </ResponsiveContainer>
      ) : (
        <div className="h-72 flex flex-col justify-center items-center">
          <TextSearch />
          <p>No data</p>
        </div>
      )}
    </DialogBase>
  );
}
