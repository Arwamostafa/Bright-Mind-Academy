import { Component, ElementRef, OnInit } from '@angular/core';
import * as SignalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserAuthentication } from '../../services/user-authentication';

@Component({
  selector: 'app-chat',
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class Chat implements OnInit {

  private con: SignalR.HubConnection;
  msg: { text: string, isLeft: boolean }[] = [];

  name:string | null = " ";
  isConnected: boolean = false;
  isLeft: boolean = false;
  message: string = "";
  readMsg: string ="";


  constructor(private _authUser: UserAuthentication) {

    this.con = new SignalR.HubConnectionBuilder()
  .withUrl("https://localhost:7092/chat", {
    withCredentials: true
  })
  .withAutomaticReconnect()
  .build();

  // prompt("start con");

    this.con.start().then(() => {
    this.isConnected = true;
    console.log("Connection started");
  }).catch(err => {
    console.error("SignalR connection error:", err);
  });

    // prompt("consume");

    this.con.on("newMsg", (n, m) => {
      const isMsgFromMe = m === this.message;
      this.isLeft = !isMsgFromMe;

      if(!this.isLeft){
        this.msg.push({
          text: `${m}`,
          isLeft: !isMsgFromMe
        });
      } else {
        this.msg.push({
          text: `${n} : ${m}`,
          isLeft: !isMsgFromMe
        });
      }

      this.readMsg = m;
    });


  }
  sendMesage(){
    // console.log(sentMsg.value);
    // this.con.invoke("SendMessage",this.name, sentMsg.value);
    if (this.isConnected) {
    this.con.invoke("SendMessage", this.name, this.message).then(() => {
      // this.msg.push({
      //   text: `${this.name}: ${this.message}`,
      //   isLeft: false // sent by me → right side
      // });
      this.message = ''; // clear input
    });
  } else {
    console.warn("Connection not ready yet.");
  }

  }

  ngOnInit(){
    this.name = this._authUser.getFullName();
  }
}
