insert into MyAccountToken 
(
ticks,
name,
email,
web,
skype,
password 
) 
values 
(
@ticks /* bigint */, 
@name /* text */,
@email /* text */,
@web /* text */,
@skype /* text */,
@password /* text */
)