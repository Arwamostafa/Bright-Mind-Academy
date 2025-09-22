import { Component, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Chatbotservice } from '../../services/chatbotservice';

@Component({
  selector: 'app-chatbotspesefic',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chatbotspesefic.html',
  styleUrls: ['./chatbotspesefic.css']
})
export class Chatbotspesefic {
  prompt: string = "";
  messages: { role: string, content: string }[] = [];
  loading = false;

  @ViewChild('chatContainer') chatContainer!: ElementRef;

  constructor(private _Chatbotservice: Chatbotservice) {}

getanswer(): void {
  const userMessage = this.prompt.trim();
  if (!userMessage) return;

  this.messages.push({ role: 'user', content: userMessage });
  this.loading = true;

  this._Chatbotservice.getanswerspesefic(userMessage).subscribe({
    next: (res) => {
      // ✅ Properly extract the "answer" field from the response
      const responseText = res.answer || '⚠️ No response from bot.';

      this.messages.push({ role: 'bot', content: responseText });
      this.loading = false;
      this.prompt = '';
      this.scrollToBottom();
    },
    error: (err) => {
      console.error(err);
      this.messages.push({ role: 'bot', content: '⚠️ Something went wrong.' });
      this.loading = false;
      this.scrollToBottom();
    }
  });
}


  scrollToBottom(): void {
    setTimeout(() => {
      if (this.chatContainer) {
        this.chatContainer.nativeElement.scrollTop = this.chatContainer.nativeElement.scrollHeight;
      }
    }, 100);
  }
}
