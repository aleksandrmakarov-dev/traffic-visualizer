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
import { StationResponse } from "@/lib/contracts/station/station.response";
import { Button } from "@/shared/components/ui/button";
import { Checkbox } from "@/shared/components/ui/checkbox";
import { useTranslation } from "react-i18next";
import { TextSearch } from "lucide-react";
import { useThemeContext } from "@/context/ThemeProvider";
import { lineColors, timeRanges } from "@/lib/constants";
import moment from "moment";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/shared/components/ui/select";
import { Label } from "@/shared/components/ui/label";

interface StationHistoryDialogProps {
  trigger?: JSX.Element;
  station: StationResponse;
}

interface LineData {
  date: string; // X-axis label
  [key: string]: number | string; // Each line data can have multiple values
}

export function StationHistoryDialog({
  trigger,
  station,
}: StationHistoryDialogProps) {
  const [timeRange, setTimeRange] = useState<string>(timeRanges.Today);

  const { language } = useThemeContext();
  const { t } = useTranslation(["modal"]);

  const { data, isError, error, refetch } = useStationsHistoryById(
    {
      id: station.id,
      timeRange: timeRange,
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
      <div className="grid grid-cols-[1fr_2fr_2fr] gap-x-5">
        <div className="flex flex-col gap-y-3">
          <Label>{t("range")}</Label>
          <Select defaultValue={timeRange} onValueChange={setTimeRange}>
            <SelectTrigger>
              <SelectValue placeholder="Choose" />
            </SelectTrigger>
            <SelectContent>
              <SelectGroup>
                {Object.entries(timeRanges).map((item) => (
                  <SelectItem key={item[1]} value={item[1]}>
                    {item[0]}
                  </SelectItem>
                ))}
              </SelectGroup>
            </SelectContent>
          </Select>
          <Button onClick={() => refetch()}>Get History</Button>
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
      {data && data.sensors.length > 0 ? (
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
