export interface ILessonCreate {
    title: string;
  description?: string;
  unitId: number;
  videoUrl?: File;
  pdfUrl?: File;
  assigmentUrl?: File;
  assigmentDeadLine?: Date;
}
