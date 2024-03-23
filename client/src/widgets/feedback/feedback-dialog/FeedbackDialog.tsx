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
  const { mutate, isPending, isError, error } = useCreateFeedback();

  const [open, setOpen] = useState<boolean>(false);

  const onSubmit = (values: FeedbackRequest) => {
    mutate(values, {
      onSuccess: () => setOpen(false),
    });
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
      />
      <FeedbackForm isLoading={isPending} onSubmit={onSubmit} />
    </DialogBase>
  );
}
