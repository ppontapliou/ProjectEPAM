import { Contact } from './IContact';


export class Ad {
  Id: number=0;
  Name: string='';
  Title: string='';
  DateCreation: Date;
  Contact: Contact=new Contact();
  Picture: string='';
  State: string='';
  Type: string;
  Category: string='';
  Adress: string='';
}
