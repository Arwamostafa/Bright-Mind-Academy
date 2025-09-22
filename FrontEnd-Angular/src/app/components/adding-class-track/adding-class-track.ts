


import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IClass } from '../../models/iclass.ts';
import { ITrack } from '../../models/itrack.ts';
import { ClassService } from '../../services/class-service.js';
import { TrackService } from '../../services/track-service.js';

@Component({
  selector: 'app-adding-class-track',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './adding-class-track.html',
  styleUrl: './adding-class-track.css'
})
export class AddingClassTrack implements OnInit {

 editClassId: number | null = null;
  classes!: IClass[] ;

  ClassForm:FormGroup;

  // track
  editTrackId: number | null = null;
  tracks!: ITrack[] ;
  TrackForm:FormGroup;

  constructor(private _classService: ClassService, private _trackService: TrackService){
    this.ClassForm= new FormGroup({
      className: new FormControl('', [Validators.required, Validators.minLength(3)])
    });

    this.TrackForm= new FormGroup({
      trackName: new FormControl('', [Validators.required, Validators.minLength(3)])
    });
  }
  get className(){return this.ClassForm.get('className')}

  ngOnInit(){
    this.loading();
  }

  loading(){
       this._classService.getAllClasses().subscribe({
      next: (next) => {
        // console.log(next);

        this.classes = next
      }
    });



    this._trackService.getAllTracks().subscribe({
      next: (data) => {

        this.tracks = data ;
        // console.log(this.tracks);
      }
    })
  }

  addClass() {
    if (this.ClassForm.valid) {
      const formData = this.ClassForm.value;

      if (this.editClassId !== null) {
        // Update existing class
        const index = this.classes.findIndex(i => i.classID === this.editClassId);
        if (index > -1) {
          console.log(formData);
          this._classService.updateClass(this.editClassId, formData).subscribe({
            next: (data)=>{
              console.log(data);

            },
            error: (err) => {
              console.error("Failed to uodate class:", err)
            }
          })
          this.loading();
          // this.classes[index] = { id: this.editClassId, ...formData };
        }
        this.editClassId = null; // Exit edit mode
      } else {
        // Add new class
        // const newId = this.classes.length > 0 ? Math.max(...this.classes.map(i => i.classID)) + 1 : 1;
        // this.classes.push({ id: newId, ...formData });

        this._classService.addClass(formData).subscribe({
          next : (data) => {

            console.log(data);
          },
          error: (err) => {
          console.error("Failed to add class:", err)
        }
        })
        this.loading();
        this.ClassForm.reset()
      }
    }
  }
  classByIndex(index: number): number {
    return index;
  }
  DeleteClass(id: number) {
    // this.classes = this.classes.filter(classes => classes.classID !== id);
    // If the deleted unit is being edited, reset the form
    this._classService.deleteClass(id).subscribe({
          next : (data) => {

            console.log(data);
          },
          error: (err) => {
          console.error("Failed to delete class:", err)
        }
    });
    this.loading();
    if (this.editClassId === id) {
      this.ClassForm.reset();
      this.editClassId = null;
    }
  }
  UpdateClass(id: number) {
    const classToUpdate = this.classes.find(classes=> classes.classID === id);
    if (classToUpdate) {
      this.ClassForm.patchValue(classToUpdate);
      this.editClassId = id;  // Set edit mode
    }
  }


// track




  get trackName(){return this.TrackForm.get('trackName')}
addTrack() {
    if (this.TrackForm.valid) {
      const formData = this.TrackForm.value;

      if (this.editTrackId !== null) {
        // Update existing track
        const index = this.tracks.findIndex(i => i.trackID === this.editTrackId);
        if (index > -1) {
          // this.tracks[index] = { id: this.editTrackId, ...formData };
          this._trackService.updateTrack(this.editTrackId, formData).subscribe({
            next: (data)=>{
              console.log(data);

            },
            error: (err) => {
              console.error("Failed to update track:", err)
            }
          })
          this.ngOnInit();
        }
        this.editTrackId = null; // Exit edit mode
      } else {
        // Add new track
        // const newId = this.trackes.length > 0 ? Math.max(...this.trackes.map(i => i.id)) + 1 : 1;
        // this.trackes.push({ id: newId, ...formData });

        this._trackService.addTrack(formData).subscribe({
          next : (data) => {

            console.log(data);
          },
          error: (err) => {
          console.error("Failed to add track:", err)
        }
        })
        this.ngOnInit();
        this.TrackForm.reset()
      }
    }
  }
  trackByIndex(index: number): number {
    return index;
  }
  DeleteTrack(id: number) {
    // this.trackes = this.trackes.filter(track => track.id !== id);
    // If the deleted unit is being edited, reset the form
    this._trackService.deleteTrack(id).subscribe({
          next : (data) => {

            console.log(data);
          },
          error: (err) => {
          console.error("Failed to delete track:", err)
        }
    });
    this.ngOnInit();
    if (this.editTrackId === id) {
      this.TrackForm.reset();
      this.editTrackId = null;
    }
  }
  UpdateTrack(id: number) {
    console.log(id);

    const trackToUpdate = this.tracks.find(track => track.trackID === id);
    if (trackToUpdate) {
      this.TrackForm.patchValue(trackToUpdate);
      this.editTrackId = id;  // Set edit mode
    }
  }
}
