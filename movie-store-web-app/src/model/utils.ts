export interface Route {
  name: string;
  route: string;
}
export interface EditModalReturn {
  id: string;
  state: 'SUCCESS' | 'ERROR' | 'NONE';
}
