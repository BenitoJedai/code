create table 
	if not exists 
		MyDeviceToken
		(
			id INTEGER PRIMARY KEY AUTOINCREMENT,
			
			-- every entry needs identity and timing?
			ticks BIGINT not null,

			name text not null,
			value text not null,

			-- at this point we do not know about 
			-- the system who has account info
			-- all we know its and id to the account
			-- in the system we are compiled to
			account INTEGER
		)