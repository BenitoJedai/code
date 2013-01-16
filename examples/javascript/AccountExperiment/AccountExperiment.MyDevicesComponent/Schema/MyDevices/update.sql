update MyDeviceToken set
                     name = @name /* text */
                    , value = @value /* text */
                     
where MyDeviceToken.account = @account /* bigint */ 
and MyDeviceToken.id = @id /* bigint */ 
                    