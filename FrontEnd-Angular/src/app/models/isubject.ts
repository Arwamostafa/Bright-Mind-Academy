export interface ISubject {
  subjectId: number,
  instructorName : string,
  subjectName: string,
  subjectDescription: string,
  subjectPrice: number,
  imgUrl: string
}

export interface IISubject {
    subjectID:number;
    subjectName:string;
    subjectDescription:string;
    instructorID:number;
    price: number;
    instructorName: string,
    classID:number;
    trackID:number;
    className? :string;
    trackName? :string;
}

