'use strict';

(function (W, D, WS) {

    const url = `ws://${D.location.host}/echo`;
    //const url = `ws://echo.websocket.org/`;
    

    let _ws;

    class MyWebSocket {

        constructor() {
            _ws = new WS(url);

            _ws.onopen = (e) => { console.log(e); }
            _ws.onclose = (e) => { console.log(e); }
            _ws.onerror = (e) => { console.log(e); }
        }

        send(message) {
            _ws.send(JSON.stringify(message));
            //_ws.send(message);
        }

        listen(callback) {
            _ws.onmessage = (e) => {
                console.log(e);
                var data;

                switch (typeof(e.data)) {
                    case 'string':
                        data = e.data;
                        break;
                    case 'object':
                        data = JSON.parse(e.data);
                        break;
                }

                callback(data);
            };
        }
    }

    W.onload = () => {
        W.myWebSocket = new MyWebSocket();
    }

    // W.onunload = () => {
    //     if (W.myWebSocket) {
    //         W.myWebSocket.close();
    //     } 

    // }

})(window, document, WebSocket);