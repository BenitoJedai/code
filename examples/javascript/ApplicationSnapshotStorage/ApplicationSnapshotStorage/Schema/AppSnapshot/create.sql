create table if not exists 

AppSnapshot 

(

AppSnapshotKey INTEGER PRIMARY KEY AUTOINCREMENT, 
-- app handler will start serving the html at /key url
AppSnapshotContent text not null, 

)