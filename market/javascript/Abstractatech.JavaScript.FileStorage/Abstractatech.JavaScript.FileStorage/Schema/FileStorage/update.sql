update FileStorageTable set
                     ContentValue = @ContentValue /* text */
                     where ContentKey = @ContentKey /* integer */
                    