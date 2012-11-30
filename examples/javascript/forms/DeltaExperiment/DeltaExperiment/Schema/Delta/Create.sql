create table 
	if not exists 
		Delta(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			-- sqlite vs mysql
			-- http://www.sqlite.org/datatype3.html
			ticks BIGINT not null,

			-- (zyx ivec3) do we have custom types available?
			x INTEGER not null,
			y INTEGER not null,
			z INTEGER not null
		)