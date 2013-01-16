insert into MyDeviceToken 
(
ticks,
name,
value,
account
) 
values 
(
@ticks /* bigint */, 
@name /* text */,
@value /* text */,

-- can we do generic? IIdentity 
@account /* bigint */

)