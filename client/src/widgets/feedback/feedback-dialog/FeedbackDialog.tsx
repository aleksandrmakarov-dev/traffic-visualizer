import { FeedbackForm } from "@/entities/feedback";
import { useCreateFeedback } from "@/features/feedback/create";
import { FeedbackRequest } from "@/lib/contracts/feedback/feedback.request";
import { DialogBase } from "@/shared/components/DialogBase";
import { FormAlert } from "@/shared/components/FormAlert";
import { useState } from "react";

interface FeedbackDialogProps {
  trigger?: JSX.Element;
}

export function FeedbackDialog({ trigger }: FeedbackDialogProps) {
  const { mutate, isPending, isError, error, isSuccess } = useCreateFeedback();

  const [open, setOpen] = useState<boolean>(false);

  const onSubmit = (values: FeedbackRequest) => {
    mutate(values);
  };

  return (
    <DialogBase
      trigger={trigger}
      title="Give a feedback"
      open={open}
      setOpen={setOpen}
    >
      <FormAlert
        className="mb-2"
        isError={isError}
        error={error?.response?.data}
        isSuccess={isSuccess}
        success={{
          title: "We received you feedback",
          message:
            "Thank you for your feedback. We will carefully look through it",
        }}
      />
      <FeedbackForm isLoading={isPending} onSubmit={onSubmit} />
    </DialogBase>
  );
}
