update TheGridTable set
                     ContentValue = @ContentValue /* text */
                    , ContentComment = @ContentComment /* text */
                     where ContentKey = @ContentKey /* integer */
                    