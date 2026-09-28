'use client'
import { Empty, EmptyDescription, EmptyHeader, EmptyMedia, EmptyTitle } from "@/components/ui/empty";
import { Loader2Icon } from "lucide-react";

export const AuthLoading = () => {
  return <div
      role="status"
      aria-live="polite"
      className="fixed inset-0 z-50 flex flex-col items-center justify-center bg-white dark:bg-zinc-950"
    >
      <Empty className="w-full">
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <Loader2Icon className="size-8 animate-spin" />
        </EmptyMedia>
        <EmptyTitle>Đang xác thực tài khoản</EmptyTitle>
        <EmptyDescription>
          Vui lòng đợi trong khi chúng tôi xử lý yêu cầu của bạn. Không làm mới trang.
        </EmptyDescription>
      </EmptyHeader>
    </Empty>
    </div>;
}