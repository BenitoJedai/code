create table if not exists TheGridTable 
(
    ContentKey INTEGER PRIMARY KEY AUTOINCREMENT
    , ContentValue text not null
    , ContentComment text not null
    , ParentContentKey INTEGER 
    , FOREIGN KEY(ParentContentKey) REFERENCES TheGridTable (ContentKey)
) 