export interface IUnit {
   id: number;
  title: string;
  description: string;
  subjectId: number;
  subjectName?: string;
}

export interface ISection {
  unitName: string;
  lessons: {
    id: number;
    number: number;
    title: string;
  }[];
}
