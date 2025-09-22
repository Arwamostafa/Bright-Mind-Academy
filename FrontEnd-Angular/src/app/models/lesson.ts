export interface Ilesson {
  id: number;
  title: string;
  description?: string;
  unitId: number;
  unitName: string;
  videoUrl?: string;
  pdfUrl?: string;
  assigmentUrl?: string;
  assigmentDeadLine?: Date;
}
