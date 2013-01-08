create table 
	if not exists 
		PointerSync(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			-- sqlite vs mysql
			-- http://www.sqlite.org/datatype3.html
			ticksms BIGINT not null,

			-- (zyx ivec3) do we have custom types available?
			message text not null

		)