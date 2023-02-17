var conection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();
connection.start(); 