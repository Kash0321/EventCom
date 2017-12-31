import { Component } from '@angular/core';

import { HubConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    constructor() {
        let connection = new HubConnection('/messaging');

        connection.on('send', data => {
            console.log(data);
        });

        connection.start()
            .then(() => connection.invoke('send', 'Kash', 'Hello'));
    }
}
