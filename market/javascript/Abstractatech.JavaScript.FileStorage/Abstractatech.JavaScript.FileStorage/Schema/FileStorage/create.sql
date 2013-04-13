create table if not exists 

FileStorageTable 

(
ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, 
ContentValue text not null, 
ContentType text not null, 
ContentBytes blob
)