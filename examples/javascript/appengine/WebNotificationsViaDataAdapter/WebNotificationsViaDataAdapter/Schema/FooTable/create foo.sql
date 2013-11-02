create table if not exists 

FooTable 

(
-- Caused by: java.lang.RuntimeException: You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'INTEGER PRIMARY KEY AUTO_INCREMENT
-- , delay INTEGER
-- , text text not null
-- http://stackoverflow.com/questions/3949064/auto-increment-column

FooTableKey INTEGER  NOT NULL  PRIMARY KEY AUTOINCREMENT

, delay INTEGER 
, text text not null



)