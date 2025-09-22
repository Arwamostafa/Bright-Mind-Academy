import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AssigmentService {
  private questions = [
  {
    id: 1,
    question: 'What is the basic unit of life?',
    options: ['Atom', 'Molecule', 'Cell', 'Tissue'],
    correct: 'Cell'
  },
  {
    id: 2,
    question: 'Which organelle is known as the powerhouse of the cell?',
    options: ['Nucleus', 'Mitochondria', 'Ribosome', 'Golgi body'],
    correct: 'Mitochondria'
  },
  {
    id: 3,
    question: 'DNA is found only in the nucleus of a cell.',
    options: ['True', 'False'],
    correct: 'False'
  },
  {
    id: 4,
    question: 'Which system in the human body is responsible for transporting oxygen and nutrients?',
    options: ['Digestive system', 'Circulatory system', 'Respiratory system', 'Nervous system'],
    correct: 'Circulatory system'
  },
  {
    id: 5,
    question: 'Name the pigment that gives plants their green color.',
    options: ['Chlorophyll', 'Hemoglobin', 'Melanin', 'Keratin'],
    correct: 'Chlorophyll'
  },
  {
    id: 6,
    question: 'Which part of the plant is responsible for photosynthesis?',
    options: ['Root', 'Stem', 'Leaf', 'Flower'],
    correct: 'Leaf'
  },
  {
    id: 7,
    question: 'The process by which living organisms maintain a stable internal environment is called:',
    options: ['Digestion', 'Homeostasis', 'Respiration', 'Metabolism'],
    correct: 'Homeostasis'
  }
];


  getQuestions() {
    return this.questions;
  }

  calculateScore(answers: any): number {
    let score = 0;
    this.questions.forEach(q => {
      if (answers[q.id] === q.correct) score++;
    });
    return score;
  }

  
}
