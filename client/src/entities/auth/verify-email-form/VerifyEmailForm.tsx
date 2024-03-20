import {
  VerifyEmailRequest,
  verifyEmailRequest,
} from "@/lib/contracts/auth/verify-email.request";
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
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

interface VerifyEmailFormProps {
  data?: VerifyEmailRequest;
  isLoading?: boolean;
  onSubmit: (data: VerifyEmailRequest) => void;
}

export function VerifyEmailForm({
  data,
  isLoading,
  onSubmit,
}: VerifyEmailFormProps) {
  const form = useForm<VerifyEmailRequest>({
    resolver: zodResolver(verifyEmailRequest),
    defaultValues: {
      email: "",
      token: "",
    },
    values: data,
  });

  return (
    <Form {...form}>
      <form className="space-y-5" onSubmit={form.handleSubmit(onSubmit)}>
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input type="email" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="token"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Token</FormLabel>
              <FormControl>
                <Input type="text" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button loading={isLoading} type="submit" className="w-full">
          Verify my account
        </Button>
      </form>
    </Form>
  );
}
