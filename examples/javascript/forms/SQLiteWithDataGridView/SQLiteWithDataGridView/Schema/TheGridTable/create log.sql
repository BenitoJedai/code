create table if not exists TheGridTableLog
(
ContentKey INTEGER PRIMARY KEY AUTOINCREMENT
, ContentReferenceKey INTEGER 
, ContentComment text not null 
, FOREIGN KEY(ContentReferenceKey) REFERENCES  TheGridTable (ContentKey)
)