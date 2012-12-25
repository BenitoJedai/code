create table 
	if not exists 
		SystemConsoleOut
		(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			
			session INTEGER not null, 

			value text not null

		)