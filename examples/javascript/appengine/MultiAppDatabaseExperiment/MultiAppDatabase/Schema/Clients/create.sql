create table if not exists 

Clients 

(
ClientID INTEGER PRIMARY KEY AUTOINCREMENT, 
Username text not null, 
Password text not null,
ScreenHeight integer,
ScreenWidth integer
)