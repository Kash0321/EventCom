import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';
declare var jquery: any;
declare var $: any;

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
    private _hubConnection: HubConnection;
    public async: any;
    tuser = '';
    message = '';
    messages: any[] = [];

    constructor() {
    }

    public sendMessage(): void {
        this._hubConnection.invoke('Send', this.tuser, this.message);
        //this.messages.unshift({ 'user': this.tuser, 'message': this.message, 'sentorreceived': 'Sent' });
    }

    ngOnInit() {
        this._hubConnection = new HubConnection('/messaging');

        this._hubConnection.on('Send', (user: string, message: string) => {
            let d = new Date();
            let dStr = d.getDate() + '/' + d.getMonth() + 1 + '/' + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes();
            this.messages.unshift({ 'user': user, 'message': message, 'sentorreceived': 'Received', 'date': dStr  });
        });

        this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });
    }

}
