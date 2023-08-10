import { ToastrService } from "ngx-toastr";

export function toastError(toastr: ToastrService, error: any) {
    Object.keys(error.errors).forEach(function (key) {
        error.errors[key].forEach((error: string | undefined) => {
            toastr.error(error);
        });
    });
}