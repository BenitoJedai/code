create table if not exists 

AppSnapshot 

(
-- AUTOINCREMENT is only allowed on an INTEGER PRIMARY KEY
AppSnapshotKey integer PRIMARY KEY AUTOINCREMENT, 
-- app handler will start serving the html at /key url
-- http://dev.mysql.com/doc/refman/5.1/en/blob.html
AppSnapshotContent LONGTEXT not null 
)