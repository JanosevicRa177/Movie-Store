import { Role } from "src/app/api/api-reference";

export interface Route {
    name: string;
    route: string;
    protected: boolean;
    role?: Role;
}
