import { useStationContext } from "@/context/StationProvider";
import { useThemeContext } from "@/context/ThemeProvider";
import { useSensors } from "@/entities/sensor";
import { SensorResponse } from "@/lib/contracts/sensor/sensor.response";
import { Station } from "@/lib/contracts/station/station";
import { DialogBase } from "@/shared/components/DialogBase";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/shared/components/ui/table";
import _ from "lodash";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

type Row = {
  key: string;
  value: SensorResponse[];
};

export function StationCompareDialog() {
  const { language } = useThemeContext();
  const { t } = useTranslation(["station", "sensors", "units"]);
  const { selected, comparator, setComparator } = useStationContext();
  const [rows, setRows] = useState<Row[]>([]);
  const [open, setOpen] = useState<boolean>(true);

  const { data: selectedSensors } = useSensors(
    { stationId: selected?.id },
    { enabled: !!selected }
  );

  const { data: comparatorSensors } = useSensors(
    { stationId: comparator?.id },
    { enabled: !!comparator }
  );

  useEffect(() => {
    setOpen(!!selected && !!comparator);
  }, [selected, comparator]);

  useEffect(() => {
    if (!open) {
      setComparator(null);
    }
  }, [open]);

  useEffect(() => {
    if (selectedSensors && comparatorSensors) {
      const data = _.chain(_.union(selectedSensors, comparatorSensors))
        .groupBy((item) => item.name.replace(/[12]/g, ""))
        .map(
          (value, key): Row => ({
            key: key,
            value: value,
          })
        )
        .value();

      setRows(data);
    }
  }, [selectedSensors, comparatorSensors]);

  if (!selected || !comparator) return null;

  return (
    <DialogBase
      className="w-full max-w-6xl"
      title={t("compareTitle")}
      open={open}
      setOpen={setOpen}
    >
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead />
            <TableHead
              className="text-lg text-center text-foreground"
              colSpan={2}
            >
              {selected.names[language as keyof Station["names"]]}
            </TableHead>
            <TableHead
              className="text-lg text-center text-foreground"
              colSpan={2}
            >
              {comparator.names[language as keyof Station["names"]]}
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          <TableRow>
            <TableCell />
            <TableCell className="font-medium text-center">
              {selected.direction1Municipality}
            </TableCell>
            <TableCell className="font-medium text-center">
              {selected.direction2Municipality}
            </TableCell>
            <TableCell className="font-medium text-center">
              {comparator.direction1Municipality}
            </TableCell>
            <TableCell className="font-medium text-center">
              {comparator.direction2Municipality}
            </TableCell>
          </TableRow>
          {rows.map((row) => (
            <TableRow key={`cr-${row.key}`}>
              <TableCell className="font-medium text-foreground">
                {t(row.key, { ns: "sensors" })}
              </TableCell>
              {row.value.map((cell) => (
                <TableCell key={`cc-${cell.id}`} className="text-center">
                  {cell.value}{" "}
                  {t(cell.unit === "***" ? "%" : cell.unit, {
                    ns: "units",
                  })}
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </DialogBase>
  );
}
