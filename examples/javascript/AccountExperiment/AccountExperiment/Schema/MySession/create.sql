create table 
	if not exists 
		MySessionToken
		(
			id INTEGER PRIMARY KEY AUTOINCREMENT,
			
			-- account, who knew password

			-- time, when will this expire?
			-- any last interaction + 5 min? timer in client? 
		
			ticks BIGINT not null,

			cookie text not null,

			account INTEGER, 
			
			FOREIGN KEY(account) REFERENCES  MyAccountToken (id)

		)