import {
  LocationRequest,
  locationRequest,
} from "@/lib/contracts/location/location.request";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
} from "@/shared/components/ui/form";
import { Input } from "@/shared/components/ui/input";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Button } from "@/shared/components/ui/button";
import { Search } from "lucide-react";

interface LocationSearchFormProps {
  data?: LocationRequest;
  isLoading?: boolean;
  onSubmit: (values: LocationRequest) => void;
}

export function LocationSearchForm({
  data,
  isLoading,
  onSubmit,
}: LocationSearchFormProps) {
  const form = useForm<LocationRequest>({
    resolver: zodResolver(locationRequest),
    defaultValues: {
      query: "",
    },
    values: data,
  });

  return (
    <div className="bg-white shadow-md p-1 rounded-md border border-border">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <FormField
            control={form.control}
            name="query"
            disabled={isLoading}
            render={({ field }) => (
              <FormItem>
                <FormControl>
                  <div className="flex gap-x-2">
                    <Input
                      className="border-transparent focus-visible:outline-none focus-visible:ring-transparent"
                      placeholder="Search location..."
                      {...field}
                    />
                    <Button
                      loading={isLoading}
                      className="shrink-0"
                      variant="link"
                      size="icon"
                    >
                      <Search className="w-4 h-4" />
                    </Button>
                  </div>
                </FormControl>
              </FormItem>
            )}
          />
        </form>
      </Form>
    </div>
  );
}
