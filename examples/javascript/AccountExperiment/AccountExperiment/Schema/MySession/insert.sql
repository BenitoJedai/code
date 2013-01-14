insert into MySessionToken 
(
ticks,
cookie,
account
) 
values 
(
@ticks /* bigint */, 
@cookie /* text */,
@account /* bigint */

)