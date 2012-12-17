create table 
	if not exists 
		Pages
		(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			
			XKey text not null, 
			Content text not null

		)