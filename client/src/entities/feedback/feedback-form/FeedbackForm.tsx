import {
  FeedbackRequest,
  feedbackRequest,
} from "@/lib/contracts/feedback/feedback.request";
import { Button } from "@/shared/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/shared/components/ui/form";
import { Input } from "@/shared/components/ui/input";
import { Textarea } from "@/shared/components/ui/textarea";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface FeedbackFormProps {
  isLoading?: boolean;
  onSubmit: (values: FeedbackRequest) => void;
}

export function FeedbackForm({ isLoading, onSubmit }: FeedbackFormProps) {
  const { t } = useTranslation(["feedback"]);
  const form = useForm<FeedbackRequest>({
    resolver: zodResolver(feedbackRequest),
    defaultValues: {
      title: "",
      description: "",
    },
  });

  return (
    <Form {...form}>
      <form className="space-y-3" onSubmit={form.handleSubmit(onSubmit)}>
        <FormField
          control={form.control}
          name="title"
          disabled={isLoading}
          render={({ field }) => (
            <FormItem>
              <FormLabel>{t("titleLbl")}</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="description"
          disabled={isLoading}
          render={({ field }) => (
            <FormItem>
              <FormLabel>{t("descriptionLbl")}</FormLabel>
              <FormControl>
                <Textarea {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button loading={isLoading} className="w-full sm">
          {t("submitBtn")}
        </Button>
      </form>
    </Form>
  );
}
