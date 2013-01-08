insert into PointerSync (ticksms, message) values (

-- Now.ticks / 1000?
-- server side timestamp, do we need it?
-- only to delete old messages? like a timeout?
@ticksms /* bigint */, 

-- what about xml? what about json? anonymous types?
@message /* text */
)