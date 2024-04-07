import { useThemeContext } from "@/context/ThemeProvider";
import { RoadworkResponse } from "@/lib/contracts/roadwork/roadwork.response";
import { capitalize, cn } from "@/lib/utils";
import { HTMLAttributes } from "react";
import { useTranslation } from "react-i18next";

interface RoadworkItemProps extends HTMLAttributes<HTMLDivElement> {
  roadwork: RoadworkResponse;
}

export function RoadworkItem({
  roadwork,
  className,
  ...other
}: RoadworkItemProps) {
  const { language } = useThemeContext();
  const { t } = useTranslation(["tooltip", "roadworks", "sensors", "units"]);

  return (
    <div className={cn(className)} {...other}>
      <p className="underline mb-1.5">
        {new Date(roadwork.startTime).toLocaleDateString("fi-FI")} -{" "}
        {new Date(roadwork.endTime).toLocaleDateString("fi-FI")}
      </p>
      <h4 className="font-medium">{t("worktypes", { ns: "roadworks" })}:</h4>
      <ul className="pl-2 my-1.5">
        {roadwork.workTypes.map((workType, index) => (
          <li key={index}>
            {language === "fi"
              ? workType.description ?? "Muu"
              : capitalize(workType.type.replaceAll("_", " "))}
          </li>
        ))}
      </ul>
      <h4 className="font-medium">{t("restrictions", { ns: "roadworks" })}</h4>
      <ul className="my-1.5 pl-2">
        {roadwork.restrictions.map((restriction, index) => (
          <li key={index}>
            {language === "fi"
              ? restriction.name
              : capitalize(restriction.type.replaceAll("_", " "))}
            {restriction.quantity && restriction.unit
              ? " (" + restriction.quantity + " " + restriction.unit + ")"
              : null}
          </li>
        ))}
      </ul>
    </div>
  );
}
