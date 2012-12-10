create table 
	if not exists 
		SensorySync(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			-- sqlite vs mysql
			-- http://www.sqlite.org/datatype3.html
			ms BIGINT not null,

			-- ivec2, look around
			x INTEGER not null,
			y INTEGER not null,

			-- 0 or 1. 1 means keys shall stay up, strafe
			-- ivec4
			goleft INTEGER not null,
			goup INTEGER not null,
			goright INTEGER not null,
			godown INTEGER not null


		)

-- consult X:\jsc.svn\examples\javascript\forms\DeltaExperiment\DeltaExperiment\ApplicationWebService.cs