create table if not exists 

Table1Meta 

(
MetaKey INTEGER PRIMARY KEY AUTOINCREMENT

, DeclaringType INTEGER 

, MemberName text not null
, MemberValue text not null

, FOREIGN KEY(DeclaringType) REFERENCES Table1(ContentKey)
)