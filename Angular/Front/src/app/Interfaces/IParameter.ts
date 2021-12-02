
export interface IParameter {
  Id: number;
  Name: string;
}
export class Parameter implements IParameter {
  constructor(public Id: number, public Name: string) {

  }


}
