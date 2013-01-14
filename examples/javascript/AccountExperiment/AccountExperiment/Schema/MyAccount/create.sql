create table 
	if not exists 
		MyAccountToken
		(
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			
			ticks BIGINT not null,

			name text not null,

			email text not null,

			web text not null,

			skype text not null,

			password text not null

			-- http://www.informationweek.com/security/attacks/sony-hacked-again-1-million-passwords-ex/229900111

			-- !!! http://dustwell.com/how-to-handle-passwords-bcrypt.html
			-- http://arr.gr/blog/2012/01/storing-passwords-the-right-way/
			-- http://phpsec.org/articles/2005/password-hashing.html
			-- http://blog.moertel.com/articles/2006/12/15/never-store-passwords-in-a-database


			-- how many levels of security?
			-- level0 - plain password
			-- level10 - bcrypt
			-- level100 - secondary device, phone
			-- level1000 - biometrics

		)