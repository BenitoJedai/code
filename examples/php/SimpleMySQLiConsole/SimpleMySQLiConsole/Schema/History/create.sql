create table 
	if not exists 
		History1(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			-- sqlite vs mysql
			-- http://www.sqlite.org/datatype3.html
			query text not null,
			context text not null

		)